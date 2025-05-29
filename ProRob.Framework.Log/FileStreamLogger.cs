using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using ProRob;

namespace ProRob.Log
{
    //----------------------------
    // Singleton Design Pattern 
    //----------------------------
    public sealed class FileStreamLogger
    {
        private static readonly TimeSpan IntervalCheckDate = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan IntervalWait = TimeSpan.FromMilliseconds(1);

        private static FileStreamLogger instance;

        private static readonly Object locker = new Object();

        private static Task taskLogger;
        private static Task taskCheckDate;

        private static readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private static readonly BlockingCollection<string> queue = new BlockingCollection<string>();
        private static FileStream fileStream = null;
        private static volatile bool promiseToStartNewFileStream = false;

        public static string LogFolder { get; private set; }
        public static string LogFilename { get; private set; }
        public static string LogExtension { get; private set; }
        public static string PathLogFile { get; private set; }

        public static bool Initialized { get; private set; } = false;
        public static int ElementsLogged { get; private set; } = 0;
        public static int ElementsProcessed { get; private set; } = 0;
        public static int Errors { get; private set; } = 0;

        static FileStreamLogger()
        {
            var _ = new FileStreamLogger();
        }

        public static void AddLog(string content)
        {
            if (Initialized)
            {
                if (queue.IsAddingCompleted == false)
                {
                    queue.Add(content);

                    ElementsLogged++;
                }
            }
        }

        public static void Start(string logFolder, string logFilename, string logExtension)//, bool appendDate = true)
        {
            if (Initialized == false)
            {
                LogFolder = logFolder;
                LogFilename = logFilename;

                if (logExtension.ElementAt(0).Equals('.') == false)
                {
                    logExtension = $".{logExtension}";
                }

                LogExtension = logExtension;

                PathLogFile = GetPathLogFile();

                Directory.CreateDirectory(LogFolder);

                taskCheckDate = Task.Run(() => TaskCheckDate(cancellationTokenSource.Token));
                taskLogger = Task.Run(() => TaskLogger(cancellationTokenSource.Token));

                Initialized = true;

                AddLog($"\n\n\n///////////////////////////////////////////////////////////////////////////////////////\n");
                AddLog($"// {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}\n");
                AddLog($"///////////////////////////////////////////////////////////////////////////////////////\n\n\n");

                ProConsole.WriteLine($"[LOGGER] {DateTime.Now} Started", ConsoleColor.Green);
            }
        }

        private static void TaskCheckDate(CancellationToken cancellationToken)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;

            int precedentDayOfYear = -1;

            while (!cancellationToken.IsCancellationRequested)
            {
                int currentDayOfYear = DateTime.Now.DayOfYear;

                if (currentDayOfYear != precedentDayOfYear)
                {
                    PathLogFile = GetPathLogFile();

                    promiseToStartNewFileStream = true;

                    Console.WriteLine($"[LOGGER] PathLogFile={PathLogFile}");
                }

                precedentDayOfYear = currentDayOfYear;

                Thread.Sleep(IntervalCheckDate);
            }
        }

        private static string GetPathLogFile()
        {
            return Path.Combine(LogFolder, $"{LogFilename}_{DateTime.Now:yyyyMMdd}{LogExtension}");
        }

        private static void TaskLogger(CancellationToken cancellationToken)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;

            while (!cancellationToken.IsCancellationRequested)
            {
                while (promiseToStartNewFileStream == false)
                {
                    Thread.Sleep(IntervalWait);
                }

                ProConsole.WriteLine($"[LOGGER] TaskLogger started", ConsoleColor.Green);

                while (!queue.IsCompleted)
                {
                    try
                    {
                        var content = queue.Take();

                        byte[] buffer = Encoding.UTF8.GetBytes(content);

                        if (promiseToStartNewFileStream)
                        {
                            promiseToStartNewFileStream = false;

                            if (fileStream != null)
                            {
                                fileStream.Close();
                                fileStream.Dispose();
                            }

                            fileStream = new FileStream(PathLogFile, FileMode.Append, FileAccess.Write);
                        }

                        fileStream?.Write(buffer, 0, buffer.Length);

                        ElementsProcessed++;
                    }
                    catch
                    {
                        if (queue.IsAddingCompleted == false)
                        {
                            Errors++;
                        }
                    }
                }
            }

            if (fileStream != null)
            {
                fileStream.Close();
                fileStream.Dispose();
            }

            ProConsole.WriteLine($"[LOGGER] TaskLogger terminated", ConsoleColor.Red);
        }

        public static void Stop()
        {
            if (Initialized)
            {
                string message = $"[LOGGER] {DateTime.Now} Stopped (Elements logged:{ElementsLogged} Elements processed: {ElementsProcessed} Errors:{Errors})";
                
                AddLog(message);

                queue.CompleteAdding();

                cancellationTokenSource.Cancel();

                taskLogger.Wait();

                queue.Dispose();

                Initialized = false;

                ProConsole.WriteLine(message, ConsoleColor.Red);
            }
        }

        public static FileStreamLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new FileStreamLogger();
                        }
                    }
                }

                return instance;
            }
        }
    }
}