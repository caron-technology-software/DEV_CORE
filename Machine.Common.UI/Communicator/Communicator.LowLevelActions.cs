using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace Machine.UI.Communication
{
    public partial class Communicator
    {
        public static void SendLowLevelControlCommand(string command)
        {
            Communicator.SendHttpGetRequest("command", command);
        }
    }
}