using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel.StateMachine;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        public void TaskEmergencyHandler(CancellationToken cancellationToken)
        {
            //-------------------------------------
            // Wait State Machine
            //-------------------------------------
            while (StateMachine is null)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            //-------------------------------------
            // Task
            //-------------------------------------
            ProConsole.WriteLine("[ENTERING] TaskEmergencyHandler", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
            ThreadsStarted++;

            while (!cancellationToken.IsCancellationRequested)
            {
                //-------------------------------------------------------------
                // [WARNING]
                // - Attualmente i controlli di emergenza vengono effettuati 
                //   all'interno del task "TaskHighLevelControlStatusUpdate"
                //-------------------------------------------------------------

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            ProConsole.WriteLine("[EXITING] TaskEmergencyHandler", ConsoleColor.Red);
        }
    }
}