using System;

namespace Machine.DataCollections
{
    public class MachineDataElement<T>
    {
        public DateTime Timestamp { get; set; }
        public T Value { get; set; }

        public MachineDataElement()
        {
            //--
        }
    }
}
