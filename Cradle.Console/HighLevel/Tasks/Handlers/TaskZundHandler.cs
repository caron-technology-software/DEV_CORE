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
        private const int MillisecondsTimeoutAfterSendZundCommand = 250;

        public void TaskZundHandler(CancellationToken cancellationToken)
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
            ProConsole.WriteLine("[ENTERING] TaskZundHandler", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
            ThreadsStarted++;

            //-------------------------------------------------------
            // Inputs/outputs
            //-------------------------------------------------------
            bool enableExternalIO = false;
            bool precEnableExternalIO = false;

            bool march = false;
            bool precMarch = false;

            bool zundEnable = false;
            bool precZundEnable = false;

            bool cutOff = false;
            bool precCutOff = false;

            bool pcpmEnabled = false;
            bool pcpm = false;

            bool syncApiEnable = false;
            bool precSyncApiEnable = false;

            //-------------------------------------------------------
            // Cycle
            //-------------------------------------------------------
            while (!cancellationToken.IsCancellationRequested)
            {
                //*******************************************************
                // LOGICA: 0->ERROR 1->OK
                //*******************************************************

                //-------------------------------------------------------
                // Updates
                //-------------------------------------------------------
                bool commandExecuted = false;

                enableExternalIO = HighLevel.Settings.HighLevel.MachineParameters.EnableExternalInputsOutputs.Value;

                march = LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled];
                zundEnable = LowLevel.IO.DigitalInputs[(byte)DigitalInput.ZundEnable];
                cutOff = LowLevel.IO.DigitalInputs[(byte)DigitalInput.ZundCutOff];

                pcpmEnabled = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
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
                //////pcpm = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence];
                pcpm = HighLevel.Status.PhotocelMaterialPresenceFiltered;
                //pcpm = !bol01;
                //GPF25

                syncApiEnable = HighLevel.Signals.EnableSync;

                //-------------------------------------------------------
                // External Commands
                //-------------------------------------------------------
                if (HighLevel.Signals.Cut)
                {
                    Console.WriteLine($"HighLevel.Signals.Cut");
                    HighLevel.Signals.Cut = false;
                    cutOff = true;
                }

                //-------------------------------------------------------
                // Init
                //-------------------------------------------------------
                if (enableExternalIO == true && precEnableExternalIO == false)
                {
                    Communicator.SendCommand(Command.ZundError, true);
                    commandExecuted = true;
                }

                //-------------------------------------------------------
                // External Inputs Outputs
                //-------------------------------------------------------
                if (enableExternalIO)
                {
                    if (march == true && precMarch == false)
                    {
                        if (LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.ZundError] == false)
                        {
                            Communicator.SendCommand(Command.ZundError, true);
                            commandExecuted = true;
                        }
                    }

                    //-------------------------------------------------------
                    // Errors
                    //-------------------------------------------------------
                    if (march == true)
                    {
                        bool e1 = LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.ZundError] == false &&
                                  LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitCutterOperatorSide];

                        bool e2 = LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.ZundError] == true &&
                                  LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitCutterOperatorSide] == false &&
                                  HighLevel.Status.HighLevelControlState != ControlState.CutOff;

                        bool e3 = LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.ZundError] == true &&
                                  HighLevel.Status.HighLevelControlState == ControlState.Sharpening;
                        if (e1)
                        {
                            Communicator.SendCommand(Command.ZundError, true);
                            commandExecuted = true;
                        }
                        else if (e2 || e3)
                        {
                            Communicator.SendCommand(Command.ZundError, false);
                            commandExecuted = true;
                        }
                    }
                    else //march=false
                    {
                        if (LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.ZundError] == true)
                        {
                            Communicator.SendCommand(Command.ZundError, false);
                            commandExecuted = true;
                        }
                    }


                    //-------------------------------------------------------
                    // Sync
                    //-------------------------------------------------------

                    //Attivazione sync (API)
                    //GPIx6   dato che precSyncApiEnable può essere sfalsato se il comando viene fatto manualmente [spegnimento da bottone UI e non con API]
                    //        bisognerebbe togliere il check precSyncApiEnable come da codice remmato sottostante
                    //        Se non lo si fa in caso di chiusura del sync manuale, bisogna fare prima il disable sync con le API e poi l'enable sync API
                    //        altrimenti l'enable sync API non funziona.
                    //if (syncApiEnable == true)
                    if (syncApiEnable == true && precSyncApiEnable == false)
                    //GPFx6
                    {
                        if (HighLevel.Status.CradleInSync == false)
                        {
                            if (IsSyncAllowed())
                            {
                                HighLevel.Status.CradleInSync = true;

                                Communicator.SetMachineLowLevelSettings(
                                                HighLevel.Settings.LowLevelMotion,
                                                HighLevel.Settings.HighLevel.FunctionsEnabled,
                                                HighLevel.Settings.HighLevel.MachineParameters);

                                Devices.Cradle.SetLowLevelWorkingState();
                            }
                            else
                            {
                                Communicator.SendCommand(Command.ZundError, false);
                                commandExecuted = true;
                            }
                        }
                    }

                    //Attivazione sync (IO)
                    if (zundEnable == true && precZundEnable == false)
                    {
                        if (HighLevel.Status.CradleInSync == false)
                        {
                            if (IsSyncAllowed())
                            {
                                HighLevel.Status.CradleInSync = true;

                                Communicator.SetMachineLowLevelSettings(
                                                HighLevel.Settings.LowLevelMotion,
                                                HighLevel.Settings.HighLevel.FunctionsEnabled,
                                                HighLevel.Settings.HighLevel.MachineParameters);

                                Devices.Cradle.SetLowLevelWorkingState();
                            }
                            else
                            {
                                Communicator.SendCommand(Command.ZundError, false);
                                commandExecuted = true;
                            }
                        }
                    }

                    //Disattivazione sync (March)
                    if (march == false && precMarch == true)
                    {
                        if (HighLevel.Status.CradleInSync == true)
                        {
                            HighLevel.Status.CradleInSync = false;
                            Devices.Cradle.SetLowLevelWorkingState();
                        }
                    }

                    //Disattivazione sync (API) 
                    //GPIx6   dato che precSyncApiEnable può essere sfalsato se il comando viene fatto manualmente [spegnimento da bottone UI e non con API]
                    //        bisognerebbe togliere il check precSyncApiEnable come da codice remmato sottostante
                    //        Se non lo si fa in caso di apertura del sync manuale, bisogna fare prima l'enable sync con le API e poi il disable sync API
                    //        altrimenti il disable sync API non funziona.
                    //if (syncApiEnable == true)
                    if (syncApiEnable == false && precSyncApiEnable == true)
                    //GPFx6
                    {
                        if (HighLevel.Status.CradleInSync == true)
                        {
                            HighLevel.Status.CradleInSync = false;
                            Devices.Cradle.SetLowLevelWorkingState();
                        }
                    }

                    //Disattivazione sync (IO)
                    if (zundEnable == false && precZundEnable == true)
                    {
                        HighLevel.Signals.EnableSync = false;

                        if (HighLevel.Status.CradleInSync == true)
                        {
                            HighLevel.Status.CradleInSync = false;
                            Devices.Cradle.SetLowLevelWorkingState();
                        }
                    }

                    if (HighLevel.Status.CradleInSync != LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.ZundStatus])
                    {
                        Communicator.SendCommand(Command.ZundStatus, HighLevel.Status.CradleInSync);
                        commandExecuted = true;
                    }

                    //-------------------------------------------------------
                    // Cutter
                    //-------------------------------------------------------
                    if (HighLevel.Status.HighLevelControlState == ControlState.CutOff)
                    {
                        //Attesa disattivazione Sync
                        while (HighLevel.Status.CradleInSync)
                        {
                            Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                        }

                        Communicator.SendCommand(Command.ZundStatus, HighLevel.Status.CradleInSync);
                        commandExecuted = true;

                        //Attesa di uscire dallo stato CutOff
                        while (HighLevel.Status.HighLevelControlState != ControlState.Normal)
                        {
                            Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                        }
                    }

                    if (cutOff == false && precCutOff == true)
                    {
                        if (march &&
                            LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitCutterOperatorSide] &&
                            HighLevel.Status.HighLevelControlState == ControlState.Normal &&
                            Math.Abs(LowLevel.Axes.Cradle.Velocity) < Machine.Constants.Kinematics.MinVelocityToConsiderDeviceInMovement)
                        {
                            //Reset error
                            Communicator.SendCommand(Command.ZundError, true);
                            commandExecuted = true;

                            bool syncPreCut = HighLevel.Status.CradleInSync;

                            //Disabilito sync
                            HighLevel.Status.CradleInSync = false;
                            Communicator.SendCommand(Command.ZundStatus, HighLevel.Status.CradleInSync);
                            Devices.Cradle.SetLowLevelWorkingState();

                            StateMachine.SetStateFromTask(ControlState.CutOff);

                            //Attesa di entrare nello stato CutOff
                            while (HighLevel.Status.HighLevelControlState == ControlState.Normal)
                            {
                                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                            }

                            //Attesa di uscire dallo stato CutOff
                            while (HighLevel.Status.HighLevelControlState != ControlState.Normal)
                            {
                                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                            }

                            if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitCutterOperatorSide] == false)
                            {
                                Communicator.SendCommand(Command.ZundError, false);
                                commandExecuted = true;
                            }

                            //Riattivazione (eventuale) Sync
                            if (syncPreCut)
                            {
                                HighLevel.Status.CradleInSync = syncPreCut;
                            }
                        }
                        else
                        {
                            Communicator.SendCommand(Command.ZundError, false);
                            commandExecuted = true;
                        }
                    }

                    //-------------------------------------------------------
                    // Precedent inputs/outputs
                    //-------------------------------------------------------
                    precEnableExternalIO = enableExternalIO;

                    precZundEnable = zundEnable;
                    precMarch = march;
                    precCutOff = cutOff;

                    precSyncApiEnable = syncApiEnable;
                }

                //-------------------------------------------------------
                // Delay
                //-------------------------------------------------------
                if (commandExecuted)
                {
                    Thread.Sleep(MillisecondsTimeoutAfterSendZundCommand);
                }

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);

                // INTERNAL FUNCTION
                bool IsSyncAllowed()
                {
                    if (LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled] == false)
                    {
                        return false;
                    }

                    if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitCutterOperatorSide] == false)
                    {
                        return false;
                    }

                    if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningMotorSideLoad] == false ||
                                   LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningOperatorSideLoad] == false)
                    {
                        return false;
                    }

                    bool c1 = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
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
                    //////bool c2 = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence];
                    bool c2 = HighLevel.Status.PhotocelMaterialPresenceFiltered;
                    //bool c2 = !bol01;
                    //GPF25

                    bool cond = false;

                    if (c1 && c2)
                    {
                        cond = true;
                    }
                    else if (c1 && !c2)
                    {
                        cond = false;

                    }
                    else if (!c1 && c2)
                    {
                        cond = true;
                    }
                    else if (!c1 && !c2)
                    {
                        cond = true;
                    }

                    return cond;
                }
            }

            ProConsole.WriteLine("[EXITING] TaskZundHandler", ConsoleColor.Red);
        }
    }
}