using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;
using ProRob.Communication;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.LowLevel.Communication;

namespace Caron.Cradle.Control.HighLevel.Devices
{
    public class ElectricDrives
    {
        private Thread threadAutoCentering = null;
        private Thread threadAlignment = null;

        private LowLevel.ControlStatus LowLevel { get; set; }
        private HighLevel.ControlStatus HighLevel { get; set; }
        private Communicator Communicator { get; set; }

        private volatile bool manualAlignmentIsRunning = false;
        private volatile bool taskAlignmentIsRunning = false;

        public ElectricDrives(LowLevel.ControlStatus lowLevel, HighLevel.ControlStatus highLevel, Communicator communicator)
        {
            this.LowLevel = lowLevel;
            this.HighLevel = highLevel;
            this.Communicator = communicator;
        }

        #region STOP
        public void StopAllActions(bool excludeToStopAutoCentering = false)
        {
            StopTaskAlignment();
            StopAlignmentMaterialWithExitCondition();

            if (excludeToStopAutoCentering == false)
            {
                StopTaskAutoCentering();
                HighLevel.TasksStatus.AutoCenteringActivationStatus = false;
            }

            StopAllLowLevelActions();
        }
        #endregion

        #region Cradle
        public bool StartCradleUp()
        {
            var dataPacket = new DataPacket((byte)Command.CradleOverturningUp, 0x01);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }

        public bool StopCradleUp()
        {
            var dataPacket = new DataPacket((byte)Command.CradleOverturningUp, 0x00);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }

        public bool StartCradleDown()
        {
            var dataPacket = new DataPacket((byte)Command.CradleOverturningDown, 0x01);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }

        public bool StopCradleDown()
        {
            var dataPacket = new DataPacket((byte)Command.CradleOverturningDown, 0x00);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }
        #endregion

        #region Spoon
        public bool SpoonUp()
        {
            if (HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value == false)
            {
                return true;
            }

            var dataPacket = new DataPacket((byte)Command.SpoonUp, 0x01);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }

        public bool SpoonDown()
        {
            if (HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value == false)
            {
                return true;
            }

            var dataPacket = new DataPacket((byte)Command.SpoonDown, 0x01);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }
        #endregion

        #region Alignment
        public bool StartTaskAlignment()
        {
            if (taskAlignmentIsRunning == true)
            {
                return false;
            }

            Console.WriteLine($"[{DateTime.Now}] ElectricDrives.StartTaskAlignment");

            StopAllActions();

            threadAlignment = new Thread(new ThreadStart(AlignmentMaterial));
            taskAlignmentIsRunning = true;
            threadAlignment.Start();

            return true;
        }

        public bool StopTaskAlignment()
        {
            if (taskAlignmentIsRunning == false)
            {
                return false;
            }

            Console.WriteLine($"[{DateTime.Now}] ElectricDrives.StopTaskAlignment");
            taskAlignmentIsRunning = false;

            try
            {
                threadAlignment.Abort();

                try
                {
                    while (threadAlignment.IsAlive)
                    {
                        Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                    }
                }
                catch
                {
                    //--
                }

                threadAlignment = null;
            }
            catch
            {
                //--
            }

            StopAlignmentMotorSide();
            StopAlignmentOperatorSide();

            return true;
        }

        public void StartAlignmentMaterialWithExitCondition()
        {
            Console.WriteLine($"[{DateTime.Now}] Entering:  StartAlignmentMaterialWithExitCondition()");

            manualAlignmentIsRunning = true;

            bool exitCond = false;

            while (exitCond == false && manualAlignmentIsRunning)
            {
                try
                {
                    bool c1 = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellOperatorSide];
                    bool c2 = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMotorSide];

                    if (HighLevel.Configuration.IsLeftMachine)
                    {
                        if (c1 && c2)
                        {
                            //io->virtual_inputs.cradle_alignment_op_side = 0;
                            //io->virtual_inputs.cradle_alignment_mt_side = 1;
                            StopAlignmentOperatorSide();
                            StartAlignmentMotorSide();

                        }
                        else if (!c1 && !c2)
                        {
                            //io->virtual_inputs.cradle_alignment_mt_side = 0;
                            //io->virtual_inputs.cradle_alignment_op_side = 1;
                            StopAlignmentMotorSide();
                            StartAlignmentOperatorSide();
                        }
                        else //if (c1 || c2)
                        {

                            //io->virtual_inputs.cradle_alignment_mt_side = 0;
                            //io->virtual_inputs.cradle_alignment_op_side = 0;
                            StopAlignmentMotorSide();
                            StopAlignmentOperatorSide();

                            exitCond = true;
                        }
                    }
                    else
                    {
                        if (c1 && c2)
                        {
                            //io->virtual_inputs.cradle_alignment_mt_side = 0;
                            //io->virtual_inputs.cradle_alignment_op_side = 1;
                            StopAlignmentMotorSide();
                            StartAlignmentOperatorSide();
                        }
                        else if (!c1 && !c2)
                        {
                            //io->virtual_inputs.cradle_alignment_op_side = 0;
                            //io->virtual_inputs.cradle_alignment_mt_side = 1;
                            StopAlignmentOperatorSide();
                            StartAlignmentMotorSide();
                        }
                        else //if (c1 || c2)
                        {

                            //io->virtual_inputs.cradle_alignment_mt_side = 0;
                            //io->virtual_inputs.cradle_alignment_op_side = 0;
                            StopAlignmentMotorSide();
                            StopAlignmentOperatorSide();

                            exitCond = true;
                        }
                    }

                }
                catch
                {
                    //--
                }

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);// HighLevel.Settings.Root.MachineParameters.WaitRelaysFallingEdge.Value);
            }

            Console.WriteLine($"[{DateTime.Now}] Exiting:  StartAlignmentMaterialWithExitCondition()");
        }

        public void StopAlignmentMaterialWithExitCondition()
        {
            manualAlignmentIsRunning = false;
        }

        private void AlignmentMaterial()
        {
            Console.WriteLine($"[{DateTime.Now}] Entering:  AlignmentMaterial()");

            bool exitCond = false;

            while (exitCond == false && taskAlignmentIsRunning)
            {
                try
                {
                    bool c1 = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellOperatorSide];
                    bool c2 = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMotorSide];

                    if (HighLevel.Configuration.IsLeftMachine)
                    {

                        if (c1 && c2)
                        {
                            //io->virtual_inputs.cradle_alignment_op_side = 0;
                            //io->virtual_inputs.cradle_alignment_mt_side = 1;
                            StopAlignmentOperatorSide();
                            Thread.Sleep(HighLevel.Settings.HighLevel.MachineParameters.WaitRelaysAfterRisingEdge.Value);
                            StartAlignmentMotorSide();

                        }
                        else if (!c1 && !c2)
                        {
                            //io->virtual_inputs.cradle_alignment_mt_side = 0;
                            //io->virtual_inputs.cradle_alignment_op_side = 1;
                            StopAlignmentMotorSide();
                            Thread.Sleep(HighLevel.Settings.HighLevel.MachineParameters.WaitRelaysAfterRisingEdge.Value);
                            StartAlignmentOperatorSide();
                        }
                        else //if (c1 || c2)
                        {

                            //io->virtual_inputs.cradle_alignment_mt_side = 0;
                            //io->virtual_inputs.cradle_alignment_op_side = 0;
                            StopAlignmentMotorSide();
                            Thread.Sleep(HighLevel.Settings.HighLevel.MachineParameters.WaitRelaysAfterRisingEdge.Value);
                            StopAlignmentOperatorSide();

                            //exitCond = true;
                        }
                    }
                    else
                    {
                        if (c1 && c2)
                        {
                            //io->virtual_inputs.cradle_alignment_mt_side = 0;
                            //io->virtual_inputs.cradle_alignment_op_side = 1;
                            StopAlignmentMotorSide();
                            Thread.Sleep(HighLevel.Settings.HighLevel.MachineParameters.WaitRelaysAfterRisingEdge.Value);
                            StartAlignmentOperatorSide();
                        }
                        else if (!c1 && !c2)
                        {
                            //io->virtual_inputs.cradle_alignment_op_side = 0;
                            //io->virtual_inputs.cradle_alignment_mt_side = 1;
                            StopAlignmentOperatorSide();
                            Thread.Sleep(HighLevel.Settings.HighLevel.MachineParameters.WaitRelaysAfterRisingEdge.Value);
                            StartAlignmentMotorSide();
                        }
                        else //if (c1 || c2)
                        {

                            //io->virtual_inputs.cradle_alignment_mt_side = 0;
                            //io->virtual_inputs.cradle_alignment_op_side = 0;
                            StopAlignmentMotorSide();
                            Thread.Sleep(HighLevel.Settings.HighLevel.MachineParameters.WaitRelaysAfterRisingEdge.Value);
                            StopAlignmentOperatorSide();

                            //exitCond = true;
                        }
                    }
                }
                catch
                {
                    // --
                }

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            taskAlignmentIsRunning = false;

            Console.WriteLine($"[{DateTime.Now}] Exiting:  AlignmentMaterial()");
        }
        #endregion

        #region Centering
        private void ThreadAutoCentering()
        {
            ProConsole.WriteLine("[ENTERING] ThreadAutoCentering", ConsoleColor.Green);

            HighLevel.TasksStatus.AutoCenteringActivationStatus = true;

            StopAllActions(true);

            StartAlignmentOperatorSide();

            while (LowLevel.Actions.AlignmentOperatorSide)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            StopAlignmentOperatorSide();
            Thread.Sleep(HighLevel.Settings.HighLevel.MachineParameters.WaitRelaysFallingEdge.Value);

            StartAlignmentMotorSide();
            Thread.Sleep(HighLevel.Settings.HighLevel.MachineParameters.IntervalMotorSideAutoCentering.Value);

            StopAlignmentMotorSide();

            HighLevel.TasksStatus.AutoCenteringActivationStatus = false;

            ProConsole.WriteLine("[EXITING] ThreadAutoCentering", ConsoleColor.Red);
        }

        public bool StartTaskAutoCentering()
        {
            #region OLD_CONDITIONS
            //if (HighLevel.Settings.Root.MachineParameters.AutoCenteringEnabled.Value == 1 &&
            //    LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false &&
            //    HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled)
            #endregion

            if (threadAutoCentering is null || threadAutoCentering.IsAlive == false)
            {
                Console.WriteLine("StartAutoCentering");

                threadAutoCentering = new Thread(new ThreadStart(ThreadAutoCentering));
                threadAutoCentering.Start();

                return true;
            }
            else
            {
                Console.WriteLine($"StartTaskAutoCentering: NOT_STARTED (null:{threadAutoCentering is null} alive:{threadAutoCentering.IsAlive})");
                return false;
            }
        }

        public bool StopTaskAutoCentering()
        {
            if (threadAutoCentering != null && threadAutoCentering.IsAlive)
            {
                Console.WriteLine("StopAutoCentering");

                threadAutoCentering.Abort();
                threadAutoCentering = null;

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Alignment (Motors)
        public bool StopAllLowLevelActions()
        {
            Console.WriteLine("StopAllLowLevelActions");

            var dataPacket = new DataPacket((byte)Command.StopAllActions, 0x00);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }

        public bool StartAlignmentMotorSide()
        {
            if (LowLevel.Actions.AlignmentMotorSide || LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitAlignmentMotorSide])
            {
                return false;
            }

            Console.WriteLine("StartAlignmentMotorSide");

            var dataPacket = new DataPacket((byte)Command.AlignmentMotorSide, 0x01);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }

        public bool StopAlignmentMotorSide()
        {
            if (LowLevel.Actions.AlignmentMotorSide == false)
            {
                return false;
            }

            Console.WriteLine("StopAlignmentMotorSide");

            var dataPacket = new DataPacket((byte)Command.AlignmentMotorSide, 0x00);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }

        public bool StartAlignmentOperatorSide()
        {
            if (LowLevel.Actions.AlignmentOperatorSide || LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitAlignmentOperatorSide])
            {
                return true;
            }

            Console.WriteLine("StartAlignmentOperatorSide");

            var dataPacket = new DataPacket((byte)Command.AlignmentOperatorSide, 0x01);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }

        public bool StopAlignmentOperatorSide()
        {
            if (LowLevel.Actions.AlignmentOperatorSide == false)
            {
                return true;
            }

            Console.WriteLine("StopAlignmentOperatorSide");

            var dataPacket = new DataPacket((byte)Command.AlignmentOperatorSide, 0x00);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }
        #endregion

        #region Titan
        public bool StartTitanUp()
        {
            var dataPacket = new DataPacket((byte)Command.TitanUp, 0x01);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }

        public bool StopTitanUp()
        {
            var dataPacket = new DataPacket((byte)Command.TitanUp, 0x00);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }

        public bool StartTitanDown()
        {
            var dataPacket = new DataPacket((byte)Command.TitanDown, 0x01);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }

        public bool StopTitanDown()
        {
            var dataPacket = new DataPacket((byte)Command.TitanDown, 0x00);
            return Communicator.TrySendDataPacket(dataPacket.Create());
        }
        #endregion
    }
}
