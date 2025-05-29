using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

using ProRob;
using ProRob.Extensions;

using Machine.UI.Communication;
using Machine.UI.Controls;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.LowLevel;

using HighLevelControlState = Caron.Cradle.Control.HighLevel.ControlState;

namespace Caron.Cradle.UI
{
    public partial class FormLoadUnload : FormCradleBase
    {
        private DateTime lastCradleRewindMouseUpEvent = DateTime.UtcNow;
        private DateTime timestampPageLoad = DateTime.UtcNow;

        public FormLoadUnload()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            //-------------------------------------------------------
            // SuspendLayout
            //-------------------------------------------------------
            SuspendLayout();

            cbStop.StateChangeActivated = false;

            #region Localization
            mlAutoAligment.Text = Localization.AutoCentering;
            mlAlignment.Text = Localization.ManualCradleAlignment;
            mlOverturning.Text = Localization.CradleOverturning;
            mlTitan.Text = Localization.LoadUnloadTitan;
            mlCradleJog.Text = Localization.CradleJog;
            mlRewind.Text = Localization.FastRewind;
            #endregion

            #region Buttons
            mbTitanUp.StateChangeActivated = false;
            mbTitanUp.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbTitanDown.StateChangeActivated = false;
            mbTitanDown.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbOverturningDown.StateChangeActivated = false;
            mbOverturningDown.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;


            mbOverturningUp.StateChangeActivated = false;
            mbOverturningUp.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;


            mbAlignmentOperatorSide.StateChangeActivated = false;
            mbAlignmentOperatorSide.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;


            mbAlignmentMotorSide.StateChangeActivated = false;
            mbAlignmentMotorSide.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            cbCradleJogACW.StateChangeActivated = false;
            cbCradleJogACW.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;


            cbCradleJogCW.StateChangeActivated = false;
            cbCradleJogCW.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbRewind.StateChangeActivated = false;
            mbRewind.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbAutoCentering.StateChangeActivated = false;
            mbRewind.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            #endregion

            #region Machine Configuration (Icons)
            if (Supervisor.Control.HighLevel.Configuration.IsLeftMachine)
            {
                Bitmap bmp;

                bmp = global::Caron.Cradle.UI.Properties.Resources.alignement_motor_side_pc_green_SX;
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                mbAlignmentMotorSide.ActiveBackgroundImage = (Bitmap)bmp.Clone();

                bmp = global::Caron.Cradle.UI.Properties.Resources.alignement_motor_side_pc_gray_SX;
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                mbAlignmentMotorSide.InactiveBackgroundImage = (Bitmap)bmp.Clone();

                bmp = global::Caron.Cradle.UI.Properties.Resources.alignement_operator_side_pc_green_SX;
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                mbAlignmentOperatorSide.ActiveBackgroundImage = (Bitmap)bmp.Clone();

                bmp = global::Caron.Cradle.UI.Properties.Resources.alignement_operator_side_pc_gray_SX;
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                mbAlignmentOperatorSide.InactiveBackgroundImage = (Bitmap)bmp.Clone();

                mbOverturningUp.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_cw_green_SX;
                mbOverturningUp.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_cw_gray_SX;

                mbOverturningDown.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_acw_green_SX;
                mbOverturningDown.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_acw_gray_SX;

                mbTitanUp.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.titan_up_green_SX;
                mbTitanUp.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.titan_up_gray_SX;

                mbTitanDown.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.titan_down_green_SX;
                mbTitanDown.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.titan_down_gray_SX;

                mbAutoCentering.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.centering_ex_in_green_SX;
                mbAutoCentering.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.centering_ex_in_gray_SX;
            }
            else
            {
                Bitmap bmp;

                bmp = global::Caron.Cradle.UI.Properties.Resources.alignement_motor_side_pc_green_DX;
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                mbAlignmentMotorSide.ActiveBackgroundImage = (Bitmap)bmp.Clone();

                bmp = global::Caron.Cradle.UI.Properties.Resources.alignement_motor_side_pc_gray_DX;
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                mbAlignmentMotorSide.InactiveBackgroundImage = (Bitmap)bmp.Clone();

                bmp = global::Caron.Cradle.UI.Properties.Resources.alignement_operator_side_pc_green_DX;
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                mbAlignmentOperatorSide.ActiveBackgroundImage = (Bitmap)bmp.Clone();

                bmp = global::Caron.Cradle.UI.Properties.Resources.alignement_operator_side_pc_gray_DX;
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                mbAlignmentOperatorSide.InactiveBackgroundImage = (Bitmap)bmp.Clone();

                mbOverturningUp.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_cw_green_DX;
                mbOverturningUp.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_cw_gray_DX;

                mbOverturningDown.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_acw_green_DX;
                mbOverturningDown.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_acw_gray_DX;

                mbTitanUp.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.titan_up_green_DX;
                mbTitanUp.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.titan_up_gray_DX;

                mbTitanDown.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.titan_down_green_DX;
                mbTitanDown.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.titan_down_gray_DX;

                mbAutoCentering.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.centering_ex_in_green_DX;
                mbAutoCentering.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.centering_ex_in_gray_DX;
            }

            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.TitanPresence.Value == false)
            {
                mlTitan.Visible = false;
                mpTitan.Visible = false;
            }
            #endregion

            //-------------------------------------------------------
            // Mirror Form's controls
            //-------------------------------------------------------
            if (Supervisor.Control.HighLevel.Configuration.IsRightMachine)
            {
                Machine.UI.FormsControlsHelper.MirrorAllControls(this);

                //Scambio locazione pulsanti
                var l1 = mbOverturningUp.Location;
                var l2 = mbOverturningDown.Location;
                mbOverturningUp.Location = l2;
                mbOverturningDown.Location = l1;
            }

            //-------------------------------------------------------
            // ResumeLayout
            //-------------------------------------------------------
            ResumeLayout();
        }

        private void FormLoadUnload_Load(object sender, EventArgs e)
        {
            //----------------------------------
            // Events
            //----------------------------------
            Supervisor.Events.TitanLimitChanged += TitanLimitReached;
            Supervisor.Events.OverturningLimitChanged += OverturningLimitChanged;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
                timestampPageLoad = DateTime.UtcNow;

                SyncStatusOnLoad = Supervisor.Control.HighLevel.Status.CradleInSync;
                Communicator.SetVariable($"working_mode/set_cradle_sync", "value", false);

                Communicator.SendHttpGetRequest("tasks_status/alignment", "?value=false");
                Communicator.SendLowLevelControlCommand("spoon_up");

                Autocentering();

                //-----------------------------------
                // Reset pulsanti
                //-----------------------------------
                mbOverturningDown.Active = false;
                mbOverturningUp.Enabled = true;

                mbOverturningUp.Active = false;
                mbOverturningDown.Enabled = true;

                mbTitanUp.Active = false;
                mbTitanUp.Enabled = true;

                mbTitanDown.Active = false;
                mbTitanDown.Enabled = true;
            }
            else
            {
                //-----------------------------------
                // Attesa ritorno fase di rewind
                //-----------------------------------
                while (Supervisor.Control.HighLevel.Status.HighLevelControlState == Control.HighLevel.ControlState.CradleJogLoadUnload)
                {
                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                }

                Communicator.SetVariable($"working_mode/set_cradle_sync", "value", SyncStatusOnLoad);
                Communicator.SendHttpGetRequest("tasks_status/alignment", "?value=true");

                long ms = (long)(DateTime.UtcNow - timestampPageLoad).TotalMilliseconds;

                Communicator.SendHttpGetRequest("working/add_load_unload_time", $"?milliseconds={ms}");
            }

            base.OnVisibleChanged(e);
        }

        #region Titan
        #region Logic
        private void TitanLimitReached(object sender, EventArgs e)
        {
            if (mbTitanUp.Active)
            {
                HandleButtonTitanUpReleased();
            }
            else if (mbTitanDown.Active)
            {
                HandleButtonTitanDownReleased();
            }
        }

        private bool IsTitanInFinalStage()
        {
            return Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.TitanLimit];
        }

        private bool IsTitanInFirstStage()
        {
            return !IsTitanInFinalStage();
        }

        private bool IsCradleInLoadPosition()
        {
            bool l1 = Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningMotorSideUnload];
            bool l2 = Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningOperatorSideUnload];

            return l1 || l2;
        }
        #endregion

        #region Buttons Handles
        private void HandleButtonTitanUpPressed()
        {
            Console.WriteLine("cbTitanUp_Pressed");

            if (IsTitanInFinalStage())
            {
                if (IsCradleInLoadPosition())
                {
                    //Impulsive
                    mbTitanUp.Active = true;
                    Communicator.SendLowLevelControlCommand("start_titan_up");
                }
            }
            else
            {
                //Automatic
                mbTitanUp.Active = true;
                Communicator.SendLowLevelControlCommand("start_titan_up");
            }
        }

        private void HandleButtonTitanUpReleased()
        {
            Console.WriteLine("cbTitanUp_Released");

            if (IsTitanInFinalStage())
            {
                //Impulsive
                mbTitanUp.Active = false;
                Communicator.SendLowLevelControlCommand("stop_titan_up");
            }
            else
            {
                //Automatic
                mbTitanUp.Active = true;
            }
        }

        private void HandleButtonTitanDownPressed()
        {
            Console.WriteLine("cbTitanDown_Pressed");

            if (IsTitanInFinalStage())
            {
                //Automatic
                mbTitanDown.Active = true;
                Communicator.SendLowLevelControlCommand("start_titan_down");
            }
            else
            {
                //Impulsive
                mbTitanDown.Active = true;
                Communicator.SendLowLevelControlCommand("start_titan_down");
            }
        }

        private void HandleButtonTitanDownReleased()
        {
            Console.WriteLine("cbTitanDown_MouseUp");

            if (IsTitanInFinalStage())
            {
                //Automatic
                mbTitanDown.Active = true;
            }
            else
            {
                //Impulsive
                mbTitanDown.Active = false;
                Communicator.SendLowLevelControlCommand("stop_titan_down");
            }
        }
        #endregion

        #region Callbacks
        private void mbTitanUp_Pressed(object sender, MouseEventArgs e)
        {
            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.TitanPresence.Value == false)
            {
                MachineMessageBox.Show(Localization.Warning, Localization.TitanNotPresent);
                return;
            }

            if (Supervisor.Control.HighLevel.Settings.HighLevel.MachineParameters.AutoTitanHandling.Value == true)
            {
                HandleButtonTitanUpPressed();
            }
            else
            {
                Communicator.SendLowLevelControlCommand("start_titan_up");
                mbTitanUp.Active = true;
            }
        }

        private void mbTitanUp_Released(object sender, MouseEventArgs e)
        {
            if (Supervisor.Control.HighLevel.Settings.HighLevel.MachineParameters.AutoTitanHandling.Value == true)
            {
                HandleButtonTitanUpReleased();
            }
            else
            {
                Communicator.SendLowLevelControlCommand("stop_titan_up");
                mbTitanUp.Active = false;
            }
        }

        private void mbTitanDown_Pressed(object sender, MouseEventArgs e)
        {
            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.TitanPresence.Value == false)
            {
                MachineMessageBox.Show(Localization.Warning, Localization.TitanNotPresent);
                return;
            }

            if (Supervisor.Control.HighLevel.Settings.HighLevel.MachineParameters.AutoTitanHandling.Value == true)
            {
                HandleButtonTitanDownPressed();
            }
            else
            {
                Communicator.SendLowLevelControlCommand("start_titan_down");
                mbTitanDown.Active = true;
            }
        }

        private void mbTitanDown_Released(object sender, MouseEventArgs e)
        {
            if (Supervisor.Control.HighLevel.Settings.HighLevel.MachineParameters.AutoTitanHandling.Value == true)
            {
                HandleButtonTitanDownReleased();
            }
            else
            {
                Communicator.SendLowLevelControlCommand("stop_titan_down");
                mbTitanDown.Active = false;
            }
        }
        #endregion
        #endregion

        #region Overturning
        private void OverturningLimitChanged(object sender, EventArgs e)
        {
            bool limitLoad = Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningMotorSideLoad] ||
                             Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningOperatorSideLoad];

            bool limitUnload = Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningMotorSideUnload] ||
                               Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningOperatorSideUnload];

            if (limitLoad)
            {
                mbOverturningUp.Enabled = true;
                mbOverturningUp.Active = false;

                mbOverturningDown.Enabled = true;
                mbOverturningDown.Active = false;
            }

            if (limitUnload)
            {
                mbOverturningUp.Enabled = true;
                mbOverturningUp.Active = false;

                mbOverturningDown.Enabled = true;
                mbOverturningDown.Active = false;
            }
        }

        private void mbOverturningUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (Supervisor.CradleHelper.CheckIfCutterIsOutOfPositionAndShowPopUp())
            {
                return;
            }

            if (Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningMotorSideLoad])
            {
                return;
            }

            //---------------------------
            // CheckOverturningConditions
            //---------------------------
            bool cond = true; // Supervisor.CradleHelper.CheckOverturningConditions();

            if (cond)
            {
                mbOverturningUp.Active = true;
                mbOverturningDown.Enabled = false;
                Communicator.SendLowLevelControlCommand("start_cradle_up");
            }
            else
            {
                MachineMessageBox.Show(Localization.Warning, Localization.MaterialPresence);
            }
        }

        private void mbOverturningUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (Supervisor.Control.HighLevel.Settings.HighLevel.MachineParameters.AutoCradleOverturningHandling.Value == false)
            {
                Communicator.SendLowLevelControlCommand("stop_cradle_up");

                mbOverturningUp.Active = false;
                mbOverturningDown.Enabled = true;
            }
        }

        private void mbOverturningDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (Supervisor.CradleHelper.CheckIfCutterIsOutOfPositionAndShowPopUp())
            {
                return;
            }

            if (Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningMotorSideUnload])
            {
                return;
            }

            //---------------------------
            // CheckOverturningConditions
            //---------------------------
            bool cond = Supervisor.CradleHelper.CheckOverturningConditions();

            if (cond)
            {
                mbOverturningUp.Enabled = false;
                mbOverturningDown.Active = true;
                Communicator.SendLowLevelControlCommand("start_cradle_down");
            }
            else
            {
                MachineMessageBox.Show(Localization.Warning, Localization.MaterialPresence);
            }
        }

        private void mbOverturningDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (Supervisor.Control.HighLevel.Settings.HighLevel.MachineParameters.AutoCradleOverturningHandling.Value == false)
            {
                Communicator.SendLowLevelControlCommand("stop_cradle_down");

                mbOverturningDown.Active = false;
                mbOverturningUp.Enabled = true;
            }
        }

        #endregion

        #region Alignment
        private void mbAlignmentOperatorSide_MouseUp(object sender, MouseEventArgs e)
        {
            mbAlignmentOperatorSide.Active = false;
            Communicator.SendLowLevelControlCommand("stop_alignment_op_side");
        }

        private void mbAlignmentOperatorSide_MouseDown(object sender, MouseEventArgs e)
        {
            mbAlignmentOperatorSide.Active = true;
            Communicator.SendLowLevelControlCommand("stop_task_alignment");
            Communicator.SendLowLevelControlCommand("start_alignment_op_side");
        }

        private void mbAlignmentMotorSide_MouseUp(object sender, MouseEventArgs e)
        {
            mbAlignmentMotorSide.Active = false;
            Communicator.SendLowLevelControlCommand("stop_alignment_mt_side");
        }

        private void mbAlignmentMotorSide_MouseDown(object sender, MouseEventArgs e)
        {
            mbAlignmentMotorSide.Active = true;
            Communicator.SendLowLevelControlCommand("stop_task_alignment");
            Communicator.SendLowLevelControlCommand("start_alignment_mt_side");
        }
        #endregion

        #region AutoCentering
        private void Autocentering()
        {
            if (Supervisor.CradleHelper.CheckAutoCenteringConditions())
            {
                mbAlignmentMotorSide.Enabled = false;
                mbAlignmentOperatorSide.Enabled = false;

                Communicator.SendLowLevelControlCommand("autocentering");

                mbAlignmentMotorSide.Enabled = true;
                mbAlignmentOperatorSide.Enabled = true;
            }
        }
        #endregion

        #region Rewinding
        private void mbRewind_MouseDown(object sender, MouseEventArgs e)
        {
            if (Supervisor.Control.HighLevel.TasksStatus.AutoCenteringActivationStatus)
            {
                return;
            }

            if (Supervisor.CradleHelper.CheckIfCutterIsOutOfPositionAndShowPopUp())
            {
                return;
            }

            if (Supervisor.CradleHelper.CheckCradleIsOutOfPositionAndShowPopUp())
            {
                return;
            }

            if (Supervisor.CradleHelper.CheckRewindConditions() == false)
            {
                MachineMessageBox.Show(Localization.Warning, Localization.FastRewindLockWithCradleOpen);
                return;
            }

            if ((DateTime.UtcNow - lastCradleRewindMouseUpEvent) < Machine.UI.Constants.Intervals.IntervalBetweenCradleJogs)
            {
                return;
            }

            if (mbRewind.Active)
            {
                return;
            }

            if (Supervisor.Control.HighLevel.Status.HighLevelControlState != Control.HighLevel.ControlState.LoadUnload)
            {
                return;
            }

            mbRewind.Active = true;
            Communicator.SendHttpGetRequest("cradle/start_rewind");
        }

        private void mbRewind_MouseUp(object sender, MouseEventArgs e)
        {
            Communicator.SendHttpGetRequest("cradle/stop_rewind");

            mbRewind.Enabled = false;

            Thread.Sleep(Machine.UI.Constants.Intervals.IntervalAfterMouseUpEvent);

            mbRewind.Enabled = true;
            mbRewind.Active = false;

            lastCradleRewindMouseUpEvent = DateTime.UtcNow;
        }
        #endregion

        #region Cradle Jog
        private DateTime lastTimestampButtonJogPressed = DateTime.UtcNow;

        private volatile bool taskJogCWisRunning = false;
        private volatile Stopwatch swCradleJogCW = new Stopwatch();
        private volatile bool jogCWStarted = false;
        private volatile bool buttonCWReleased = false;

        private volatile bool taskJogACWisRunning = false;
        private volatile Stopwatch swCradleJogACW = new Stopwatch();
        private volatile bool jogACWStarted = false;
        private volatile bool buttonACWReleased = false;

        #region CW
        private void CbCradleJogCW_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"ENTER CbCradleJogCW_MouseDown {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");

            if (Supervisor.Control.HighLevel.TasksStatus.AutoCenteringActivationStatus)
            {
                return;
            }

            if (Supervisor.CradleHelper.CheckIfCutterIsOutOfPositionAndShowPopUp())
            {
                return;
            }

            if (Supervisor.CradleHelper.CheckCradleIsOutOfPositionAndShowPopUp())
            {
                return;
            }

            //-------------------------------------------
            // Exit conditions
            //-------------------------------------------
            var cs = Supervisor.Control.HighLevel.Status;
            var hcs = Supervisor.Control.HighLevel.Status.HighLevelControlState;

            if ((DateTime.UtcNow - lastTimestampButtonJogPressed) < Machine.UI.Constants.Intervals.IntervalBetweenCradleJogs ||
               (hcs == HighLevelControlState.CradleJogLoadUnload && (CradleJogLoadUnloadSubState)cs.HighLevelControlSubState != CradleJogLoadUnloadSubState.WaitExit) ||
               hcs == HighLevelControlState.CutOff ||
               hcs == HighLevelControlState.CradleRewind ||
               jogCWStarted || jogACWStarted || cbCradleJogCW.Active || cbCradleJogACW.Active || taskJogCWisRunning || taskJogACWisRunning)
            {
                lastTimestampButtonJogPressed = DateTime.UtcNow;
                return;
            }

            //--------------------------------------------
            // Routing
            //--------------------------------------------
            if (hcs == HighLevelControlState.CradleJogLoadUnload && (CradleJogLoadUnloadSubState)cs.HighLevelControlSubState == CradleJogLoadUnloadSubState.WaitExit)
            {
                Communicator.SetHighLevelControlState("load_unload");
            }

            lastTimestampButtonJogPressed = DateTime.UtcNow;
            jogCWStarted = false;
            buttonCWReleased = false;

            //cbCradleSync.Enabled = false;
            cbCradleJogACW.Enabled = false;
            cbCradleJogCW.Active = true;

            TimeSpan delayStartJog = Machine.UI.Constants.Intervals.DelayJogIfCradleIsStationary;
            if (Math.Abs(Supervisor.Control.LowLevel.Axes.Cradle.Velocity) > Machine.Constants.Kinematics.MinVelocityToConsiderDeviceInMovement)
            {
                delayStartJog = Machine.UI.Constants.Intervals.DelayJogIfCradleInMovement;
            }

            taskJogCWisRunning = true;

            Task.Run(() =>
            {
                swCradleJogCW.Restart();

                while (buttonCWReleased == false && swCradleJogCW.Elapsed < delayStartJog)
                {
                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                }

                //Verifico il rilascio del pulsante
                if (buttonCWReleased == false)
                {
                    Communicator.SendHttpGetRequest("cradle/start_jog_load_unload/cw");
                    jogCWStarted = true;
                }

                Console.WriteLine($"EXIT CW Thread {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");

                taskJogCWisRunning = false;
            });

            Console.WriteLine($"EXIT CbCradleJogCW_MouseDown {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");
        }

        private void CbCradleJogCW_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"ENTER CbCradleJogCW_MouseUp {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");

            buttonCWReleased = true;

            while (taskJogCWisRunning)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            if (jogCWStarted)
            {
                Communicator.SendHttpGetRequest("cradle/stop_jog_load_unload");
                jogCWStarted = false;
            }

            cbCradleJogACW.Enabled = true;
            //cbCradleSync.Enabled = true;
            cbCradleJogCW.Active = false;

            lastTimestampButtonJogPressed = DateTime.UtcNow;

            Console.WriteLine($"EXIT CbCradleJogCW_MouseUp {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");
        }
        #endregion

        #region ACW
        private void CbCradleJogACW_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"ENTER CbCradleJogACW_MouseDown {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");

            if (Supervisor.CradleHelper.CheckIfCutterIsOutOfPositionAndShowPopUp())
            {
                return;
            }

            if (Supervisor.CradleHelper.CheckCradleIsOutOfPositionAndShowPopUp())
            {
                return;
            }

            //-------------------------------------------
            // Exit conditions
            //-------------------------------------------
            var cs = Supervisor.Control.HighLevel.Status;
            var hcs = Supervisor.Control.HighLevel.Status.HighLevelControlState;

            if ((DateTime.UtcNow - lastTimestampButtonJogPressed) < Machine.UI.Constants.Intervals.IntervalBetweenCradleJogs ||
               (hcs == HighLevelControlState.CradleJogLoadUnload && (CradleJogLoadUnloadSubState)cs.HighLevelControlSubState != CradleJogLoadUnloadSubState.WaitExit) ||
               hcs == HighLevelControlState.CutOff ||
               hcs == HighLevelControlState.CradleRewind ||
               jogCWStarted || jogACWStarted || cbCradleJogCW.Active || cbCradleJogACW.Active || taskJogCWisRunning || taskJogACWisRunning)
            {
                lastTimestampButtonJogPressed = DateTime.UtcNow;
                return;
            }

            //--------------------------------------------
            // Routing
            //--------------------------------------------
            if (hcs == HighLevelControlState.CradleJog && (CradleJogSubState)cs.HighLevelControlSubState == CradleJogSubState.WaitExit)
            {
                Communicator.SetHighLevelControlState("load_unload");
            }

            lastTimestampButtonJogPressed = DateTime.UtcNow;
            jogACWStarted = false;
            buttonACWReleased = false;

            cbCradleJogCW.Enabled = false;
            //cbCradleSync.Enabled = false;
            cbCradleJogACW.Active = true;

            TimeSpan delayStartJog = Machine.UI.Constants.Intervals.DelayJogIfCradleIsStationary;
            if (Math.Abs(Supervisor.Control.LowLevel.Axes.Cradle.Velocity) > Machine.Constants.Kinematics.MinVelocityToConsiderDeviceInMovement)
            {
                delayStartJog = Machine.UI.Constants.Intervals.DelayJogIfCradleInMovement;
            }

            taskJogACWisRunning = true;

            Task.Run(() =>
            {
                swCradleJogACW.Restart();

                while (buttonACWReleased == false && swCradleJogACW.Elapsed < delayStartJog)
                {
                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                }

                //Verifico il rilascio del pulsante
                if (buttonACWReleased == false)
                {
                    Communicator.SendHttpGetRequest("cradle/start_jog_load_unload/acw");
                    jogACWStarted = true;
                }

                taskJogACWisRunning = false;
                Console.WriteLine($"EXIT ACW Thread {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");
            });

            Console.WriteLine($"EXIT CbCradleJogACW_MouseDown {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");
        }

        private void CbCradleJogACW_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"ENTER CbCradleJogACW_MouseUp {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");

            buttonACWReleased = true;

            while (taskJogACWisRunning)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            if (jogACWStarted)
            {
                Communicator.SendHttpGetRequest("cradle/stop_jog_load_unload");
                jogACWStarted = false;
            }

            cbCradleJogCW.Enabled = true;
            //cbCradleSync.Enabled = true;
            cbCradleJogACW.Active = false;

            lastTimestampButtonJogPressed = DateTime.UtcNow;

            Console.WriteLine($"EXIT CbCradleJogACW_MouseUp {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");
        }
        #endregion
        #endregion

        #region Stop
        private void cbStop_Click(object sender, EventArgs e)
        {
            Communicator.SendLowLevelControlCommand("stop");

            Task.Run(() =>
            {
                this?.Invoke((MethodInvoker)delegate ()
                {
                    cbStop.PulseButton(150, 4);
                });
            });

            //-------------------------------------
            //Reset buttons state
            //-------------------------------------
            mbTitanUp.Active = false;
            mbTitanDown.Active = false;

            mbOverturningUp.Enabled = true;
            mbOverturningUp.Active = false;

            mbOverturningDown.Enabled = true;
            mbOverturningDown.Active = false;
        }
        #endregion

        #region Auto Centering
        private void mbAutoCentering_Click(object sender, EventArgs e)
        {
            if (Supervisor.CradleHelper.CheckIfCutterIsOutOfPositionAndShowPopUp())
            {
                return;
            }

            if (Supervisor.CradleHelper.CheckCradleIsOutOfPositionAndShowPopUp())
            {
                return;
            }

            if (Supervisor.CradleHelper.CheckAutoAlignmentConditions() == false)
            {
                return;
            }

            mbAutoCentering.Active = true;

            mbAlignmentMotorSide.Enabled = false;
            mbAlignmentOperatorSide.Enabled = false;

            Communicator.SendLowLevelControlCommand("material_alignment");

            mbAutoCentering.Active = false;

            mbAlignmentMotorSide.Enabled = true;
            mbAlignmentOperatorSide.Enabled = true;
        }
        #endregion

        private void MbRewind_Click(object sender, EventArgs e)
        {

        }
    }
}
