//GPIx21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

using Machine.Common;
using Machine.RealTime;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel.StateMachine;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        public void TaskStateIOConfig(CancellationToken cancellationToken)
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
            ProConsole.WriteLine("[ENTERING] TaskStateIOConfig", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

            bool exitCondition = false;
            int cycles = 0;

            try
            {
                StateMachine.IOConfigSubState = IOConfigSubState.Running;

                while (exitCondition == false)
                {
                    RealTime.HighLevel.Wait();

                    switch (StateMachine.IOConfigSubState)
                    {
                        case IOConfigSubState.Running:
                            {
                                //--
                            }
                            break;

                        case IOConfigSubState.WaitExit:
                            {
                                //--   
                            }
                            break;
                    }

                    //-------------------------------------
                    // End cycle
                    //-------------------------------------  
                    if (cancellationToken.IsCancellationRequested)
                    {
                        exitCondition = true;
                    }

                    if ((cycles % 2500) == 0)
                    {
                        Console.WriteLine($"[TaskStateIOConfig]");
                    }

                    cycles++;

                } //while(true)
            }
            catch
            {
                Console.WriteLine("[ERROR ON TaskStateIOConfig]");
            }

            ProConsole.WriteLine("[EXITING] TaskStateIOConfig", ConsoleColor.Red);

        } //task
    } //class
} //namespace
//GPFx21
