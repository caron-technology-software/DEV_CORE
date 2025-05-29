using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

using ProRob.Extensions.Object;

using Machine.UI.Communication;
using Machine.UI.Controls;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.LowLevel;

using HighLevelControlState = Caron.Cradle.Control.HighLevel.ControlState;

namespace Caron.Cradle.UI
{
    public partial class FormManualOperations : FormCradleBase
    {
        private DateTime LastCutterVelocityUpdate { get; set; } = DateTime.UtcNow;

        public FormManualOperations()
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

            #region Localizations
            mlTitle.Text = Localization.ManualOperations;

            mlCradleJog.Text = Localization.CradleJog;
            mlAlignment.Text = Localization.ManualCradleAlignment;
            mlTitan.Text = Localization.LoadUnloadTitan;
            mlSpoon.Text = Localization.Spoon;
            mlOverturning.Text = Localization.CradleOverturning;
            mlAutoAlignment.Text = Localization.AutoCentering;
            mlCutter.Text = Localization.Cutter;
            mlCutterVelocity.Text = Localization.CutterVelocity;
            #endregion

            #region Buttons
            cbCradleJogACW.StateChangeActivated = false;
            cbCradleJogACW.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            cbCradleJogCW.StateChangeActivated = false;
            cbCradleJogCW.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbAutoCentering.StateChangeActivated = false;
            mbAutoCentering.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbOverturningDown.StateChangeActivated = false;
            mbOverturningDown.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbOverturningUp.StateChangeActivated = false;
            mbOverturningUp.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbSpoonUp.StateChangeActivated = false;
            mbSpoonUp.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbSpoonDown.StateChangeActivated = false;
            mbSpoonDown.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbAlignmentOperatorSide.StateChangeActivated = false;
            mbAlignmentOperatorSide.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbAlignmentMotorSide.StateChangeActivated = false;
            mbAlignmentMotorSide.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbTitanUp.StateChangeActivated = false;
            mbTitanUp.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbTitanDown.StateChangeActivated = false;
            mbTitanDown.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            cbCutOff.StateChangeActivated = false;
            cbCutOff.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbSharpening.StateChangeActivated = true;
            mbSharpening.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;
            #endregion

            #region Cutter Velocity Slider
            mpbsCutterVelocity.PropertyName = Localization.CutterVelocity;
            mpbsCutterVelocity.LoadBackgroundImages(
                global::Caron.Cradle.UI.Properties.Resources.cutter_gray_00,
                new List<Image>()
                {
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_00,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_01,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_02,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_03,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_04,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_05,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_06,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_07,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_08,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_09,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_10,
                });

            mpbsCutterVelocity.MinValue = Supervisor.Control.HighLevel.Settings.HighLevel.MachineParameters.MinCutterVelocity.Value;
            mpbsCutterVelocity.MaxValue = 100;

            mpbsCutterVelocity.ValueChangedEventEnabled = true;
            mpbsCutterVelocity.ValueChanged += MpbsCutterVelocity_ValueChanged;
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

            if (Supervisor.Control.HighLevel.Configuration.IsLeftMachine)
            {
                var location = cbCutOff.Location;
                location.X = 75;
                cbCutOff.Location = location;

                mbSharpening.Visible = false;
            }

            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value == false)
            {
                mpSpoon.Visible = false;
                mlSpoon.Visible = false;
                cbStop.Visible = false;
            }
            #endregion

            #region Functions Enabled
            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value == false)
            {
                panelCuttOff.Visible = false;
                cbCutOff.Visible = false;
                mlCutter.Visible = false;
                mbSharpening.Visible = false;

                mlCutterVelocity.Visible = false;
                mpbsCutterVelocity.Visible = false;
            }

            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.TitanPresence.Value == false)
            {
                mlTitan.Visible = false;
                mpTitan.Visible = false;
            }
            #endregion

            //-------------------------------------------------------
            // ResumeLayout
            //-------------------------------------------------------
            ResumeLayout();

            Task.Run(() =>
            {
                while(Supervisor.IsRunning)
                {
                    if(Supervisor.UI.State == Control.StateUI.ManualOperations)
                    {
                        if (Supervisor.Control.HighLevel.Status.CradleInSync)
                        {
                            Communicator.SetVariable($"working_mode/set_cradle_sync", "value", false);
                        }
                    }

                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                }
            });
        }

        private void FormManualOperations_Load(object sender, EventArgs e)
        {
            //-------------------------------------------------------
            // Events UI Update
            //-------------------------------------------------------
            Supervisor.Events.CutOffEnabledChanged += CutOffEnabledChanged;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                //------------------------------------------------
                // Machine
                //------------------------------------------------
                SyncStatusOnLoad = Supervisor.Control.HighLevel.Status.CradleInSync;

                Communicator.SetVariable($"working_mode/set_cradle_sync", "value", false);
                Communicator.SendHttpGetRequest("tasks_status/alignment/off");

                //------------------------------------------------
                // UI
                //------------------------------------------------
                UpdateCutterVelocity();
                UpdateCutOffButton();
            }
            else
            {
                //------------------------------------------------
                // Machine
                //------------------------------------------------
                Communicator.SetVariable($"working_mode/set_cradle_sync", "value", SyncStatusOnLoad);
                Communicator.SendHttpGetRequest("tasks_status/alignment/on");
            }
        }

        //------------------------------------------------
        // Events
        //------------------------------------------------
        #region Events
        private void MpbsCutterVelocity_ValueChanged(object sender, EventArgs e)
        {
            if ((DateTime.UtcNow - LastCutterVelocityUpdate) > Machine.UI.Constants.Intervals.MinSliderUpdate)
            {
                LastCutterVelocityUpdate = DateTime.UtcNow;

                double value = (double)mpbsCutterVelocity.Value / 100.0;
                var context = Supervisor.Control.HighLevel.WorkingContext.Clone();
                context.Parameters.CutterVelocity = value;

                Communicator.SendHttpPostRequest("working_context", context);
            }
        }

        private void CutOffEnabledChanged(object sender, EventArgs e)
        {
            UpdateCutOffButton();
        }

        #endregion

        //------------------------------------------------
        // Cutter Velocity Slider
        //------------------------------------------------
        #region Cutter Velocity Slider
        private void UpdateCutterVelocity()
        {
            this?.Invoke((MethodInvoker)delegate ()
            {
                float velocity = (float)(Supervisor.Control.HighLevel.WorkingContext.Parameters.CutterVelocity * 100.0);
                mpbsCutterVelocity.SetValueWithoutEvent(velocity);
            });
        }
        #endregion

        //------------------------------------------------
        // Stop Button
        //------------------------------------------------
        #region Stop Button
        private async void CbStop_Click(object sender, EventArgs e)
        {
            Communicator.SendHttpGetRequest("cutter", "stop");
            Communicator.SendLowLevelControlCommand("stop");

            await Task.Run(() =>
            {
                Task.Run(() =>
                {
                    this?.Invoke((MethodInvoker)delegate ()
                    {
                        cbStop.PulseButton(150, 4);
                    });
                });

                //Reset buttons state
                mbTitanUp.Active = false;
                mbTitanDown.Active = false;

                mbOverturningDown.Active = false;
                mbOverturningUp.Active = false;
            });
        }
        #endregion

        //------------------------------------------------
        // Spoon
        //------------------------------------------------
        #region Spoon
        private void mbSpoonUp_MouseDown(object sender, MouseEventArgs e)
        {
            mbSpoonUp.PulseButton();
            Communicator.SendLowLevelControlCommand("spoon_up");
        }

        private void mbSpoonDown_MouseDown(object sender, MouseEventArgs e)
        {
            mbSpoonDown.PulseButton();
            Communicator.SendLowLevelControlCommand("spoon_down");
        }
        #endregion

        //------------------------------------------------
        // Overturning
        //------------------------------------------------
        #region Overturning
        private void mbOverturningUp_MouseDown(object sender, MouseEventArgs e)
        {
            //---------------------------
            // CheckOverturningConditions
            //---------------------------
            bool cond = true; //Supervisor.CradleHelper.CheckOverturningConditions();

            if (cond)
            {
                mbOverturningUp.Active = true;
                Communicator.SendLowLevelControlCommand("start_cradle_up");
            }
            else
            {
                MachineMessageBox.Show(Localization.Warning, Localization.MaterialPresence);
            }
        }

        private void mbOverturningUp_MouseUp(object sender, MouseEventArgs e)
        {
            mbOverturningUp.Active = false;
            Communicator.SendLowLevelControlCommand("stop_cradle_up");
        }

        private void mbOverturningDown_MouseDown(object sender, MouseEventArgs e)
        {
            //---------------------------
            // CheckOverturningConditions
            //---------------------------
            bool cond = true; //Supervisor.CradleHelper.CheckOverturningConditions();

            if (cond)
            {
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
            mbOverturningDown.Active = false;
            Communicator.SendLowLevelControlCommand("stop_cradle_down");
        }
        #endregion

        //------------------------------------------------
        // Alignment
        //------------------------------------------------
        #region Alignment
        private void mbAlignmentOperatorSide_MouseUp(object sender, MouseEventArgs e)
        {
            mbAlignmentOperatorSide.Active = false;
            Communicator.SendLowLevelControlCommand("stop_alignment_op_side");
        }

        private void mbAlignmentOperatorSide_MouseDown(object sender, MouseEventArgs e)
        {
            mbAlignmentOperatorSide.Active = true;
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
            Communicator.SendLowLevelControlCommand("start_alignment_mt_side");
        }
        #endregion

        //------------------------------------------------
        // Titan
        //------------------------------------------------
        #region Titan
        private void mbTitanUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.TitanPresence.Value == false)
            {
                MachineMessageBox.Show(Localization.Warning, Localization.TitanNotPresent);
                return;
            }

            Communicator.SendLowLevelControlCommand("start_titan_up");
            mbTitanUp.Active = true;
        }

        private void mbTitanUp_MouseUp(object sender, MouseEventArgs e)
        {
            Communicator.SendLowLevelControlCommand("stop_titan_up");
            mbTitanUp.Active = false;
        }

        private void mbTitanDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.TitanPresence.Value == false)
            {
                MachineMessageBox.Show(Localization.Warning, Localization.TitanNotPresent);
                return;
            }

            Communicator.SendLowLevelControlCommand("start_titan_down");
            mbTitanDown.Active = true;
        }

        private void mbTitanDown_MouseUp(object sender, MouseEventArgs e)
        {
            Communicator.SendLowLevelControlCommand("stop_titan_down");
            mbTitanDown.Active = false;
        }
        #endregion

        //------------------------------------------------
        // Cradle Jog
        //------------------------------------------------
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
               (hcs == HighLevelControlState.CradleJogManualOperations && (CradleJogManualOperationsSubState)cs.HighLevelControlSubState != CradleJogManualOperationsSubState.WaitExit) ||
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
            if (hcs == HighLevelControlState.CradleJogManualOperations
                && (CradleJogManualOperationsSubState)cs.HighLevelControlSubState == CradleJogManualOperationsSubState.WaitExit)
            {
                Communicator.SetHighLevelControlState("manual_operations");
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
                    Communicator.SendHttpGetRequest("cradle/start_jog_manual_operations/cw");
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
                Communicator.SendHttpGetRequest("cradle/stop_jog_manual_operations");
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
               (hcs == HighLevelControlState.CradleJogManualOperations && (CradleJogManualOperationsSubState)cs.HighLevelControlSubState != CradleJogManualOperationsSubState.WaitExit) ||
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
                Communicator.SetHighLevelControlState("manual_operations");
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
                    Communicator.SendHttpGetRequest("cradle/start_jog_manual_operations/acw");
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
                Communicator.SendHttpGetRequest("cradle/stop_jog_manual_operations");
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

        //------------------------------------------------
        // Autoalignment
        //------------------------------------------------
        #region AutoAlignment
        private void mbAutoAlignment_Click(object sender, EventArgs e)
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

        //------------------------------------------------
        // Cut Off
        //------------------------------------------------
        #region Cut Off

        private volatile bool cutOffButtonReleased = false;

        private async void CbCutOff_MouseDown(object sender, MouseEventArgs e)
        {
            if ((DateTime.UtcNow - lastTimestampButtonJogPressed) < TimeSpan.FromSeconds(1))
            {
                return;
            }

            if (Supervisor.Control.HighLevel.Status.HighLevelControlState == HighLevelControlState.CradleJog ||
                Supervisor.Control.HighLevel.Status.HighLevelControlState == HighLevelControlState.CradleJogManualOperations ||
                Supervisor.Control.HighLevel.Status.HighLevelControlState == HighLevelControlState.CradleJogLoadUnload ||

                Supervisor.Control.HighLevel.Status.JogState != JogState.Stopped ||

                Supervisor.Control.LowLevel.Info.MachineState == (byte)Control.LowLevel.ControlState.CradleJog)
            {
                return;
            }

            cutOffButtonReleased = false;

            await Task<int>.Run(() =>
            {
                Thread.Sleep(Machine.UI.Constants.Intervals.CutOffMinButtonInterval);

                if (cutOffButtonReleased == false)
                {
                    if (Supervisor.Control.HighLevel.Status.CutOffEnabled)
                    {
                        if (Supervisor.Control.HighLevel.Status.SharpeningEnabled)
                        {
                            Communicator.SetHighLevelControlState("sharpening");
                        }
                        else
                        {
                            Communicator.SetHighLevelControlState("cutoff");
                        }
                    }
                }
            });
        }

        private void CbCutOff_MouseUp(object sender, MouseEventArgs e)
        {
            cutOffButtonReleased = true;
        }

        private void UpdateCutOffButton()
        {
            this?.Invoke((MethodInvoker)delegate ()
            {
                try
                {
                    if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value == false)
                    {
                        cbCutOff.Enabled = false;
                        cbStop.Enabled = false;
                        cbCutOff.Active = false;
                        cbCutOff.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cut_off_disabled;
                        return;
                    }

                    //Cut Off
                    bool c1 = Supervisor.Control.LowLevel.Info.MachineState == (byte)Control.LowLevel.ControlState.CutOff;
                    bool c2 = Supervisor.Control.HighLevel.Status.CutOffEnabled;

                    cbStop.Enabled = true;

                    if (c1 == true)
                    {
                        cbCutOff.Enabled = false;
                        cbCutOff.Active = true;
                        cbCutOff.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cut_off_green;
                        cbCutOff.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cut_off_disabled;
                    }
                    else
                    {
                        if (c2)
                        {
                            cbCutOff.Enabled = true;
                            cbCutOff.Active = false;
                            cbCutOff.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cut_off_disabled;
                            cbCutOff.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cut_off_gray;
                        }
                        else
                        {
                            cbCutOff.Enabled = false;
                            cbCutOff.Active = false;
                            cbCutOff.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cut_off_disabled;
                            cbCutOff.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cut_off_disabled;
                        }
                    }

                    //------------------------------
                    // Sharpening
                    //------------------------------
                    if (Supervisor.Control.HighLevel.Status.SharpeningEnabled)
                    {
                        mbSharpening.Active = true;
                    }
                    else
                    {
                        mbSharpening.Active = false;
                    }
                }
                catch
                {
                    //--
                }
            });
        }
        #endregion

        #region Sharpening
        private void MbSharpening_Click(object sender, EventArgs e)
        {
            //Logica apperentemente inversa in quanto il cambio di stato del pulsante avviene dopo la callback
            if (mbSharpening.Active)
            {
                Communicator.SendHttpGetRequest("cutter/sharpening/disable");
            }
            else
            {
                Communicator.SendHttpGetRequest("cutter/sharpening/enable");
            }
        }
        #endregion
    }
}
