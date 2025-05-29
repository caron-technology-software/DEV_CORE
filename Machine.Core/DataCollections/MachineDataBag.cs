using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using ProRob.Extensions.Object;

namespace Machine.DataCollections
{
    //[IMPROVE] Circular Buffer, usare int al posto di datetime
    public class MachineDataBag<T>
    {
        public DateTime LastUpdate { get; private set; } = DateTime.MinValue;
        public TimeSpan RefreshTime { get; private set; }
        public TimeSpan TimeWindowDataCollection { get; private set; }
        public int NumberOfElements { get; private set; } = 0;

        private DateTime dateTimeNow;

        private readonly object locker = new object();
        private List<MachineDataElement<T>> bag = new List<MachineDataElement<T>>();

        private readonly TimeSpan maintenanceInterval;
        private DateTime lastMaintenance = DateTime.MinValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ExecuteBagMaintenance()
        {
            if ((dateTimeNow - lastMaintenance) > maintenanceInterval)
            {
                lock (locker)
                {
                    bag = bag.Where(x => x.Timestamp > (dateTimeNow - TimeWindowDataCollection)).ToList();
                }

                lastMaintenance = dateTimeNow;
            }
        }

        public MachineDataBag(TimeSpan refreshTime, TimeSpan timeWindowDataCollection, int maintenanceInterval = 1000)
        {
            this.maintenanceInterval = TimeSpan.FromMilliseconds(maintenanceInterval);
            this.RefreshTime = refreshTime;
            this.TimeWindowDataCollection = timeWindowDataCollection;
            this.NumberOfElements = (int)(this.TimeWindowDataCollection.TotalMilliseconds / this.RefreshTime.TotalMilliseconds);

            Console.WriteLine($"[MachineDataBag] NumberOfElements:{this.NumberOfElements}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddData(DateTime timestamp, T value)
        {
            dateTimeNow = timestamp;

            lock (locker)
            {
                AddData(new MachineDataElement<T>() { Timestamp = dateTimeNow, Value = value });
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddData(T value)
        {
            dateTimeNow = DateTime.UtcNow;

            lock (locker)
            {
                AddData(new MachineDataElement<T>() { Timestamp = dateTimeNow, Value = value });
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddData(MachineDataElement<T> element)
        {
            if ((dateTimeNow - LastUpdate) > RefreshTime)
            {
                LastUpdate = dateTimeNow;

                lock (locker)
                {
                    bag.Add(element.Clone());
                }

                ExecuteBagMaintenance();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<MachineDataElement<T>> GetData()
        {
            dateTimeNow = DateTime.UtcNow;

            var newBag = new List<MachineDataElement<T>>();

            lock (locker)
            {
                newBag.AddRange(bag.Where(x => x.Timestamp > (dateTimeNow - TimeWindowDataCollection)).ToList());
            }

            return newBag;
        }

        public void Reset()
        {
            lock (locker)
            {
                bag.Clear();
            }
        }
    }
}