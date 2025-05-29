using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Machine.UI.Communication
{
    public partial class Communicator
    {
        public static void ShowMessageBox(string text, bool simulateStopButton = true)
        {
            if (simulateStopButton)
            {
                Communicator.SetVariable("message_box", "show_message", "message", text);
            }
            else
            {
                Communicator.SetVariable("message_box", "show_message_no_stop", "message", text);
            }         
        }
    }
}
