#undef  LOG_DISABLED
#define REPLACE_LOG_ITEM_GUID_SESSION
#undef ASYNC_ADD_LOG

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob.Interfaces;
using ProRob.Threading;

namespace ProRob.Log
{
    public class ProLogLiteTXT : IProLog, IStoppable
    {
        public const int MaxElementsInQueue = 10;

        public const int LimitResults = 1000;
        public static Guid CurrentGuidSession { get; private set; } = Guid.NewGuid();

        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private Task task;

        private readonly BlockingCollection<string> queue = new BlockingCollection<string>();
        private readonly Counter threadsInQueue = new Counter();
        private StreamWriter streamWriter;
        public int ElementDiscarded { get; private set; } = 0;
        public bool WorksInQueue => threadsInQueue.CurrentCount > 0;
        public int MessagesLogged { get; private set; } = 0;
        public int Errors { get; private set; } = 0;
        public bool IsRunning { get; private set; } = false;
        public bool AddNewLine { get; private set; } = false;

        public string LogFolderPath { get; private set; }
        public string LogName { get; private set; }
        public string LogFullPath { get; private set; }


        public ProLogLiteTXT(string logFolderPath, string logName, bool addNewLine = false)
        {
            Console.WriteLine("[ProLogLiteTXT]");
            TicToc.Tic("");

            LogFolderPath = logFolderPath;
            LogName = logName;
            AddNewLine = addNewLine;

            LogFullPath = Path.Combine(LogFolderPath, $"{DateTime.Now.ToString("yyyyMMdd")}_{logName}");

            Console.WriteLine(LogFullPath);

            streamWriter = new StreamWriter(LogFullPath, true);

            task = Task.Run(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;

                TaskLogHandler(cancellationTokenSource.Token);
            });

            IsRunning = true;

            TicToc.Toc();

            AddLog($"\n\n\n///////////////////////////////////////////////////////////////////////////////////////\n");
            AddLog($"// {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} \t UTC: {DateTime.UtcNow}\n");//MMIx08
            AddLog($"///////////////////////////////////////////////////////////////////////////////////////\n\n\n");
        }

        private void TaskLogHandler(CancellationToken cancellationToken)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;

            while (!cancellationToken.IsCancellationRequested)
            {
                while (!queue.IsCompleted)
                {
                    string item;

                    try
                    {
                        item = queue.Take();
                        AddLogInternal(item);
                    }
                    catch
                    {
                        Errors++;
                    }
                }
            }
        }

        private void AddLogInternal(string item)
        {
#if LOG_DISABLED
            return;
#else
            if (!IsRunning)
            {
                return;
            }

#if ASYNC_ADD_LOG
            Task.Run(() =>
            {
#endif
            threadsInQueue.Increment();

            try
            {
                if (AddNewLine)
                {
                    streamWriter.WriteLine($"[{DateTime.Now}] {item}");//MMIx08
                }
                else
                {
                    streamWriter.Write($"[{DateTime.Now}] {item}");//MMIx08
                }
            }
            finally
            {
                threadsInQueue.Decrement();
            }
#if ASYNC_ADD_LOG
            });
#endif

#endif //LOG_DISABLED
        }

        public void AddLog(string value)
        {
            AddLog(value, LogType.ConsoleOutput);
        }

        public void AddLog(string log, LogType logType = LogType.ConsoleOutput)
        {
#if LOG_DISABLED
            return;
#else
            if (string.IsNullOrEmpty(log))
            {
                return;
            }

            if (IsRunning == false)
            {
                return;
            }

            if (queue.Count() > MaxElementsInQueue || threadsInQueue.CurrentCount > MaxElementsInQueue)
            {
                ElementDiscarded++;
                return;
            }

            queue.Add(log);
#endif
        }

        public IEnumerable<LogItem> GetSessionLogs(Guid guidSession, LogType logType = LogType.All, int limit = LimitResults)
        {
#if LOG_DISABLED
            return new List<LogItem>();
#else

            return null;
#endif
        }

        public IEnumerable<LogItem> GetCurrentSessionLogs(LogType logType = LogType.All, int limit = LimitResults)
        {
            return GetSessionLogs(CurrentGuidSession);
        }

        public void Stop()
        {
            if (!IsRunning)
            {
                return;
            }

            IsRunning = false;
            queue.CompleteAdding();
            cancellationTokenSource.Cancel();
            task.Wait();

            Console.WriteLine($"[ProLogger] threadsInQueue: {threadsInQueue.CurrentCount}");
            threadsInQueue.WaitValue(0);

            streamWriter.Close();

            Console.WriteLine($"[ProLogger] CheckPoint()");

            Console.WriteLine($"[ProLogger] Disposed (Message logged: {MessagesLogged} Elements discarted: {ElementDiscarded} Errors: {Errors})");
        }
    }
}
