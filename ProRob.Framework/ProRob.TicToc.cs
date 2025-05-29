using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob
{
    public static class TicToc
    {
        [ThreadStatic]
        private static System.Diagnostics.Stopwatch sw;

        [ThreadStatic]
        private static string text = string.Empty;

        public static void Tic(string textToPrintOnToc = "")
        {
            text = textToPrintOnToc;

            if (sw is null)
            {
                sw = new System.Diagnostics.Stopwatch();
            }

            if (sw.IsRunning)
            {
                sw.Restart();
            }
            else
            {
                sw.Start();
            }
        }

        public static void Restart()
        {
            Tic();
        }

        public static long Toc()
        {
            //sw.Stop();
            if (sw is null)
            {
                return 0;
            }

            long elapsedMilliseconds = sw.ElapsedMilliseconds;

            if (string.IsNullOrEmpty(text))
            {
                ProConsole.WriteLine($"Elapsed time: {elapsedMilliseconds} ms");
            }
            else
            {
                ProConsole.WriteLine($"[{text}] - Elapsed time: {elapsedMilliseconds} ms");
            }

            return elapsedMilliseconds;
        }

        public static TimeSpan GetTimeSpan()
        {
            if (sw is null)
            {
                return TimeSpan.MinValue;
            }

            return TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds);
        }
    }
}
