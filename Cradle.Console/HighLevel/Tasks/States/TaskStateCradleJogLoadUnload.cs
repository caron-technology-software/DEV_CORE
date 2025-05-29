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
        public void TaskCradleJogLoadUnload(CancellationToken cancellationToken)
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
            ProConsole.WriteLine("[ENTERING] TaskCradleJogLoadUnload\n\n", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

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

            bool autocenteringDone = false;

            Console.WriteLine($"materialPresentAtStart:{materialPresentAtStart} ");
            Console.WriteLine($"JogState:{HighLevel.Status.JogState} ");

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
            StateMachine.CradleJogLoadUnloadSubState = CradleJogLoadUnloadSubState.Jogging;

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
                    StateMachine.CradleJogLoadUnloadSubState = CradleJogLoadUnloadSubState.Completed;
                }

                switch (StateMachine.CradleJogLoadUnloadSubState)
                {
                    case CradleJogLoadUnloadSubState.Jogging:

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
                        //////if (materialPresentAtStart == false && LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence])
                        if (materialPresentAtStart == false && HighLevel.Status.PhotocelMaterialPresenceFiltered)
                        //if (materialPresentAtStart == false && !bol01)
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

                            Console.WriteLine($"cradlePositionToStop:{cradlePositionToStop} ");
                            Console.WriteLine($"==> ENTERING LowLevel.Axes.Cradle.Position:{ LowLevel.Axes.Cradle.Position} ");

                            StateMachine.CradleJogLoadUnloadSubState = CradleJogLoadUnloadSubState.Material;
                        }

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
                        if (materialPresentAtStart &&
                            HighLevel.Settings.HighLevel.MachineParameters.AutoCenteringEnabled.Value == true &&
                            HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled == true &&
                            //GPI25
                            //////LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false &&
                            HighLevel.Status.PhotocelMaterialPresenceFiltered == false &&
                            //bol01 &&
                            //GPF25
                            autocenteringDone == false &&
                            HighLevel.Status.JogState == JogState.CwMode)
                        {
                            autocenteringDone = true;

                            Task.Run(() =>
                            {
                                Devices.ElectricDrives.StartTaskAutoCentering();
                            });
                        }

                        break;

                    case CradleJogLoadUnloadSubState.Material:

                        if (sr)
                        {
                            if (LowLevel.Axes.Cradle.Position < cradlePositionToStop)
                            {
                                materialPresentAtStart = true;
                                StateMachine.CradleJogLoadUnloadSubState = CradleJogLoadUnloadSubState.WaitButton;

                                Communicator.StopJog();

                                Console.WriteLine($"==> EXITING [1] LowLevel.Axes.Cradle.Position:{ LowLevel.Axes.Cradle.Position} ");
                            }
                        }
                        else
                        {
                            if (LowLevel.Axes.Cradle.Position > cradlePositionToStop)
                            {
                                materialPresentAtStart = true;
                                StateMachine.CradleJogLoadUnloadSubState = CradleJogLoadUnloadSubState.WaitButton;

                                Communicator.StopJog();

                                Console.WriteLine($"==> EXITING [2] LowLevel.Axes.Cradle.Position:{ LowLevel.Axes.Cradle.Position} ");
                            }
                        }
                        break;

                    case CradleJogLoadUnloadSubState.WaitButton:

                        //--

                        break;

                    case CradleJogLoadUnloadSubState.Completed:

                        StateMachine.CradleJogLoadUnloadSubState = CradleJogLoadUnloadSubState.WaitExit;
                        //StateMachine.SetStateFromTask(ControlState.CradleJogLoadUnload);

                        break;

                    case CradleJogLoadUnloadSubState.WaitExit:
                        StateMachineHelper.PrintYouShouldNotSeeMeMessage(cycles);
                        break;
                }

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                cycles++;

            } //while()

            ProConsole.WriteLine("[EXITING] TaskCradleJogLoadUnload", ConsoleColor.Red);

        } //task

    } //class
} //namespace