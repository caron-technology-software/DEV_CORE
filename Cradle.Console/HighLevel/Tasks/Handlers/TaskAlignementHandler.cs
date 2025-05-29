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
        public void TaskAlignementHandler(CancellationToken cancellationToken)
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
            ProConsole.WriteLine("[ENTERING] TaskAlignementHandler", ConsoleColor.Green);
            ThreadsStarted++;

            bool executeAlignment = false;
            bool primaryExecution = true;
            DateTime timestampToStartRunHandler = DateTime.MaxValue;

            while (!cancellationToken.IsCancellationRequested)
            {
                if (HighLevel.Status.HighLevelControlState == ControlState.Normal &&
                    HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive &&
                    HighLevel.Status.CradleInSync &&
                    HighLevel.WorkingContext.Parameters.PhotocellAlignmentEnabled)
                {
                    if (executeAlignment == false)
                    {
                        executeAlignment = true;
                        timestampToStartRunHandler = DateTime.UtcNow + TimeSpan.FromSeconds(2);
                    }
                }
                else
                {
                    executeAlignment = false;
                }

                if (executeAlignment && DateTime.UtcNow > timestampToStartRunHandler)
                {
                    primaryExecution = true;

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
                    bool mp = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
                    bool sr = HighLevel.WorkingContext.Parameters.StraightRoller;

                    bool cv;

                    if (sr)
                    {
                        cv = LowLevel.Axes.Cradle.Velocity < (-HighLevel.Settings.HighLevel.MachineParameters.MinCradleVelocityAutoCentering.Value);
                    }
                    else
                    {
                        cv = LowLevel.Axes.Cradle.Velocity > HighLevel.Settings.HighLevel.MachineParameters.MinCradleVelocityAutoCentering.Value;
                    }

                    bool cond = false;

                    if (cv)
                    {
                        if ((hw && !mp) || (hw && mp) || (!mp))
                        {
                            cond = true;
                        }
                    }

                    if (HighLevel.Status.JogState == JogState.AcwMode)
                    {
                        cond = false;
                    }

                    if (cond)
                    {
                        ExecuteAlignment();
                        Thread.Sleep(HighLevel.Settings.HighLevel.MachineParameters.WaitRelaysAfterRisingEdge.Value);
                    }
                    else
                    {
                        StopAlignment();
                        Thread.Sleep(HighLevel.Settings.HighLevel.MachineParameters.WaitRelaysFallingEdge.Value);
                    }
                }
                else
                {
                    if (primaryExecution)
                    {
                        StopAlignment();
                        primaryExecution = false;
                    }
                }

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            ProConsole.WriteLine("[EXITING] TaskAlignementHandler", ConsoleColor.Red);

            //-------------------------------
            // Internal functions
            //-------------------------------
            void StopAlignment()
            {
                Devices.ElectricDrives.StopAlignmentMotorSide();
                Devices.ElectricDrives.StopAlignmentOperatorSide();
            }

            void ExecuteAlignment()
            {
                bool c1 = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellOperatorSide];
                bool c2 = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMotorSide];

                if (HighLevel.Configuration.IsLeftMachine)
                {
                    if (c1 && c2)
                    {
                        //io->virtual_inputs.cradle_alignment_op_side = 0;
                        //io->virtual_inputs.cradle_alignment_mt_side = 1;
                        Devices.ElectricDrives.StopAlignmentOperatorSide();
                        Devices.ElectricDrives.StartAlignmentMotorSide();

                    }
                    else if (!c1 && !c2)
                    {
                        //io->virtual_inputs.cradle_alignment_mt_side = 0;
                        //io->virtual_inputs.cradle_alignment_op_side = 1;
                        Devices.ElectricDrives.StopAlignmentMotorSide();
                        Devices.ElectricDrives.StartAlignmentOperatorSide();
                    }
                    else //if (c1 || c2)
                    {

                        //io->virtual_inputs.cradle_alignment_mt_side = 0;
                        //io->virtual_inputs.cradle_alignment_op_side = 0;
                        StopAlignment();
                    }
                }
                else
                {
                    if (c1 && c2)
                    {
                        //io->virtual_inputs.cradle_alignment_mt_side = 0;
                        //io->virtual_inputs.cradle_alignment_op_side = 1;
                        Devices.ElectricDrives.StopAlignmentMotorSide();
                        Devices.ElectricDrives.StartAlignmentOperatorSide();
                    }
                    else if (!c1 && !c2)
                    {
                        //io->virtual_inputs.cradle_alignment_op_side = 0;
                        //io->virtual_inputs.cradle_alignment_mt_side = 1;
                        Devices.ElectricDrives.StopAlignmentOperatorSide();
                        Devices.ElectricDrives.StartAlignmentMotorSide();
                    }
                    else //if (c1 || c2)
                    {

                        //io->virtual_inputs.cradle_alignment_mt_side = 0;
                        //io->virtual_inputs.cradle_alignment_op_side = 0;
                        StopAlignment();
                    }
                }
            }
        }
    }
}