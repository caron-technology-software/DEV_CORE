using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

using ProRob;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel.StateMachine;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        public const float DeltaDancerBarAfterCut = 0.005f;
        private const int TimeoutPrefeed = 4000;
        private const int TimeoutSpoon = 500;
        private const int DelayAfterSetControlState = 10;

        public void TaskStateCutOff(CancellationToken cancellationToken)
        {
            //-------------------------------------
            // RegisterCurrentThread
            //-------------------------------------
            while (StateMachine is null)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            StateMachineHelper.RegisterCurrentThread(StateMachine, Thread.CurrentThread);

            HighLevel.Signals.Stop = false;

            var mp = HighLevel.Settings.HighLevel.MachineParameters;

            //-------------------------------------
            // Task
            //-------------------------------------
            ProConsole.WriteLine("[ENTERING] TaskStateCutOff", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

            //----------------------------------------------
            // Endurance
            //----------------------------------------------
            HighLevel.MachineEndurance.Cutter.NumberOfCutOff++;

            //----------------------------------------------
            // Sync
            //----------------------------------------------
            if (HighLevel.Status.CradleInSync)
            {
                //Disabilito sync
                HighLevel.Status.CradleInSync = false;
                Devices.Cradle.SetLowLevelWorkingState();
                Thread.Sleep(10);
            }

            //----------------------------------------------
            // Spoon
            //----------------------------------------------
            if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitSpoonDown] == false)
            {
                Communicator.SendCommand(Command.SpoonDown, 0x01);

                // Attesa di sicurezza
                Console.WriteLine($"[TaskStateCutOff] Waiting SpoonDown..");

                var sw = new Stopwatch();
                sw.Start();

                bool exitCondSpoon = false;
                while (exitCondSpoon == false)
                {
                    if (sw.Elapsed > TimeSpan.FromMilliseconds(TimeoutSpoon))
                    {
                        exitCondSpoon = true;
                    }
                    else if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitSpoonDown] == false)
                    {
                        sw.Restart();
                    }

                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                }

                sw.Stop();

                Console.WriteLine($"[TaskStateCutOff] SpoonDown is down");
            }
            else
            {
                Console.WriteLine($"[TaskStateCutOff] Spoon is already down");
            }

            //--------------------------------------------------
            // Variables
            //--------------------------------------------------
            bool cutterStartedInMotorSide = LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitCutterMotorSide];
            bool cutterStartedInOperatorSide = LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitCutterOperatorSide];
            bool sharpening = HighLevel.Status.SharpeningEnabled;
            bool preFeedDisabled = cutterStartedInMotorSide == false && cutterStartedInOperatorSide == false;

            var TimeOutPreeFeed = TimeSpan.FromMilliseconds(TimeoutPrefeed);

            //----------------------------------------------
            // Logic
            //----------------------------------------------
            StateMachine.CutOffSubState = CutOffSubState.Waiting;

            while (!cancellationToken.IsCancellationRequested)
            {
                switch (StateMachine.CutOffSubState)
                {
                    case CutOffSubState.Waiting:

                        if (cutterStartedInMotorSide)
                        {
                            Communicator.SetLowLevelControlState(Control.LowLevel.ControlState.CutOff);
                            Thread.Sleep(DelayAfterSetControlState);
                            Communicator.SendCommand(Command.CutVersusOperatorSide, 1.0f);

                            StateMachine.CutOffSubState = CutOffSubState.RunVersusOperatorSide;
                        }
                        else
                        {
                            float velocity = (float)HighLevel.WorkingContext.Parameters.CutterVelocity;

                            Communicator.SetLowLevelControlState(Control.LowLevel.ControlState.CutOff);
                            Thread.Sleep(10);
                            Communicator.SendCommand(Command.CutVersusMotorSide, velocity);

                            StateMachine.CutOffSubState = CutOffSubState.RunVersusMotorSide;
                        }

                        if (LowLevel.Info.MachineState == (byte)Control.LowLevel.ControlState.WaitMarch)
                        {
                            StateMachine.CutOffSubState = CutOffSubState.Stopped;
                        }

                        break;

                    case CutOffSubState.RunVersusMotorSide:

                        if (LowLevel.Info.CutterState == (byte)CutterState.MotionCompleted)
                        {
                            Communicator.SendCommand(Command.CutVersusOperatorSide, 1.0f);
                            StateMachine.CutOffSubState = CutOffSubState.RunVersusOperatorSide;
                        }

                        if (LowLevel.Info.CutterState == (byte)CutterState.Stopped)
                        {
                            StateMachine.CutOffSubState = CutOffSubState.Stopped;
                        }

                        if (LowLevel.Info.MachineState != (byte)Control.LowLevel.ControlState.CutOff)
                        {
                            StateMachine.CutOffSubState = CutOffSubState.Stopped;
                        }
                        break;

                    case CutOffSubState.RunVersusOperatorSide:
                        if (LowLevel.Info.CutterState == (byte)CutterState.MotionCompleted)
                        {
                            StateMachine.CutOffSubState = CutOffSubState.CutOffEnded;
                        }

                        if (LowLevel.Info.CutterState == (byte)CutterState.Stopped)
                        {
                            StateMachine.CutOffSubState = CutOffSubState.Stopped;
                        }

                        if (LowLevel.Info.MachineState != (byte)Control.LowLevel.ControlState.CutOff)
                        {
                            StateMachine.CutOffSubState = CutOffSubState.Stopped;
                        }
                        break;


                    case CutOffSubState.Stopped:
                        if (LowLevel.Info.CutterState == (byte)CutterState.MotionCompleted)
                        {
                            StateMachine.CutOffSubState = CutOffSubState.CutOffEnded;
                        }

                        if (LowLevel.Info.MachineState == (byte)Control.LowLevel.ControlState.WaitMarch)
                        {
                            StateMachine.SetStateFromTask(ControlState.WaitMarch);
                        }

                        if (LowLevel.Info.MachineState != (byte)Control.LowLevel.ControlState.CutOff)
                        {
                            StateMachine.CutOffSubState = CutOffSubState.CutOffEnded;
                        }
                        break;

                    case CutOffSubState.CutOffEnded:

                        Thread.Sleep(100);
                        Communicator.SetLowLevelControlState(Control.LowLevel.ControlState.WaitCommand);

                        //-------------------------------
                        //Pre-Feed
                        //-------------------------------
                        int preFeed = (int)HighLevel.WorkingContext.Parameters.PreFeedMaterial;

                        bool sr = HighLevel.WorkingContext.Parameters.StraightRoller;
                        bool phe = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
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
                        //////bool hw = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence];
                        bool hw = HighLevel.Status.PhotocelMaterialPresenceFiltered;
                        //bool hw = !bol01;
                        //GPF25

                        bool noPreFeed = false;

                        //Verifica condizioni
                        if (phe)
                        {
                            if (hw)
                            {
                                noPreFeed = false;
                            }
                            else
                            {
                                noPreFeed = true;
                            }
                        }
                        else
                        {
                            noPreFeed = false;
                        }

                        //Se il preFeed è pari a 0 mm non eseguo il prefeed
                        if (noPreFeed)
                        {
                            preFeed = 0;
                        }

                        var sw = new Stopwatch();

                        if (preFeed > 0 && HighLevel.Signals.Stop == false && preFeedDisabled == false)
                        {
                            Thread.Sleep(200);

                            sw.Restart();

                            //Carico tessuto
                            if (!sr)
                            {
                                Communicator.StartJog(-mp.CradleSpeedPositionDancerBar);

                                while (LowLevel.Info.Dancebar < mp.PositionDancerLoadWithPreFeed &&
                                       HighLevel.Signals.Stop == false &&
                                       sw.Elapsed < TimeOutPreeFeed)
                                {
                                    Thread.Yield();
                                }
                                Communicator.StartJog(0.0f);
                            }
                            else
                            {
                                Communicator.StartJog(+mp.CradleSpeedPositionDancerBar);

                                while (LowLevel.Info.Dancebar < mp.PositionDancerLoadWithPreFeed &&
                                       HighLevel.Signals.Stop == false &&
                                       sw.Elapsed < TimeOutPreeFeed)
                                {
                                    Thread.Yield();
                                }
                                Communicator.StartJog(0.0f);
                            }

                            if (HighLevel.Signals.Stop == false && sw.Elapsed < TimeOutPreeFeed)
                            {
                                Thread.Sleep(TimeSpan.FromMilliseconds(HighLevel.Settings.HighLevel.MachineParameters.WaitBetweenLoadAndUnloadBancebar));

                                //Scarico tessuto
                                if (LowLevel.Info.Dancebar > mp.PositionDancerUnloadWithPreFeed)
                                {
                                    if (!sr)
                                    {
                                        Communicator.StartJog(+mp.CradleSpeedPositionDancerBar);

                                        while (LowLevel.Info.Dancebar > (mp.PositionDancerUnloadWithPreFeed - DeltaDancerBarAfterCut))//&&
                                                                                                                                      //!HighLevel.Signals.Stop)
                                        {
                                            Thread.Yield();
                                        }
                                        Communicator.StartJog(0.0f);
                                    }
                                    else
                                    {
                                        Communicator.StartJog(-mp.CradleSpeedPositionDancerBar);

                                        while (LowLevel.Info.Dancebar > (mp.PositionDancerUnloadWithPreFeed - DeltaDancerBarAfterCut))//&&
                                                                                                                                      //!HighLevel.Signals.Stop)
                                        {
                                            Thread.Yield();
                                        }
                                        Communicator.StartJog(0.0f);
                                    }
                                }
                            }

                            Thread.Sleep(TimeSpan.FromMilliseconds(HighLevel.Settings.HighLevel.MachineParameters.WaitBetweenLoadAndUnloadBancebar));
                        }

                        if (HighLevel.Signals.Stop == false && sw.Elapsed < TimeOutPreeFeed)
                        {
                            //-------------------------------------------         
                            //Spoon up
                            //-------------------------------------------
                            Devices.ElectricDrives.SpoonUp();
                            Thread.Sleep(TimeSpan.FromMilliseconds(HighLevel.Settings.HighLevel.MachineParameters.WaitBetweenLoadAndUnloadBancebar));

                            if (preFeed > 0 && preFeedDisabled == false)
                            {
                                Console.WriteLine($"PRE FEED: {preFeed}");

                                if (!sr)
                                {
                                    float startPosition = LowLevel.Axes.Cradle.Position;
                                    float stopPosition = startPosition + preFeed;

                                    Console.WriteLine($"StartPosition:{startPosition} StopPosition:{stopPosition} (sr:{sr})");

                                    Communicator.StartJog(mp.CradleSpeedPreFeed);
                                    while (LowLevel.Axes.Cradle.Position < stopPosition &&
                                           HighLevel.Signals.Stop == false)
                                    {
                                        Thread.Yield();
                                    }
                                    Communicator.StartJog(0.0f);
                                }
                                else
                                {
                                    float startPosition = LowLevel.Axes.Cradle.Position;
                                    float stopPosition = startPosition - preFeed;

                                    Console.WriteLine($"StartPosition:{startPosition} StopPosition:{stopPosition} (sr:{sr})");

                                    Communicator.StartJog(-mp.CradleSpeedPreFeed);
                                    while (LowLevel.Axes.Cradle.Position > stopPosition &&
                                           HighLevel.Signals.Stop == false)
                                    {
                                        Thread.Yield();
                                    }
                                    Communicator.StartJog(0.0f);
                                }
                            }
                        }

                        Devices.ElectricDrives.SpoonUp();

                        StateMachine.SetStateFromTask(ControlState.Normal);

                        StateMachine.CutOffSubState = CutOffSubState.WaitExit;

                        break;

                    case CutOffSubState.WaitExit:
                        //--
                        break;
                }

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);

            } //while()

            ProConsole.WriteLine("[EXITING] TaskStateCutOff", ConsoleColor.Red);

        } //task

    } //class
} //namespace