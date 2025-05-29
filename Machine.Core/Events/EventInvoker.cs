using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Machine.Utility;

namespace Machine.Events
{
    public class EventInvoker<T>
    {
        public bool IsObjectChanged { get => checker.IsChanged; }
        public DateTime LastChange { get; private set; } = DateTime.MinValue;

        private ObjectChangedChecker<T> checker;
        private TimeSpan minInterval;

        private readonly DateTime lastCall = DateTime.MinValue;
        private TimeSpan minIntervalFromCheckCalls;

        public EventInvoker(TimeSpan minIntervalEventInvoke)
        {
            this.checker = new ObjectChangedChecker<T>();
            this.minInterval = minIntervalEventInvoke;
            this.minIntervalFromCheckCalls = minIntervalEventInvoke;
        }

        //[IMPROVE]
        public void InvokeEventIfObjectChanged(T obj, EventHandler eventHandler = null)
        {
            /*if ((DateTime.Now - lastCall) < minInterval)
            {
                lastCall = DateTime.Now;

                return;
            }*/

            checker.Check(obj);

            if (checker.IsChanged)
            {
                //[FIX]->insert a timer
                if (true)//((DateTime.Now - LastChange) > minInterval)
                {
                    if (eventHandler != null)
                    {
                        eventHandler.Invoke(this, new EventArgs());
                    }

                    LastChange = DateTime.Now;
                }
            }
        }
    }
}

