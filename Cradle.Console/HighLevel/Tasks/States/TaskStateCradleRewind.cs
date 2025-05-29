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
        public void TaskStateCradleRewind(CancellationToken cancellationToken)
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
            ProConsole.WriteLine("[ENTERING] TaskStateCradleRewind", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

            uint cycles = 0;

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

            //-------------------------------------------------------
            // Velocity
            //-------------------------------------------------------
            float velocity = (float)HighLevel.Settings.HighLevel.MachineParameters.CradleJogFastVelocity.Value;

            if (HighLevel.WorkingContext.Parameters.StraightRoller == false)
            {
                velocity = -1.0f * velocity;
            }

            //-------------------------------------------------------
            // Spoon Up
            //-------------------------------------------------------
            Devices.ElectricDrives.SpoonUp();
            //while (LowLevel.Actions.SpoonUp)
            //{
            //    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            //}

            //-------------------------------------------------------
            // Initial state
            //-------------------------------------------------------
            StateMachine.CradleRewindSubState = CradleRewindSubState.Jogging;

            //-------------------------------------------------------
            // Logic
            //-------------------------------------------------------
            Communicator.StartJog(velocity);

            while (!cancellationToken.IsCancellationRequested)
            {
                //-------------------------------------------------------
                // Stop Requested
                //-------------------------------------------------------
                if (HighLevel.Status.JogState == JogState.Stopped)
                {
                    StateMachine.CradleRewindSubState = CradleRewindSubState.Completed;
                }

                switch (StateMachine.CradleRewindSubState)
                {
                    case CradleRewindSubState.Jogging:
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
                        //---------------------------------------
                        // Auto Centering
                        //---------------------------------------
                        if (materialPresentAtStart &&
                           HighLevel.Settings.HighLevel.MachineParameters.AutoCenteringEnabled.Value == true &&
                           HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled == true &&
                           //GPI25
                           //////LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
                           HighLevel.Status.PhotocelMaterialPresenceFiltered == false)
                           //bol01)
                           //GPF25
                        {
                            Console.WriteLine($"[StartTaskAutoCentering]");
                            Task.Run(() =>
                            {
                                Devices.ElectricDrives.StartTaskAutoCentering();
                            });
                        }

                        break;

                    case CradleRewindSubState.Completed:
                        StateMachine.CradleRewindSubState = CradleRewindSubState.WaitExit;
                        //StateMachine.SetStateFromTask(ControlState.LoadUnload);
                        break;

                    case CradleRewindSubState.WaitExit:
                        StateMachineHelper.PrintYouShouldNotSeeMeMessage(cycles);
                        break;
                }

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                cycles++;

            } //while()

            ProConsole.WriteLine("[EXITING] TaskStateCradleRewind", ConsoleColor.Red);

        } //task

    } //class
} //namespace