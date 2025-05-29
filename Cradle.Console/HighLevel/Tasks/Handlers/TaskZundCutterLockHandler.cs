using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

using Machine.Utility;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.StateMachine;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        public void TaskZundCutterLockHandler(CancellationToken cancellationToken)
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
            ProConsole.WriteLine("[ENTERING] TaskZundCutterLockHandler", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
            ThreadsStarted++;

            //-------------------------------------------------------
            // Inputs/outputs
            //-------------------------------------------------------
            bool enableCutterCradleLock = false;
            bool precEnableCutterCradleLock = false;

            bool marchEnabledMachine = false;
            bool precMarchEnabledMachine = false;

            //-------------------------------------------------------
            // Cycle
            //-------------------------------------------------------
            while (!cancellationToken.IsCancellationRequested)
            {
                //GPIx101 3) command che mi setta la variabile di basso livello e "Check_Photocell_Roll_Presence"  
                //           in base a "EnableFunctionPhotocellRollPresence" e "EnablePhotocellRollPresence" nel workingstate corrente.
                //////////if (HighLevel.Settings.HighLevel.FunctionsEnabled.EnableFunctionPhotocellRollPresence.Value)
                //////////{
                //////////    if (HighLevel.WorkingContext.Parameters.EnablePhotocellRollPresence)
                //////////    {
                //////////        if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellRollPresence] == false)
                //////////        {
                //////////            if (LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.CradleCutterLock] == false)
                //////////            {
                //////////                Communicator.SendCommand(Command.CradleCutterLock, true);
                //////////            }
                //////////        }
                //////////        else
                //////////        {
                //////////            if (LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.CradleCutterLock] == true)
                //////////            {
                //////////                Communicator.SendCommand(Command.CradleCutterLock, false);
                //////////            }
                //////////        }
                //////////    }
                //////////}
                //GPFx101

                //-------------------------------------------------------
                // Updates
                //-------------------------------------------------------
                enableCutterCradleLock = HighLevel.Settings.HighLevel.MachineParameters.EnableCutterCradleLock.Value;
                marchEnabledMachine = LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled];

                //-------------------------------------------------------
                // Init
                //-------------------------------------------------------
                if (enableCutterCradleLock == false && precEnableCutterCradleLock == true)
                {
                    if (LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.CradleCutterLock] == true)
                    {
                        Communicator.SendCommand(Command.CradleCutterLock, false);
                    }
                }

                //-------------------------------------------------------
                // Enable Cutter Cradle Lock
                //-------------------------------------------------------
                if (enableCutterCradleLock)
                {
                    //-------------------------------------------------------
                    // March
                    //-------------------------------------------------------
                    if (marchEnabledMachine == false)
                    {
                        if (LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.CradleCutterLock] == false)
                        {
                            Communicator.SendCommand(Command.CradleCutterLock, true);
                        }
                    }
                    else
                    {
                        //-------------------------------------------------------
                        // Cutter
                        //-------------------------------------------------------
                        //GPIx101 3) command che mi setta la variabile di basso livello e "Check_Photocell_Roll_Presence"  
                        //           in base a "EnableFunctionPhotocellRollPresence" e "EnablePhotocellRollPresence" nel workingstate corrente.
                        bool cond = (HighLevel.Status.HighLevelControlState == ControlState.CutOff ||
                                     HighLevel.Status.HighLevelControlState == ControlState.Sharpening ||
                                     LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitCutterOperatorSide] == false);

                        bool cond2 = ( HighLevel.Settings.HighLevel.FunctionsEnabled.EnableFunctionPhotocellRollPresence.Value &&
                                       HighLevel.WorkingContext.Parameters.EnablePhotocellRollPresence &&
                                       //LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellRollPresence] == false);
                                       HighLevel.Status.PhotocelRollPresenceFiltered == false);

                        //////////if (cond)
                        if (cond || cond2)
                        //GPFx101
                        {
                            if (LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.CradleCutterLock] == false)
                            {
                                Communicator.SendCommand(Command.CradleCutterLock, true);
                                Console.WriteLine($"Communicator.SendCommand(Command.CradleCutterLock, true); LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.CradleCutterLock]: {LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.CradleCutterLock]} LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellRollPresence]: {LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellRollPresence]}");
                            }
                        }
                        else
                        {
                            if (LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.CradleCutterLock] == true)
                            {
                                Communicator.SendCommand(Command.CradleCutterLock, false);
                                Console.WriteLine($"Communicator.SendCommand(Command.CradleCutterLock, false); LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.CradleCutterLock]: {LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.CradleCutterLock]} LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellRollPresence]: {LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellRollPresence]}");
                            }
                        }
                    }
                }

                //-------------------------------------------------------
                // Precedent inputs/outputs
                //-------------------------------------------------------
                precEnableCutterCradleLock = enableCutterCradleLock;
                precMarchEnabledMachine = marchEnabledMachine;

                //-------------------------------------------------------
                // Delay
                //-------------------------------------------------------
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            ProConsole.WriteLine("[EXITING] TaskZundCutterLockHandler", ConsoleColor.Red);
        }
    }
}