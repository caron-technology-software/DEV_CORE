using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

using LiteDB;

using ProRob;
using ProRob.Threading;

using Caron.Cradle.Control.HighLevel;

namespace Caron.Cradle.Control
{
    public class DatabaseWorkings
    {
        private const int LimitResults = 1000;

        private static readonly Object locker = new Object();

        private static volatile Counter threadsInQueue = new Counter();

        private static volatile DatabaseWorkings instance;
        private static volatile LiteDatabase db;

        public static volatile bool IsInitialized = false;
        public static ILiteCollection<Working> WorkingsCollection;
        public static ILiteCollection<MachineEvent> MachineEventsCollection;

        static DatabaseWorkings()
        {
            var _ = new DatabaseWorkings();
        }

        public static void Close()
        {
            IsInitialized = false;

            Console.WriteLine($"[DatabaseWorkings] Waiting threadInWorks..");
            threadsInQueue.WaitValue(0);

            Console.WriteLine($"[DatabaseWorkings] Disposing database..");
            db.Checkpoint();
            db.Dispose();
        }

        private DatabaseWorkings()
        {
            if (IsInitialized)
            {
                return;
            }

            try
            {
                Console.WriteLine("[DatabaseWorkings] Opening databases..");
                db = new LiteDatabase(Constants.Path.Database.DatabaseWorkingsFile);


                Console.WriteLine("[DatabaseWorkings] Opening collections..");
                WorkingsCollection = db.GetCollection<Working>("Workings");
                MachineEventsCollection = db.GetCollection<MachineEvent>("MachineEvents");


                #region Indexes
                Console.WriteLine("[DatabaseWorkings] Creating indexes 1/2..");
                WorkingsCollection.EnsureIndex(x => x.Guid);
                WorkingsCollection.EnsureIndex(x => x.StartDateTime);
                WorkingsCollection.EnsureIndex(x => x.StartDateTime.Year);
                WorkingsCollection.EnsureIndex(x => x.StartDateTime.Month);

                WorkingsCollection.EnsureIndex(x => x.StopDateTime);
                WorkingsCollection.EnsureIndex(x => x.StopDateTime.Year);
                WorkingsCollection.EnsureIndex(x => x.StopDateTime.Month);

                WorkingsCollection.EnsureIndex(x => x.WorkingName);
                WorkingsCollection.EnsureIndex(x => x.TotalTimeCounter);
                WorkingsCollection.EnsureIndex(x => x.Material);
                WorkingsCollection.EnsureIndex(x => x.MaterialCode);
                WorkingsCollection.EnsureIndex(x => x.MaterialSpread);

                Console.WriteLine("[DatabaseWorkings] Creating indexes 2/2..");
                MachineEventsCollection.EnsureIndex(x => x.GuidSession);
                MachineEventsCollection.EnsureIndex(x => x.Timestamp);
                MachineEventsCollection.EnsureIndex(x => x.EventType);
                #endregion
            }
            catch
            {
                ProConsole.WriteLine("[DatabaseWorkings] Initialization failed", ConsoleColor.Cyan);
                return;
            }

            ProConsole.WriteLine("[DatabaseWorkings] Initialization completed", ConsoleColor.Cyan);

            IsInitialized = true;
        }

        public static DatabaseWorkings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new DatabaseWorkings();
                        }
                    }
                }

                return instance;
            }
        }

        public static void Add(MachineEvent machineEvent)
        {
            if (!IsInitialized)
            {
                return;
            }

            threadsInQueue.Increment();
            Task.Run(() =>
            {
                try
                {
                    MachineEventsCollection.Insert(machineEvent);
                }
                finally
                {
                    threadsInQueue.Decrement();
                }
            });
        }

        public static void Add(Working working)
        {
            if (!IsInitialized)
            {
                return;
            }

            threadsInQueue.Increment();
            Task.Run(() =>
            {
                try
                {
                    WorkingsCollection.Insert(working);
                }
                finally
                {
                    threadsInQueue.Decrement();
                }
            });
        }

        public static int GetCount()
        {
            if (!IsInitialized)
            {
                return -1;
            }

            return WorkingsCollection.Count();
        }

        public static IEnumerable<Working> GetList()
        {
            if (!IsInitialized)
            {
                return null;
            }

            return WorkingsCollection.Query().Limit(LimitResults).ToEnumerable();
        }
    }

    #region DatabaseWorkingsBuilder
#if TEST
    public static class DatabaseWorkingsBuilder
    {
        public static void CreateTestDatabase()
        {
            var rnd = new Random();
            var startDate = DateTime.Now;

            int n = 10_000;
            var list = new List<Working>(n);

            TicToc.Tic("Populate list");
            for (int i = 0; i < n; i++)
            {
                startDate += TimeSpan.FromSeconds(rnd.NextDouble() * 3600);
                var stopDate = startDate + TimeSpan.FromSeconds(rnd.NextDouble() * 3600);

                var totalTimeCounter = TimeSpan.FromMilliseconds((stopDate - startDate).TotalMilliseconds * 0.55);

                var working = new Working()
                {
                    Guid = Guid.NewGuid(),
                    StartDateTime = startDate,
                    StopDateTime = stopDate,
                    WorkingName = $"LAV_{i}",
                    Material = $"MATERIAL_{rnd.Next(3)}",
                    MaterialCode = $"MATERIAL_CODE_{rnd.Next(50)}",
                    MaterialSpread = rnd.NextDouble() * 500.0,
                    TotalTimeCounter = totalTimeCounter,
                    TotalCradleInSyncAndInMovementTimeCounter = TimeSpan.FromMilliseconds(totalTimeCounter.TotalMilliseconds * 0.25),
                    TotalTimeLoadUnloadCounter = TimeSpan.FromMilliseconds(totalTimeCounter.TotalMilliseconds * 0.05),
                };

                startDate = stopDate;

                list.Add(working);
            }
            TicToc.Toc();

            if (File.Exists(Constants.Path.Database.DatabaseWorkingsFile))
            {
                File.Delete(Constants.Path.Database.DatabaseWorkingsFile);
            }

            using var db = new LiteDB.LiteDatabase(Constants.Path.Database.DatabaseWorkingsFile);

            var workings = db.GetCollection<Working>("Workings");

            TicToc.Tic("InsertBulk");

            db.BeginTrans();

            workings.InsertBulk(list);

            db.Commit();
            TicToc.Toc();

            try
            {
                workings.EnsureIndex(x => x.Guid);
                workings.EnsureIndex(x => x.StartDateTime);
                workings.EnsureIndex(x => x.StartDateTime.Year);
                workings.EnsureIndex(x => x.StartDateTime.Month);
                workings.EnsureIndex(x => x.StartDateTime.Day);

                workings.EnsureIndex(x => x.StopDateTime);
                workings.EnsureIndex(x => x.StopDateTime.Year);
                workings.EnsureIndex(x => x.StopDateTime.Month);
                workings.EnsureIndex(x => x.StopDateTime.Day);

                workings.EnsureIndex(x => x.WorkingName);
                workings.EnsureIndex(x => x.TotalTimeCounter);
                workings.EnsureIndex(x => x.Material);
                workings.EnsureIndex(x => x.MaterialSpread);
            }
            catch
            {
                //--
            }
        }
    }
#endif
    #endregion
}


