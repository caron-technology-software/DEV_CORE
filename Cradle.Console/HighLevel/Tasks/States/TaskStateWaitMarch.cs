using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.StateMachine;

using Machine.UI.Controls;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        public void TaskStateWaitMarch(CancellationToken cancellationToken)
        {
            //-------------------------------------
            // RegisterCurrentThread
            //-------------------------------------
            while (StateMachine is null)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            bool forceCloseMachineNotInMarchMessage = false;

            MachineMessageInfoFullScreenWithChecker messageBox = null;

            StateMachineHelper.RegisterCurrentThread(StateMachine, Thread.CurrentThread);

            //-------------------------------------
            // Task
            //-------------------------------------
            ProConsole.WriteLine("[ENTERING] TaskStateWaitMarch", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

            DateTime lastSendMarchTimestamp = DateTime.MinValue;

            try
            {
                StateMachine.WaitMarchSubState = WaitMarchSubState.WaitUI;

                //GPIx243
                int onlyOneTime = 0;
                //GPFx243

                while (!cancellationToken.IsCancellationRequested)
                {
                    bool marchEnabled = LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled];

                    switch (StateMachine.WaitMarchSubState)
                    {
                        case WaitMarchSubState.WaitUI:
                            {
                                if (HighLevel.Signals.UI)
                                {
                                    StateMachine.WaitMarchSubState = WaitMarchSubState.SendMarch;
                                }
                            }
                            break;

                        case WaitMarchSubState.SendMarch:
                            {
                                Console.WriteLine($"[TaskWaitMarch] SendCommand(Command.EnableMarch)");

                                if (!LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitDancer])
                                {
                                    //Communicator.SendCommand(Command.EnableMarch, 0x01);
                                    //GPIx243
                                    //if (((HighLevel.Settings.MachineEnduranceLimits.WorkingHours.WorkingFakeHours > 0.1)
                                    //    && (HighLevel.MachineEndurance.WorkingHours.WorkingFakeHours > HighLevel.Settings.MachineEnduranceLimits.WorkingHours.WorkingFakeHours)))
                                    if (((HighLevel.Settings.HighLevel.EnduranceLimits.WorkingHours.WorkingFakeHours > 0.1)
                                        && (HighLevel.MachineEndurance.WorkingHours.WorkingFakeHours > HighLevel.Settings.HighLevel.EnduranceLimits.WorkingHours.WorkingFakeHours)))
                                    {
                                        //onlyOneTime++;
                                        //if (onlyOneTime == 1)
                                        //{
                                        //    messageBox = new MachineMessageInfoFullScreenWithChecker(
                                        //                Localization.Warning, Localization.EthercatCodeError + " Code: " + MachineController.EthercutCodeError.ToString(),
                                        //                Checker2, TimeSpan.FromMilliseconds(10),
                                        //                ActionClicks);

                                        //    messageBox.Show();
                                        //}

                                        Console.WriteLine("[ETHERCAT ERROR]");
                                        HighLevel.Errors.EtherCat = true;

                                        HighLevel.MachineEndurance.Statistics.EthercatCode = (uint)MachineController.EthercutCodeError; // 135;
                                    }
                                    else
                                    {
                                        Communicator.SendCommand(Command.EnableMarch, 0x01);
                                    }
                                    //GPFx243
                                }
                                StateMachine.WaitMarchSubState = WaitMarchSubState.WaitMarch;
                            }
                            break;

                        case WaitMarchSubState.WaitMarch:
                            {
                                if (marchEnabled)
                                {
                                    Console.WriteLine($"[TaskWaitMarch] marchEnabled");

                                    StateMachine.WaitMarchSubState = WaitMarchSubState.WaitExit;

                                    Devices.Cradle.SetLowLevelWorkingState();

                                    StateMachine.SetStateFromTask(ControlState.Normal);
                                }
                                else
                                {
                                    if (LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.MarchEnabled] == false)
                                    {
                                        if ((DateTime.UtcNow - lastSendMarchTimestamp) > TimeSpan.FromMilliseconds(100))
                                        {
                                            if (!LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitDancer])
                                            {
                                                //Communicator.SendCommand(Command.EnableMarch, 0x01);
                                                //GPIx243
                                                //if (((HighLevel.Settings.MachineEnduranceLimits.WorkingHours.WorkingFakeHours > 0.1)
                                                //    && (HighLevel.MachineEndurance.WorkingHours.WorkingFakeHours > HighLevel.Settings.MachineEnduranceLimits.WorkingHours.WorkingFakeHours)))
                                                if (((HighLevel.Settings.HighLevel.EnduranceLimits.WorkingHours.WorkingFakeHours > 0.1)
                                                    && (HighLevel.MachineEndurance.WorkingHours.WorkingFakeHours > HighLevel.Settings.HighLevel.EnduranceLimits.WorkingHours.WorkingFakeHours)))
                                                {
                                                    //onlyOneTime++;
                                                    //if (onlyOneTime == 1)
                                                    //{
                                                    //    messageBox = new MachineMessageInfoFullScreenWithChecker(
                                                    //                Localization.Warning, Localization.EthercatCodeError + " Code: " + MachineController.EthercutCodeError.ToString(),
                                                    //                Checker2, TimeSpan.FromMilliseconds(10),
                                                    //                ActionClicks);

                                                    //    messageBox.Show();
                                                    //}

                                                    Console.WriteLine("[ETHERCAT ERROR]");
                                                    HighLevel.Errors.EtherCat = true;

                                                    HighLevel.MachineEndurance.Statistics.EthercatCode = (uint)MachineController.EthercutCodeError; // 135;
                                                }
                                                else
                                                {
                                                    Communicator.SendCommand(Command.EnableMarch, 0x01);
                                                }
                                                //GPFx243
                                            }
                                            lastSendMarchTimestamp = DateTime.UtcNow;
                                        }
                                    }
                                }
                            }
                            break;

                        case WaitMarchSubState.WaitExit:
                            {
                                //--
                            }
                            break;
                    }

                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);

                } //while(true)
            }
            catch
            {
                Console.WriteLine("[ERROR ON WAIT MARCH]");
            }

            ProConsole.WriteLine("[EXITING] TaskStateWaitMarch", ConsoleColor.Red);

            //---------------------------------
            // Internal functions
            //---------------------------------
            void ActionClicks()
            {
                forceCloseMachineNotInMarchMessage = true;
            }

            bool Checker()
            {
                bool condMarch = LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled];

                // In caso di emergenza, chiudo il popup per far apparire il messaggio di errore
                // relativo allo stato di emergenza
                bool emergency = HighLevel.Errors.EmergencyStatus;

                bool b01 = condMarch || forceCloseMachineNotInMarchMessage || emergency;
                if (forceCloseMachineNotInMarchMessage)
                {
                    forceCloseMachineNotInMarchMessage = false;
                }

                return (b01);
            }

            bool Checker2()
            {
                //bool condMarch = LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled];

                // In caso di emergenza, chiudo il popup per far apparire il messaggio di errore
                // relativo allo stato di emergenza
                bool emergency = HighLevel.Errors.EmergencyStatus;

                //return (condMarch || forceCloseMachineNotInMarchMessage || emergency);
                return (forceCloseMachineNotInMarchMessage || emergency);
            }

        } //task

    } //class
} //namespace