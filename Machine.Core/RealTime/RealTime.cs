#undef DEBUG_ELAPSED_TIME

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

namespace Machine.RealTime
{
    [Synchronization()]
    public static class RealTime
    {
        #region TEST TIMER
        ////Task.Run(() =>
        ////{
        ////    Thread.CurrentThread.Priority = ThreadPriority.Highest;

        ////    while(HighLevel.Signals.ControlReady==false)
        ////    {
        ////        Thread.Sleep(10);
        ////    }

        ////    Thread.Sleep(2000);

        ////    Console.WriteLine("Starting..");

        ////    int n = 1000*60*1;
        ////    var l = new List<double>();
        ////    l.Capacity = n * 2;
        ////    var t = new ProRob.MultimediaTimer();
        ////    t.Interval = 1;
        ////    t.Resolution = 0;

        ////    t.Elapsed += (o, e) =>
        ////    {
        ////        counter++;

        ////        l.Add(sw.Elapsed.TotalMilliseconds);

        ////        if (counter == n)
        ////        {
        ////            t.Stop();
        ////        }
        ////    };

        ////    sw.Start();
        ////    Thread.Sleep(10);
        ////    t.Start();

        ////    Task.Run(() =>
        ////    {
        ////        while (t.IsRunning)
        ////        {
        ////            Thread.Sleep(1000);
        ////        }

        ////        var diff = l.Skip(1).Zip(l, (cur, prev) => cur - prev);

        ////        //foreach (var d in diff)
        ////        //{
        ////        //    Console.WriteLine(d);
        ////        //}


        ////        Console.WriteLine($"min:{diff.Min():0.000} max:{diff.Max():0.000} mean:{diff.Average():0.00000} sd:{GetStandardDeviation(diff):0.00000}");

        ////    });
        ////});
        #endregion

        #region TEST TIMER
        ////volatile int counter = 0;
        ////Stopwatch sw = new Stopwatch();
        ////public static double GetStandardDeviation(IEnumerable<double> values)
        ////{
        ////    double standardDeviation = 0;
        ////    double[] enumerable = values as double[] ?? values.ToArray();
        ////    int count = enumerable.Count();
        ////    if (count > 1)
        ////    {
        ////        double avg = enumerable.Average();
        ////        double sum = enumerable.Sum(d => (d - avg) * (d - avg));
        ////        standardDeviation = Math.Sqrt(sum / count);
        ////    }
        ////    return standardDeviation;
        ////}
        #endregion

        [Synchronization()]
        public static class HighLevel
        {
            private static volatile object locker = new object();

            public static void SetEvent()
            {
                lock (locker)
                {
                    // All waiting threads will resume once we release valueLock
                    Monitor.PulseAll(locker);
                }
            }

            public static void Wait()
            {
#if TEST
                Thread.Sleep(Constants.Intervals.HighLevelControlCycle);
#else
                lock (locker)
                {
                    Monitor.Wait(locker);
                }
#endif
            }

            public static void Sleep(int milliseconds)
            {
#if DEBUG_ELAPSED_TIME
                var sw = new Stopwatch();
                sw.Start();
#endif
                if (milliseconds <= 0)
                {
                    return;
                }

                for (int i = 0; i < milliseconds; i++)
                {
                    Wait();
                }
#if DEBUG_ELAPSED_TIME
                sw.Stop();

                ProConsole.WriteLine($"[RealTime] Elapsed time:{sw.ElapsedMilliseconds} ms");
#endif
            }

            public static void Sleep(TimeSpan timespan)
            {
                Sleep((int)timespan.TotalMilliseconds);
            }
        }


        [Synchronization()]
        public static class LowLevel
        {
            private static volatile object locker = new object();

            public static void SetEvent()
            {
                lock (locker)
                {
                    // All waiting threads will resume once we release valueLock
                    Monitor.PulseAll(locker);
                }
            }

            public static void Wait()
            {
#if TEST
                Thread.Sleep(Constants.Intervals.HighLevelControlCycle);
#else
                lock (locker)
                {
                    Monitor.Wait(locker);
                }
#endif
            }
        }
    }
}
