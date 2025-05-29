using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Machine.UI.Controls
{
    class NativeMethods
    {
        public static bool SetScrollBar(IntPtr handle, ScrollBarDirection scrollBarDirection, bool show)
        {
            return ShowScrollBar(handle, (int)scrollBarDirection, show);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);
    }
}
