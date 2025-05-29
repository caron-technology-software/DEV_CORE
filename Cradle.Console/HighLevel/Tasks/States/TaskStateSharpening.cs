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
        const float SharpeningReturnVelocity = 0.5f;
        public void TaskStateSharpening(CancellationToken cancellationToken)
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
            ProConsole.WriteLine("[ENTERING] TaskStateSharpening", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

            HighLevel.MachineEndurance.Cutter.NumberOfCutOff++;

            StateMachine.SharpeningSubState = SharpeningSubState.Waiting;

            //----------------------------------------------
            // Spoon
            //----------------------------------------------
            if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitSpoonDown] == false)
            {
                Communicator.SendCommand(Command.SpoonDown, 0x01);
                while (LowLevel.Actions.SpoonDown)
                {
                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                }

                // Attesa di sicurezza
                Console.WriteLine($"[TaskStateCutOff] Waiting SpoonDown");
                var sw = new Stopwatch();
                sw.Start();
                while (LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitSpoonDown] && sw.Elapsed < TimeSpan.FromMilliseconds(500))
                {
                    if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitSpoonDown] == false)
                    {
                        sw.Restart();
                    }
                }
            }

            bool cutterStartedInMotorSide = LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitCutterMotorSide];
            bool cutterStartedInOperatorSide = LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitCutterOperatorSide];

            while (!cancellationToken.IsCancellationRequested)
            {
                switch (StateMachine.SharpeningSubState)
                {
                    case SharpeningSubState.Waiting:

                        float velocity = (float)HighLevel.WorkingContext.Parameters.CutterVelocity;

                        if (cutterStartedInOperatorSide)
                        {
                            Communicator.SetLowLevelControlState(Control.LowLevel.ControlState.CutOff);
                            Communicator.SendCommand(Command.CutVersusMotorSide, velocity);

                            StateMachine.SharpeningSubState = SharpeningSubState.RunVersusMotorSide;
                        }
                        else
                        {
                            Communicator.SetLowLevelControlState(Control.LowLevel.ControlState.CutOff);
                            Communicator.SendCommand(Command.CutVersusOperatorSide, velocity);

                            StateMachine.SharpeningSubState = SharpeningSubState.RunVersusOperatorSide;
                        }

                        break;

                    case SharpeningSubState.RunVersusOperatorSide:

                        if (LowLevel.Info.CutterState == (byte)CutterState.MotionCompleted)
                        {
                            if (cutterStartedInOperatorSide)
                            {
                                StateMachine.SharpeningSubState = SharpeningSubState.CutOffEnded;
                            }
                            else
                            {
                                Communicator.SetLowLevelControlState(Control.LowLevel.ControlState.CutOff);
                                Communicator.SendCommand(Command.CutVersusMotorSide, SharpeningReturnVelocity);

                                StateMachine.SharpeningSubState = SharpeningSubState.RunVersusMotorSide;
                            }
                        }

                        if (LowLevel.Info.CutterState == (byte)CutterState.Stopped)
                        {
                            StateMachine.SharpeningSubState = SharpeningSubState.Stopped;
                        }

                        if (LowLevel.Info.MachineState == (byte)Control.LowLevel.ControlState.WaitMarch)
                        {
                            StateMachine.CutOffSubState = CutOffSubState.Stopped;
                        }
                        break;

                    case SharpeningSubState.RunVersusMotorSide:
                        if (LowLevel.Info.CutterState == (byte)CutterState.MotionCompleted)
                        {

                            StateMachine.SharpeningSubState = SharpeningSubState.CutOffEnded;
                        }

                        if (LowLevel.Info.CutterState == (byte)CutterState.Stopped)
                        {
                            StateMachine.SharpeningSubState = SharpeningSubState.Stopped;
                        }

                        if (LowLevel.Info.MachineState == (byte)Control.LowLevel.ControlState.WaitMarch)
                        {
                            StateMachine.CutOffSubState = CutOffSubState.Stopped;
                        }
                        break;

                    case SharpeningSubState.Stopped:
                        if (LowLevel.Info.CutterState == (byte)CutterState.MotionCompleted)
                        {
                            StateMachine.SharpeningSubState = SharpeningSubState.CutOffEnded;
                        }

                        if (LowLevel.Info.MachineState == (byte)Control.LowLevel.ControlState.WaitMarch)
                        {
                            StateMachine.SetStateFromTask(ControlState.WaitMarch);
                        }
                        break;

                    case SharpeningSubState.CutOffEnded:
                        Communicator.SetLowLevelControlState(Control.LowLevel.ControlState.WaitCommand);
                        StateMachine.SetStateFromTask(Control.HighLevel.ControlState.Normal);
                        break;

                    case SharpeningSubState.WaitExit:
                        {
                            //--
                        }
                        break;
                }

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);

            } //while()

            ProConsole.WriteLine("[EXITING] TaskStateCutOff", ConsoleColor.Red);

        } //task

    } //class
} //namespace