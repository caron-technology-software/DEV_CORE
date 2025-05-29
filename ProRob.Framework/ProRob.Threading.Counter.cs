using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProRob.Threading
{
    //[TODO], [INFO], [IMPROVE] le operazioni elementi a 32 bit(es. int) dovrebbero essere atomiche
    public class Counter
    {
        private long counter = 0;
        public int CurrentCount => (int)Interlocked.Read(ref counter);

        public Counter()
        {
            //--
        }

        public void Increment()
        {
            Interlocked.Increment(ref counter);
        }

        public void Decrement()
        {
            Interlocked.Decrement(ref counter);
        }

        public void Set(int value)
        {
            Interlocked.Exchange(ref counter, value);
        }

        public void Reset()
        {
            Set(0);
        }

        public void WaitValue(int value)
        {
            while (Interlocked.Read(ref counter) != value)
            {
                Thread.Sleep(1);
            }
        }
    }
}
