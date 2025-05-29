using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Machine.UI.Controls
{
    public class StreamHID
    {
        private static readonly TimeSpan IntervalTimer = TimeSpan.FromMilliseconds(300);

        private readonly Object objLocker = new Object();

        private System.Timers.Timer timer = new System.Timers.Timer(IntervalTimer.TotalMilliseconds);

        private volatile string tmpData;

        private volatile string text;
        public string Text
        {
            get
            {
                lock (objLocker)
                {
                    return text;
                }
            }
            private set
            {
                lock (objLocker)
                {
                    text = value;
                }
            }
        }

        public StreamHID()
        {
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer elapsed");

            timer.Stop();

            lock (objLocker)
            {
                Text = tmpData;
                tmpData = string.Empty;

                //Console.WriteLine($"[StreamHID] {Text}");
            }
        }

        public void Reset()
        {
            lock (objLocker)
            {
                Text = string.Empty;
            }
        }

        public void AddChar(char character)
        {
            tmpData += character;

            //if (character.Equals('\\'))
            //{
            //    tmpData += character;
            //}

            timer.Enabled = true;
            timer.Start();
        }
    }
}
