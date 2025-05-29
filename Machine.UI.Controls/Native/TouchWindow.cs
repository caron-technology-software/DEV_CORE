using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Machine.UI.Controls
{
    public class TouchWindow
    {
        [DllImport("user32.DLL")]
        public static extern bool RegisterTouchWindow(IntPtr hwnd, int ulFlags);

        [DllImport("user32.DLL")]
        public static extern bool UnregisterTouchWindow(IntPtr hwnd);
    }
}
