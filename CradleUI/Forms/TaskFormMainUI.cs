using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

using ProRob;
using ProRob.Extensions.Object;
using ProRob.Threading;

using Machine.Events;
using Machine.UI.Communication;

using Caron.Cradle.Control;
using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel;

namespace Caron.Cradle.UI
{
    public partial class FormMain
    {
        private void TaskFormMainUI()
        {
            ProConsole.WriteLine("[ENTERING] TaskFormMainUI", ConsoleColor.Green);

            bool emergency = false;
            bool precEmergency = false;

            while (Supervisor.IsRunning)
            {
                emergency = Supervisor.Control.HighLevel.Errors.EmergencyStatus;

                //---------------------------------
                // Logic
                //---------------------------------
                if (emergency && !precEmergency)
                {
                    Console.WriteLine($"[TaskUI] SetUIState(StateUI.Messages)");

                    this?.Invoke((MethodInvoker)delegate ()
                    {
                        Supervisor.SetUIState(StateUI.Messages);
                    });
                }

                precEmergency = emergency;

                Thread.Sleep(Machine.UI.Constants.Intervals.UpdateControlStatus);
            }

            ProConsole.WriteLine($"[EXITING] TaskFormMainUI", ConsoleColor.Red);
        }
    }
}
