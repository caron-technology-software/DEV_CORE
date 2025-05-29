using System;
using System.Linq;
using System.Text;

namespace Machine.UI.Controls
{
    public class PropertyEventArgs : EventArgs
    {
        public int Value;

        public PropertyEventArgs(int value)
        {
            Value = value;
        }
    }
}
