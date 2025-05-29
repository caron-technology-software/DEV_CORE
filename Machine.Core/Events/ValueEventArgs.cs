using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.Events
{
    public class ValueEventArgs<T> : EventArgs
    {
        public T Value { get; set; }

        public ValueEventArgs(T value)
        {
            Value = value;
        }
    }
}
