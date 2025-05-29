using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

using Caron.Cradle.Control.HighLevel.StateMachine;
using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        public void TaskStateEmergency(CancellationToken cancellationToken)
        {
            //-------------------------------------
            // RegisterCurrentThread
            //-------------------------------------
            while (StateMachine is null)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            StateMachineHelper.RegisterCurrentThread(StateMachine, Thread.CurrentThread);

            //-------------------------------------
            // Task
            //-------------------------------------
            ProConsole.WriteLine("[ENTERING] TaskStateEmergency", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

            StateMachine.EmergencySubstate = EmergencySubState.Emergency;

            while (!cancellationToken.IsCancellationRequested)
            {
                //--------------------------------
                // DO NOTHING
                //--------------------------------

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            ProConsole.WriteLine("[EXITING] TaskStateEmergency", ConsoleColor.Red);
        }
    }
}