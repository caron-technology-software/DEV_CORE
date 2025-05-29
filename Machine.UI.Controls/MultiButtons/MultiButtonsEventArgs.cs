using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.UI.Controls
{
    public class MultiButtonsEventArgs : EventArgs
    {
        public int Value { get; set; }
        public MultiButtonsEventArgs(int index)
        {
            Value = index;
        }
    }
}
