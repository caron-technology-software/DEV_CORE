using Machine.Control.HighLevel;
using ProRob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caron.Cradle.UI
{
    public class UIFunctions
    {
        private Supervisor Supervisor { get; set; }

        public UIFunctions(Supervisor supervisor)
        {
            Supervisor = supervisor;
        }

        public void Close()
        {
            ProConsole.WriteLine($"[{DateTime.Now}] Close()");

            Machine.UI.Communication.Communicator.SendHttpGetRequest("application/close");

            Supervisor.ShutdownUI();

            while (Supervisor.DeinitializationCompleted == false)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }
        }

        public void ShutdownMachine()
        {
            ProConsole.WriteLine($"[{DateTime.Now}] ShutdownMachine()");

            Machine.UI.Communication.Communicator.SendHttpGetRequest("application/shutdown");

            Supervisor.ShutdownUI();

            while (Supervisor.DeinitializationCompleted == false)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }
        }
    }
}
