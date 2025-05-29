using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ProRob;

using Caron.Cradle.Control.HighLevel.StateMachine;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        public void TaskStateLoadUnload(CancellationToken cancellationToken)
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
            ProConsole.WriteLine("[ENTERING] TaskStateLoadUnload", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

            while (!cancellationToken.IsCancellationRequested)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);

            } //while()

            ProConsole.WriteLine("[EXITING] TaskStateLoadUnload", ConsoleColor.Red);

        } //task

    } //class
} //namespace