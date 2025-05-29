using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Machine.UI
{
    public class Win32Api
    {
        internal const int AW_HIDE = 0X10000;
        internal const int AW_ACTIVATE = 0X20000;
        internal const int AW_HOR_POSITIVE = 0X1;
        internal const int AW_HOR_NEGATIVE = 0X2;
        internal const int AW_SLIDE = 0X40000;
        internal const int AW_BLEND = 0X80000;
        internal const int WM_NCLBUTTONDOWN = 0xA1;
        internal const int HT_CAPTION = 0x2;
        internal const int WM_CLOSE = 0x10;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int AnimateWindow(IntPtr hwand, int dwTime, int dwFlags);

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hwnd, bool fUnknown);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public static int SendMessageMoveForm(IntPtr handle)
        {
            ReleaseCapture();
            int messageCode = SendMessage(handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            return messageCode;
        }

        public static void SendMessageCloseWindow(IntPtr hWindow)
        {
            SendMessage(hWindow, WM_CLOSE, 0, 0);
        }
    }
}
