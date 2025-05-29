#define STOP_JOG_AFTER_MATERIAL_OUT_POSITION

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
        public void TaskStateCradleJog(CancellationToken cancellationToken)
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
            ProConsole.WriteLine("[ENTERING] TaskStateCradleJog", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

#pragma warning disable CS0219
            const float MinVelocityToConsiderDeviceInMovement = Machine.Constants.Kinematics.MinVelocityToConsiderDeviceInMovement;
            const float MaxVelocityToConsiderDeviceStationary = Machine.Constants.Kinematics.MaxVelocityToConsiderDeviceStationary;
            const int MaxNumberOfIterAtWaitExitCondition = 1000;
#pragma warning restore CS0219

            bool sr = HighLevel.WorkingContext.Parameters.StraightRoller;
            bool pcpmEnabled = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
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
            //////bool pcpm = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence];
            bool pcpm = HighLevel.Status.PhotocelMaterialPresenceFiltered;
            //bool pcpm = !bol01;
            //GPf25

            uint cycles = 0;

            float cradlePositionToStop = 0.0f;
            bool materialPresentAtStart = pcpm;
            bool cutterPresence = HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value;

            //MMIx02
            bool prp = HighLevel.Status.PhotocelRollPresenceFiltered;
            bool prpEnabled = HighLevel.WorkingContext.Parameters.EnablePhotocellRollPresence && HighLevel.Settings.HighLevel.FunctionsEnabled.EnableFunctionPhotocellRollPresence.Value;//MMIx05
            //MMFx02

            TimeSpan minTimespanToRemainInMaterialSubstate = TimeSpan.FromMilliseconds(1);
            DateTime timestampStartMaterialSubState = DateTime.MaxValue;

            Console.WriteLine($"[TaskStateCradleJog] MaterialPresentAtStart:{materialPresentAtStart} JogState:{HighLevel.Status.JogState}");

            //-------------------------------------------------------
            // Promise to Disable CradleInSync
            //-------------------------------------------------------
            if (HighLevel.Status.CradleInSync == false)
            {
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
                    pcpmEnabled)
                {
                    Devices.ElectricDrives.StartTaskAlignment();
                }
                else if (HighLevel.WorkingContext.Parameters.PhotocellAlignmentEnabled &&
                         pcpmEnabled == false)
                {
                    Devices.ElectricDrives.StartTaskAlignment();
                }
            }

            //-------------------------------------------------------
            // Initial state
            //-------------------------------------------------------
            StateMachine.CradleJogSubState = CradleJogSubState.Jogging;

            //-------------------------------------------------------
            // Logic
            //-------------------------------------------------------
            Communicator.StartJog(velocity);

            while (!cancellationToken.IsCancellationRequested)
            {
                //-------------------------------------------------------
                // Refresh variables
                //-------------------------------------------------------
                sr = HighLevel.WorkingContext.Parameters.StraightRoller;
                pcpmEnabled = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
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
                //GPI25
                //////pcpm = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence];
                pcpm = HighLevel.Status.PhotocelMaterialPresenceFiltered;
                //pcpm = !bol01;
                //GPF25

                //-------------------------------------------------------
                // Stop Requested
                //-------------------------------------------------------
                if (HighLevel.Status.JogState == JogState.Stopped)
                {
                    StateMachine.CradleJogSubState = CradleJogSubState.Completed;
                }

                switch (StateMachine.CradleJogSubState)
                {
                    case CradleJogSubState.Jogging:

                        if (materialPresentAtStart == false &&
                            pcpmEnabled == true &&
                            pcpm == true)
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

                            Console.WriteLine($"cradlePositionToStop:{cradlePositionToStop} ");
                            Console.WriteLine($"==> ENTERING LowLevel.Axes.Cradle.Position:{ LowLevel.Axes.Cradle.Position} ");

                            timestampStartMaterialSubState = DateTime.UtcNow;
                            StateMachine.CradleJogSubState = CradleJogSubState.Material;
                        }

                        break;

                    case CradleJogSubState.Material:

                        if (DateTime.UtcNow > (timestampStartMaterialSubState + minTimespanToRemainInMaterialSubstate))
                        {
                            if (sr)
                            {
                                if (LowLevel.Axes.Cradle.Position < cradlePositionToStop)
                                {
                                    materialPresentAtStart = true;

#if STOP_JOG_AFTER_MATERIAL_OUT_POSITION

                                    if (pcpmEnabled && pcpm)
                                    {
                                        Communicator.StopJog();

                                        Console.WriteLine($"==> EXITING [1] LowLevel.Axes.Cradle.Position:{ LowLevel.Axes.Cradle.Position} ");
                                    }

#endif
                                    StateMachine.CradleJogSubState = CradleJogSubState.WaitButton;
                                }
                            }
                            else
                            {
                                if (LowLevel.Axes.Cradle.Position > cradlePositionToStop)
                                {
                                    materialPresentAtStart = true;

#if STOP_JOG_AFTER_MATERIAL_OUT_POSITION

                                    if (pcpmEnabled && pcpm)
                                    {
                                        Communicator.StopJog();

                                        Console.WriteLine($"==> EXITING [2] LowLevel.Axes.Cradle.Position:{ LowLevel.Axes.Cradle.Position} ");
                                    }

#endif
                                    StateMachine.CradleJogSubState = CradleJogSubState.WaitButton;
                                }
                            }
                        }

                        break;

                    case CradleJogSubState.WaitButton:

                        //------------------
                        // Wait
                        //------------------

                        break;

                    case CradleJogSubState.Completed:

                        Devices.ElectricDrives.StopTaskAlignment();
                        StateMachine.CradleJogSubState = CradleJogSubState.WaitExit;
                        //StateMachine.SetStateFromTask(ControlState.Normal);

                        break;

                    case CradleJogSubState.WaitExit:

                        StateMachineHelper.PrintYouShouldNotSeeMeMessage(cycles);

                        break;
                }

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);

                cycles++;

            } //while()

            ProConsole.WriteLine("[EXITING] TaskStateCradleJog", ConsoleColor.Red);

        } //task
    } //class
} //namespace