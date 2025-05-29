using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProRob.Multimedia
{
    public static class NewBeep
    {
        private static volatile bool isPlaying;

        public static bool IsPlaying { get => isPlaying; }

        static NewBeep()
        {
            isPlaying = false;

            Task.Run(() =>
            {
                while (true)
                {
                    if (isPlaying)
                    {
                        Console.Beep();
                    }

                    Thread.Sleep(10);
                }
            });
        }

        public static void Start()
        {
            isPlaying = true;
        }

        public static void Stop()
        {
            isPlaying = false;
        }
    }


    public static class Beep
    {
        public class BeepSettings
        {
            public int Reps { get; set; }
            public int Delay { get; set; }

            public BeepSettings()
            {
                Reps = 1;
                Delay = Beep.Delay;
            }

            public BeepSettings(int reps)
            {
                Reps = reps;
                Delay = Beep.Delay;
            }

            public BeepSettings(int reps, int delay)
            {
                Reps = reps;
                Delay = delay;
            }
        }

        private static ConcurrentQueue<BeepSettings> queue = new ConcurrentQueue<BeepSettings>();
        private static Task task;

        private const int Frequency = 5000; //[Hz]
        private const int Delay = 10; //[ms]

        static Beep()
        {
            task = Task.Run(() => { BeepBackgroundTask(); });
        }

        public static void Play(int reps, int delay)
        {
            queue.Enqueue(new BeepSettings() { Reps = reps, Delay = delay });
        }

        public static void Play(BeepSettings beep)
        {
            queue.Enqueue(beep);
        }

        public static void Play()
        {
            queue.Enqueue(new BeepSettings());
        }

        private static void BeepBackgroundTask()
        {
            while (true)
            {
                while (queue.Count < 1)
                {
                    Thread.Sleep(1);
                }

                if (queue.Count > 1)
                {
                    while (queue.Count != 1)
                    {
                        queue.TryDequeue(out BeepSettings beepTodelete);
                    }
                }

                if (queue.TryDequeue(out BeepSettings beep))
                {

                    for (int i = 0; i < beep.Reps - 1; i++)
                    {
                        Console.Beep(Frequency, beep.Delay);
                        Thread.Sleep(beep.Delay);
                    }
                    Console.Beep(Frequency, beep.Delay);
                }
            }

        }
    }
}