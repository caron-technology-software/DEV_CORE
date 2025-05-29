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

using LiteDB;

using ProRob.Interfaces;
using ProRob.Threading;

namespace ProRob.Log
{
    public class ProLogLiteDB : IProLog, IStoppable
    {
        public const int MaxElementsInQueue = 10;

        public const int LimitResults = 1000;
        public static Guid CurrentGuidSession { get; private set; } = Guid.NewGuid();

        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private Task task;

        private readonly LiteDatabase db;
        public readonly ILiteCollection<LogItem> Collection;

        private readonly BlockingCollection<LogItem> queue = new BlockingCollection<LogItem>();
        private readonly Counter threadsInQueue = new Counter();

        public int ElementDiscarded { get; private set; } = 0;
        public bool WorksInQueue => threadsInQueue.CurrentCount > 0;
        public int MessagesLogged { get; private set; } = 0;
        public int Errors { get; private set; } = 0;
        public bool IsRunning { get; private set; } = false;

        private string ComposeStringConnection(string path, string password, bool readOnly = false)
        {
            var sb = new StringBuilder();

            sb.Append($"Filename={Path.Combine(path)};");

            if (!string.IsNullOrEmpty(password))
            {
                sb.Append($"Password={password};");
            }

            sb.Append("Upgrade=false;");

            return sb.ToString();
        }

        public ProLogLiteDB(string path, string password = "")
        {
            Console.WriteLine("[ProLogger]");
            TicToc.Tic("");

            db = new LiteDatabase(ComposeStringConnection(path, password));
            db.CheckpointSize = 100;

            Collection = db.GetCollection<LogItem>();

            Collection.EnsureIndex(x => x.LogId);
            Collection.EnsureIndex(x => x.LogId.GuidSession);
            Collection.EnsureIndex(x => x.LogId.Counter);
            Collection.EnsureIndex("DISTINCT(*.LogId.GuidSession)");
            Collection.EnsureIndex("MIN(*.LogId.Counter)");
            Collection.EnsureIndex("MAX(*.LogId.Counter)");
            Collection.EnsureIndex("MIN(*.Timestamp)");
            Collection.EnsureIndex("MAX(*.Timestamp)");
            Collection.EnsureIndex(x => x.Timestamp);
            Collection.EnsureIndex(x => x.LogType);

            db.Checkpoint();

            task = Task.Run(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;

                TaskLogHandler(cancellationTokenSource.Token);
            });

            IsRunning = true;

            TicToc.Toc();
        }

        public bool RemoveOldLogs(TimeSpan timeSpan)
        {
#if LOG_DISABLED
            return true;
#else
            try
            {
                db.BeginTrans();
                {
                    Collection.DeleteMany(x => x.Timestamp < (DateTime.UtcNow.Date - timeSpan));
                }
                db.Commit();
            }
            catch
            {
                return false;
            }

            return true;
#endif
        }

        private void TaskLogHandler(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                while (!queue.IsCompleted)
                {
                    LogItem item;

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

        private void AddLogInternal(LogItem item)
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
#if REPLACE_LOG_ITEM_GUID_SESSION
                item.LogId.GuidSession = CurrentGuidSession;
#endif
                item.LogId.Counter = ++MessagesLogged;

                Collection.Insert(item);
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

        public void AddLog(LogItem logItem)
        {
            if (logItem is null)
            {
                return;
            }
#if LOG_DISABLED
            return;
#else
            AddLog(logItem.Log, logItem.LogType);
#endif
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

            queue.Add(new LogItem(log, logType));
#endif
        }

        public IEnumerable<LogItem> GetSessionLogs(Guid guidSession, LogType logType = LogType.All, int limit = LimitResults)
        {
#if LOG_DISABLED
            return new List<LogItem>();
#else
            var results = Collection.Query()
                .Where(x => x.LogId.GuidSession == guidSession)
                .OrderByDescending(x => x.LogId.Counter)
                .Limit(LimitResults)
                .ToEnumerable()
                .Reverse();

            if (results != null && logType != LogType.All)
            {
                results = results.Where(x => x.LogType == logType);
            }

            return results;
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

            db.Checkpoint();
            Console.WriteLine($"[ProLogger] CheckPoint()");

            db.Dispose();
            Console.WriteLine($"[ProLogger] Disposed (Message logged: {MessagesLogged} Elements discarted: {ElementDiscarded} Errors: {Errors})");
        }


    }
}
