using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using ProRob;

namespace Machine.UI.Communication
{
    public partial class Communicator
    {
        public static void SetHighLevelControlState(string controlState)
        {
            ProConsole.WriteLine($"[API] SetHighLevelControlState({controlState})", ConsoleColor.Yellow);

            string ret = Communicator.SetVariable("state_machine", "state", controlState);

            Console.WriteLine($"[API] Communicator.SetHighLevelControlState({ret})");
        }
    }
}

