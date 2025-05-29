using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ProRob;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel.StateMachine;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        public void TaskStateCradleJogManualOperations(CancellationToken cancellationToken)
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
            ProConsole.WriteLine("[ENTERING] TaskStateCradleJogManualOperations", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

#pragma warning disable CS0219
            const float MinimumVelocityToConsiderDeviceInMovement = Machine.Constants.Kinematics.MinVelocityToConsiderDeviceInMovement;
            const float MinimumVelocityToConsiderDeviceStationary = Machine.Constants.Kinematics.MaxVelocityToConsiderDeviceStationary;
            const int MaxNumberOfIterAtWaitExitCondition = 1000;
#pragma warning restore CS0219

            uint cycles = 0;

            float cradlePositionToStop = 0.0f;
            //GPI12 sostituzione check temporale su PhotocellMaterialPresence:
            //bool bol01 = false;
            //if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            //{
            //    bol01 = true;
            //}
            //else
            //{
            //    bol01 = false;
            //}
            //DateTime checkUntilPhotocellMaterialPresence = DateTime.MinValue;
            //checkUntilPhotocellMaterialPresence = DateTime.UtcNow + TimeSpan.FromMilliseconds(HighLevel.Settings.HighLevel.MachineParameters.CheckUntilPhotocellMaterialPresence);   //TimeSpan.FromMilliseconds(250) parametro da mettere nella Cradle per intervallo di check fotocellula presenza materiale
            ////GPI18
            //while ((DateTime.UtcNow < checkUntilPhotocellMaterialPresence) && (!MachineControllerApplication.NoInitCheckPhotocell))
            ////GPF18
            //{
            //    // code block to be executed
            //    if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            //    {
            //        bol01 = true;
            //    }
            //    else
            //    {
            //        bol01 = false;
            //        break;
            //    }
            //}
            //GPF12
            //GPI25
            //////bool materialPresentAtStart = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence];
            bool materialPresentAtStart = HighLevel.Status.PhotocelMaterialPresenceFiltered;
            //bool materialPresentAtStart = !bol01;
            //GPF25
            bool cutterPresence = HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value;

            TimeSpan minTimespanToRemainInMaterialSubstate = TimeSpan.FromSeconds(1);
            DateTime timestampStartMaterialSubState = DateTime.MaxValue;

            Console.WriteLine($"materialPresentAtStart:{materialPresentAtStart} ");
            Console.WriteLine($"JogState:{HighLevel.Status.JogState} ");

            //MMIx02
            bool pcpm = HighLevel.Status.PhotocelMaterialPresenceFiltered;
            bool pcpmEnabled = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
            bool prp = HighLevel.Status.PhotocelRollPresenceFiltered;
            bool prpEnabled = HighLevel.WorkingContext.Parameters.EnablePhotocellRollPresence && HighLevel.Settings.HighLevel.FunctionsEnabled.EnableFunctionPhotocellRollPresence.Value;//MMIx05
            //MMFx02

            //GPI12 sostituzione check temporale su PhotocellMaterialPresence:
            ////bool bol01 = false;
            //bol01 = false;
            //if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            //{
            //    bol01 = true;
            //}
            //else
            //{
            //    bol01 = false;
            //}
            ////DateTime checkUntilPhotocellMaterialPresence = DateTime.MinValue;
            //checkUntilPhotocellMaterialPresence = DateTime.MinValue;
            //checkUntilPhotocellMaterialPresence = DateTime.UtcNow + TimeSpan.FromMilliseconds(HighLevel.Settings.HighLevel.MachineParameters.CheckUntilPhotocellMaterialPresence);   //TimeSpan.FromMilliseconds(250) parametro da mettere nella Cradle per intervallo di check fotocellula presenza materiale
            ////GPI18
            //while ((DateTime.UtcNow < checkUntilPhotocellMaterialPresence) && (!MachineControllerApplication.NoInitCheckPhotocell))
            ////GPF18
            //{
            //    // code block to be executed
            //    if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            //    {
            //        bol01 = true;
            //    }
            //    else
            //    {
            //        bol01 = false;
            //        break;
            //    }
            //}
            //GPF12
            //-------------------------------------------------------
            // Promise to Disable CradleInSync
            //-------------------------------------------------------
            if (HighLevel.Status.CradleInSync == false)
            {
                //GPI25
                //////if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == true &&
                ///
                if (pcpm == true &&
                    pcpmEnabled == true)
                {
                    HighLevel.Status.PromiseToDisableCradleInSync = true;
                }
                else
                {
                    HighLevel.Status.PromiseToDisableCradleInSync = true; //lascio disabilitato perchè siamo in CradleInSync == false
                }
            }
            //-------------------------------------------------------
            // Promise to Enable CradleInSync
            //-------------------------------------------------------           
            if (HighLevel.Status.CradleInSync == true)
            {
                if (pcpmEnabled == false)
                {
                    HighLevel.Status.PromiseToEnableCradleInSync = true;
                    Console.WriteLine($"[TaskStateCradleJog] pcpmEnabled:{pcpmEnabled} PromiseToDisableCradleInSync:{HighLevel.Status.PromiseToEnableCradleInSync}");
                }
            }

            //-------------------------------------------------------
            // Cradle Sync
            //-------------------------------------------------------
            HighLevel.Status.CradleInSync = false;

            //-------------------------------------------------------
            // Disattivo task allineamento
            //-------------------------------------------------------
            HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = false;

            //-------------------------------------------------------
            // Velocity
            //-------------------------------------------------------
            float velocity = (float)HighLevel.Settings.HighLevel.MachineParameters.CradleJogVelocity.Value;

            if (HighLevel.Status.JogState == JogState.AcwMode)
            {
                velocity = -1.0f * velocity;
            }

            if (HighLevel.WorkingContext.Parameters.StraightRoller == false)
            {
                velocity = -1.0f * velocity;
            }

            //-------------------------------------------------------
            // Spoon Up
            //-------------------------------------------------------
            Devices.ElectricDrives.SpoonUp();

            if (cutterPresence)
            {
                while (LowLevel.Actions.SpoonUp)
                {
                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                }
            }

            //-------------------------------------------------------
            // StartTaskAlignment
            //-------------------------------------------------------
            if (HighLevel.Status.JogState == JogState.AcwMode)
            {
                if (materialPresentAtStart &&
                    HighLevel.WorkingContext.Parameters.PhotocellAlignmentEnabled &&
                    HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled)
                {
                    Devices.ElectricDrives.StartTaskAlignment();
                }
                else if (HighLevel.WorkingContext.Parameters.PhotocellAlignmentEnabled &&
                         HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled == false)
                {
                    Devices.ElectricDrives.StartTaskAlignment();
                }
            }

            //-------------------------------------------------------
            // Initial state
            //-------------------------------------------------------
            StateMachine.CradleJogManualOperationsSubState = CradleJogManualOperationsSubState.Jogging;

            //-------------------------------------------------------
            // Logic
            //-------------------------------------------------------
            Communicator.StartJog(velocity);

            while (!cancellationToken.IsCancellationRequested)
            {
                bool sr = HighLevel.WorkingContext.Parameters.StraightRoller;

                //-------------------------------------------------------
                // Stop Requested
                //-------------------------------------------------------
                if (HighLevel.Status.JogState == JogState.Stopped)
                {
                    StateMachine.CradleJogManualOperationsSubState = CradleJogManualOperationsSubState.Completed;
                }

                switch (StateMachine.CradleJogManualOperationsSubState)
                {
                    case CradleJogManualOperationsSubState.Jogging:

                        //GPI12 sostituzione check temporale su PhotocellMaterialPresence:
                        ////bool bol01 = false;
                        //bol01 = false;
                        //if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
                        //{
                        //    bol01 = true;
                        //}
                        //else
                        //{
                        //    bol01 = false;
                        //}
                        ////DateTime checkUntilPhotocellMaterialPresence = DateTime.MinValue;
                        //checkUntilPhotocellMaterialPresence = DateTime.MinValue;
                        //checkUntilPhotocellMaterialPresence = DateTime.UtcNow + TimeSpan.FromMilliseconds(HighLevel.Settings.HighLevel.MachineParameters.CheckUntilPhotocellMaterialPresence);   //TimeSpan.FromMilliseconds(250) parametro da mettere nella Cradle per intervallo di check fotocellula presenza materiale
                        ////GPI18
                        //while ((DateTime.UtcNow < checkUntilPhotocellMaterialPresence) && (!MachineControllerApplication.NoInitCheckPhotocell))
                        ////GPF18
                        //{
                        //    // code block to be executed
                        //    if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
                        //    {
                        //        bol01 = true;
                        //    }
                        //    else
                        //    {
                        //        bol01 = false;
                        //        break;
                        //    }
                        //}
                        //GPF12
                        if (materialPresentAtStart == false &&
                            HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled &&
                            //GPI25
                            //////LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence])
                            HighLevel.Status.PhotocelMaterialPresenceFiltered)
                            //!bol01)
                            //GPF25
                        {
                            float materialLength = HighLevel.Settings.HighLevel.MachineParameters.LengthMaterialSupplyOnLoadUnload.Value;

                            if (sr)
                            {
                                cradlePositionToStop = LowLevel.Axes.Cradle.Position - materialLength;
                            }
                            else
                            {
                                cradlePositionToStop = LowLevel.Axes.Cradle.Position + materialLength;
                            }

                            if (HighLevel.WorkingContext.Parameters.PhotocellAlignmentEnabled)
                            {
                                Devices.ElectricDrives.StartTaskAlignment();
                            }

                            //Console.WriteLine($"cradlePositionToStop:{cradlePositionToStop} ");
                            //Console.WriteLine($"==> ENTERING LowLevel.Axes.Cradle.Position:{ LowLevel.Axes.Cradle.Position} ");

                            timestampStartMaterialSubState = DateTime.UtcNow;
                            StateMachine.CradleJogManualOperationsSubState = CradleJogManualOperationsSubState.Material;
                        }
                        break;

                    case CradleJogManualOperationsSubState.Material:

                        if (DateTime.UtcNow > (timestampStartMaterialSubState + minTimespanToRemainInMaterialSubstate))
                        {
                            if (sr)
                            {
                                if (LowLevel.Axes.Cradle.Position < cradlePositionToStop)
                                {
                                    materialPresentAtStart = true;

                                    Communicator.StopJog();

                                    Console.WriteLine($"[Material ==> WaitButton] LowLevel.Axes.Cradle.Position:{ LowLevel.Axes.Cradle.Position}");

                                    StateMachine.CradleJogManualOperationsSubState = CradleJogManualOperationsSubState.WaitButton;
                                }
                            }
                            else
                            {
                                if (LowLevel.Axes.Cradle.Position > cradlePositionToStop)
                                {
                                    materialPresentAtStart = true;

                                    Communicator.StopJog();

                                    Console.WriteLine($"[Material ==> WaitButton] LowLevel.Axes.Cradle.Position:{ LowLevel.Axes.Cradle.Position}");

                                    StateMachine.CradleJogManualOperationsSubState = CradleJogManualOperationsSubState.WaitButton;

                                }
                            }
                        }
                        break;

                    case CradleJogManualOperationsSubState.WaitButton:

                        // Just Wait

                        break;

                    case CradleJogManualOperationsSubState.Completed:

                        Devices.ElectricDrives.StopTaskAlignment();

                        StateMachine.CradleJogManualOperationsSubState = CradleJogManualOperationsSubState.WaitExit;
                        //StateMachine.SetStateFromTask(ControlState.ManualOperations);

                        break;

                    case CradleJogManualOperationsSubState.WaitExit:
                        StateMachineHelper.PrintYouShouldNotSeeMeMessage(cycles);
                        break;

                }

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                cycles++;
            } //while()

            ProConsole.WriteLine("[EXITING] TaskStateCradleJogManualOperations", ConsoleColor.Red);

        } //task

    } //class
} //namespace