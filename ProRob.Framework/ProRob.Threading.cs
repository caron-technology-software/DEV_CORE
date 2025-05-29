using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace ProRob.Threading
{
    public class ThreadConfiguration
    {
        public ThreadStart ThreadStart { get; set; }
        public ThreadPriority ThreadPriority { get; set; }
        public ThreadConfiguration(ThreadStart threadStart, ThreadPriority threadPriority = ThreadPriority.Normal)
        {
            ThreadStart = threadStart;
            ThreadPriority = threadPriority;
        }
    }

    public class ThreadDispatcher
    {
        private System.Threading.Thread[] threads;

        public bool IsStarted = false;

        public ThreadDispatcher(ThreadConfiguration[] threadsData)
        {
            threads = new System.Threading.Thread[threadsData.Length];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new System.Threading.Thread(threadsData[i].ThreadStart);
                threads[i].Priority = threadsData[i].ThreadPriority;
            }
        }

        public bool WaitWithTimeout(TimeSpan timeout)
        {
            var sw = new Stopwatch();
            sw.Start();

            while (sw.Elapsed < timeout && AnyAlive())
            {
                Thread.Sleep(1);
            }

            return AnyAlive();
        }

        public bool AnyAlive()
        {
            foreach (var t in threads)
            {
                if (t.IsAlive)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Start()
        {
            if (IsStarted)
            {
                return false;
            }

            foreach (var t in threads)
            {
                t.Start();
            }

            IsStarted = true;
            return true;
        }

        public bool Stop()
        {
            try
            {
                if (!IsStarted)
                {
                    return false;
                }

                foreach (var t in threads)
                {
                    t.Abort();
                }

                foreach (var t in threads)
                {
                    while (t.ThreadState != System.Threading.ThreadState.Aborted)
                    {
                        ;
                    }
                }

                IsStarted = false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}