using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.UI.Controls
{
    public class IndexEventArgs : EventArgs
    {
        public int Index { get; set; }

        public IndexEventArgs(int index)
        {
            Index = index;
        }
    }
}
