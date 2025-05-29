using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;

using ProRob.Extensions.Object;

using Machine;
using Machine.UI.Communication;
using Machine.UI.Controls;
using Machine.Utility;

using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel;

using HighLevelControlState = Caron.Cradle.Control.HighLevel.ControlState;

namespace Caron.Cradle.UI
{
    public partial class FormDashboard : FormCradleBase
    {
        private DateTime LastCradleScalingFactorUpdate { get; set; } = DateTime.UtcNow;
        private DateTime LastCutterVelocityUpdate { get; set; } = DateTime.UtcNow;

        //GPIx243
        private bool stopTaskCheckLicenceViolation = false;
        //GPFx243

        //GPIx25
        private bool stopTaskStopButton = false;
        //GPFx25

        public FormDashboard()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            cbCutOff.Size = new Size(Machine.UI.Constants.Sizes.Dashboard.ButtonSize, Machine.UI.Constants.Sizes.Dashboard.ButtonSize);
            //cbStop.Size = new Size(Machine.UI.Constants.Sizes.Dashboard.ButtonSize, Machine.UI.Constants.Sizes.Dashboard.ButtonSize);
            mpStatistics.Visible = false;
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {

            //GPIx243      funzione di controllo delle licenze per ethercut backoff error:      
            Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                int i01 = 0;
                while (stopTaskCheckLicenceViolation == false)
                {
                    if (Supervisor.Control.HighLevel.Errors.EtherCat)
                    {
                        if (i01 == 0)
                        {
                            long lastTicks = 0;
                            if (lastTicks == 0 || (Environment.TickCount - lastTicks) > 1000)
                            {
                                Invoke(new Action(delegate ()
                                {
                                    //this.Visible = false;
                                    Supervisor.UI.EtherCatError = true;
                                    Supervisor.SetUIState(Control.StateUI.Licenses);
                                    //this.Visible = true;
                                }
                                ));
                                lastTicks = Environment.TickCount;
                            }
                            i01++;
                        }
                    }
                    Thread.Sleep(TimeSpan.FromMilliseconds(1));
                }
            });
            //GPFx243

            //GPIx25    mette stopbutton a active = true se lo stop è stato attivato nella dashboard UI:   DA TESTARE BENE!
            Task.Run(() =>
            {
                bool marchDisableOnce = false;
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                while (stopTaskStopButton == false)
                {
                    if (!marchDisableOnce &&
                        Supervisor.Control.HighLevel.Signals.Stop == true &&
                        !Supervisor.Control.LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled])
                    {
                        cbStop.Active = true;
                        marchDisableOnce = true;
                    }
                    if (Supervisor.Control.LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled])
                    {
                        marchDisableOnce = false;
                    }
                    Thread.Sleep(TimeSpan.FromMilliseconds(1));
                }
            });
            //GPFx25

            //-------------------------------------------------------
            // SuspendLayout
            //-------------------------------------------------------
            SuspendLayout();

            #region Cradle Slider
            mlMaterialRegulation.Text = Localization.Cradle;

            cpbsCradleScalingFactor.PropertyName = Localization.Cradle;
            cpbsCradleScalingFactor.LoadBackgroundImages(
                global::Caron.Cradle.UI.Properties.Resources.cradle_gray_00,
                new List<Image>()
                {
                    global::Caron.Cradle.UI.Properties.Resources.cradle_green_00,
                    global::Caron.Cradle.UI.Properties.Resources.cradle_green_01,
                    global::Caron.Cradle.UI.Properties.Resources.cradle_green_02,
                    global::Caron.Cradle.UI.Properties.Resources.cradle_green_03,
                    global::Caron.Cradle.UI.Properties.Resources.cradle_green_04,
                    global::Caron.Cradle.UI.Properties.Resources.cradle_green_05,
                    global::Caron.Cradle.UI.Properties.Resources.cradle_green_06,
                    global::Caron.Cradle.UI.Properties.Resources.cradle_green_07,
                    global::Caron.Cradle.UI.Properties.Resources.cradle_green_08,
                    global::Caron.Cradle.UI.Properties.Resources.cradle_green_09,
                    global::Caron.Cradle.UI.Properties.Resources.cradle_green_10,
                });

            cpbsCradleScalingFactor.MinValue = -30;
            cpbsCradleScalingFactor.MaxValue = +30;

            cpbsCradleScalingFactor.SetValueWithoutEvent(10);
            cpbsCradleScalingFactor.ValueChangedEventEnabled = true;
            cpbsCradleScalingFactor.ValueChanged += CpbsCradleScalingFactor_ValueChanged;
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

            #region Machine Configuration 
            #region (Left/Right)
            if (Supervisor.Control.HighLevel.Configuration.IsLeftMachine)
            {
                cbCradleSync.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_on_green_SX;
                cbCradleSync.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_off_gray_SX;

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

                cmbStraightRoller.SetImages(new List<Tuple<Image, Image>>()
                {
                    new Tuple<Image, Image>(global::Caron.Cradle.UI.Properties.Resources.tissue_up_gray_SX,global::Caron.Cradle.UI.Properties.Resources.tissue_up_green_SX),
                    new Tuple<Image, Image>(global::Caron.Cradle.UI.Properties.Resources.tissue_down_gray_SX,global::Caron.Cradle.UI.Properties.Resources.tissue_down_green_SX),
                });
            }
            else
            {
                cbCradleSync.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_on_green_DX;
                cbCradleSync.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cradle_off_gray_DX;

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

                cmbStraightRoller.SetImages(new List<Tuple<Image, Image>>()
                {
                    new Tuple<Image, Image>(global::Caron.Cradle.UI.Properties.Resources.tissue_up_gray_DX,global::Caron.Cradle.UI.Properties.Resources.tissue_up_green_DX),
                    new Tuple<Image, Image>(global::Caron.Cradle.UI.Properties.Resources.tissue_down_gray_DX,global::Caron.Cradle.UI.Properties.Resources.tissue_down_green_DX),
                });
            }

            cmbStraightRoller.Value = 0;
            cmbStraightRoller.ValueChangedEventEnabled = true;
            UpdateStraightRoller();
            #endregion

            #region Cutter Presence
            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value == false)
            {
                mlCutter.Visible = false;
                panelCuttOff.Visible = false;
                mlCutterVelocity.Visible = false;
                mpbsCutterVelocity.Visible = false;
                cbStop.Visible = false;
            }
            #endregion
            #endregion

            #region Buttons
            //Sync
            cbCradleSync.StateChangeActivated = false;
            cbCradleSync.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            //Cut Off
            cbCutOff.StateChangeActivated = false;
            cbCutOff.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            //Sharpening
            mbSharpening.StateChangeActivated = true;
            mbSharpening.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            ////Stop
            //cbStop.StateChangeActivated = false;
            //cbStop.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            //CradleJog
            cbCradleJogCW.StateChangeActivated = false;
            cbCradleJogCW.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            cbCradleJogACW.StateChangeActivated = false;
            cbCradleJogACW.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            //Alignment
            mbAlignmentMotorSide.StateChangeActivated = false;
            mbAlignmentMotorSide.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;

            mbAlignmentOperatorSide.StateChangeActivated = false;
            mbAlignmentOperatorSide.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.DefaultBackgroung;
            #endregion

            #region SearchBox
            mSearchBox.SearchButtonPressed += CSearchBox_SearchButtonPressed;
            mSearchBox.SaveButtonPressed += CSearchBox_SaveButtonPressed;
            mSearchBox.SaveWithNameButtonPressed += CSearchBox_SaveWithNameButtonPressed;
            mSearchBox.ResetButtonPressed += CSearchBox_ResetButtonPressed;

            mSearchBox.SelectedItemChanged += CSearchBox_SelectedItemChanged;
            mSearchBox.Leave += CSearchBox_Leave;
            #endregion

            #region Labels
            mlWorkingsStatistics.Text = "Loading..";
            #endregion

            //-------------------------------------------------------
            // Tasks UI Update
            //-------------------------------------------------------
            #region Tasks UI Update
            Task.Run(() =>
            {
                TaskUpdateUIControls();
            });

            Task.Run(() =>
            {
                TaskSlowUpdateUIControls();
            });
            #endregion

            //-------------------------------------------------------
            // Events UI Update
            //-------------------------------------------------------
            #region Events UI Update
            Supervisor.Events.CutOffEnabledChanged += CutOffEnabledChanged;
            //Supervisor.Events.LowLevelMachineStateChanged += LowLevelMachineStateChanged;
            //Supervisor.Events.HighLevelMachineStateChanged += HighLevelMachineStateChanged;
            Supervisor.Events.MachineStoppedChanged += MachineStoppedChanged;
            Supervisor.Events.WorkingModeChanged += WorkingModeChanged;
            Supervisor.Events.WorkingsSettingsChanged += WorkingsSettingsChanged;
            Supervisor.Events.WorkingParametersChanged += WorkingParametersChanged;
            Supervisor.Events.GuidCurrentWorkingParameterSetChanged += GuidCurrentWorkingParameterSetChanged;
            Supervisor.Events.SystemLocalizationChanged += SystemLocalizationChanged;
            Supervisor.Events.CradleSyncChanged += CradleSyncChanged;
            Supervisor.Events.MarchEnabledChanged += MarchEnabledChanged;
            #endregion

            //-------------------------------------------------------
            // Update UI Controls
            //-------------------------------------------------------
            UpdateUIControls();

            //-------------------------------------------------------
            // Machine configuration
            //-------------------------------------------------------
            #region Machine configuration
            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value == false)
            {
                panelCuttOff.Visible = false;
                cbCutOff.Visible = false;
                mlCutter.Visible = false;
            }

            if (Supervisor.Control.HighLevel.Configuration.IsLeftMachine)
            {
                var location = cbCutOff.Location;
                location.X = 75;
                cbCutOff.Location = location;

                mbSharpening.Visible = false;
            }
            if (Supervisor.Control.HighLevel.Configuration.IsRightMachine)
            {
                Machine.UI.FormsControlsHelper.MirrorAllControls(this);
            }
            #endregion

            //-------------------------------------------------------
            // ResumeLayout
            //-------------------------------------------------------
            ResumeLayout();
        }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            //-------------------------------------------------------
            //Set Signals
            //-------------------------------------------------------
            Console.WriteLine("Sending signal UI..");
            Communicator.SendHttpGetRequest("signal/ui/set");
        }

        //------------------------------------------------
        // OnVisibleChanged()
        //------------------------------------------------
        #region OnVisibleChanged
        protected override void OnVisibleChanged(EventArgs e)
        {
            Console.WriteLine($"[DASHBOARD] OnVisibleChanged({Visible})");

            if (Visible)
            {
                //-------------------------------------------------------
                // Errors / Endurance limits
                //-------------------------------------------------------
                #region Errors / Endurance limits
                #region Errors
                if (Supervisor.Control.HighLevel.Errors.EtherCat)
                {
                    Supervisor.UI.EtherCatError = true;
                    Supervisor.SetUIState(Control.StateUI.Licenses);
                }
                #endregion

                #region Endurance limits
                if (Supervisor.UI.MachineEnduranceWarningAlreadyShowed == false && Supervisor.UI.EtherCatError == false)
                {
                    var endurance = Supervisor.Control.HighLevel.MachineEndurance.Clone();
                    var limits = Supervisor.Control.HighLevel.Settings.HighLevel.EnduranceLimits.Clone();

                    for (int i = 0; i < limits.DigitalOutputsToggles.Count(); i++)
                    {
                        if (EnduranceLimit.Check(limits.DigitalOutputsToggles[i], endurance.DigitalOutputsToggles[i]))
                        {
                            string name = Enum.GetNames(typeof(DigitalOutput)).ElementAt(i).Translate();
                            string message = $"{name}";
                            new MachineMessageBoxFullScreen(Localization.MachineMaintenanceAlert, message).ShowDialog();
                        }
                    }

                    for (int i = 0; i < limits.DigitalInputsToggles.Count(); i++)
                    {
                        if (EnduranceLimit.Check(limits.DigitalInputsToggles[i], endurance.DigitalInputsToggles[i]))
                        {
                            string name = Enum.GetNames(typeof(DigitalInput)).ElementAt(i).Translate();
                            string message = $"{name}";
                            new MachineMessageBoxFullScreen(Localization.MachineMaintenanceAlert, message).ShowDialog();
                        }
                    }

                    //--------------------------
                    // MachineWorkingHours
                    //--------------------------
                    if (EnduranceLimit.Check(limits.WorkingHours.PowerOnHours, endurance.WorkingHours.PowerOnHours))
                    {
                        new MachineMessageBoxFullScreen(Localization.MachineMaintenanceAlert, Localization.PowerOnHours).ShowDialog();
                    }

                    if (EnduranceLimit.Check(limits.WorkingHours.WorkingWithCradleInSyncHours, endurance.WorkingHours.WorkingWithCradleInSyncHours))
                    {
                        new MachineMessageBoxFullScreen(Localization.MachineMaintenanceAlert, Localization.WorkingWithCradleInSyncHours).ShowDialog();
                    }

                    if (EnduranceLimit.Check(limits.WorkingHours.MachineMaintenanceHours, endurance.WorkingHours.MachineMaintenanceHours))
                    {
                        new MachineMessageBoxFullScreen(Localization.MachineMaintenanceAlert, Localization.MachineMaintenanceHours).ShowDialog();
                    }

                    //--------------------------
                    // Cutter
                    //--------------------------
                    if (EnduranceLimit.Check(limits.Cutter.NumberOfCutOff, endurance.Cutter.NumberOfCutOff))
                    {
                        new MachineMessageBoxFullScreen(Localization.MachineMaintenanceAlert, Localization.NumberOfCutOff).ShowDialog();
                    }

                    //--------------------------
                    // MachineStatistics
                    //--------------------------
                    //limits.MachineStatistics.NumberPowerOn = (uint)listboxStatistics.GetValue(0);
                    //limits.MachineStatistics.NumberPowerOff = (uint)listboxStatistics.GetValue(1);

                    //--------------------------
                    // Final operations
                    //--------------------------
                    Supervisor.UI.MachineEnduranceWarningAlreadyShowed = true;

                }
                #endregion
                #endregion

                //-------------------------------------------------------
                //PhotocellAlignmentEnabled
                //-------------------------------------------------------
                #region PhotocellAlignmentEnabled
                if (Supervisor.Control.HighLevel.WorkingContext.Parameters.PhotocellAlignmentEnabled)
                {
                    mlAlignment.Visible = false;
                    panelAlignment.Visible = false;
                }
                else
                {
                    mlAlignment.Visible = true;
                    panelAlignment.Visible = true;
                }
                #endregion

                //-------------------------------------------------------
                // Cradle Sync
                //-------------------------------------------------------
                #region CradleSync
                if (Supervisor.Control.HighLevel.Status.CradleInSync)
                {
                    Communicator.SetVariable($"working_mode/set_cradle_sync", "value", true);
                }
                #endregion

                //-------------------------------------------------------
                // Cutter Velocity
                //-------------------------------------------------------
                #region Cutter Velocity
                float velocity = (float)(Supervisor.Control.HighLevel.WorkingContext.Parameters.CutterVelocity * 100.0);
                mpbsCutterVelocity.SetValueWithoutEvent(velocity);
                #endregion

                UpdateUIControls();
            }

            base.OnVisibleChanged(e);
        }
        #endregion

        //------------------------------------------------
        // Search Box
        //------------------------------------------------
        #region Search Box
        private void CSearchBox_SelectedItemChanged(object sender, EventArgs e)
        {
            string itemName = mSearchBox.ItemText;

            //if (Supervisor.Control.HighLevel.WorkingContext.CurrentNameWorkingParameterSet != itemName)
            //{
            //    Communicator.SendHttpGetRequest("workings_settings", $"apply?name={itemName}");
            //    ProRob.ProConsole.WriteLine($"[DASHBOARD] CSearchBox_SelectedItemChanged({mSearchBox.SelectedIndex}-{itemName})", ConsoleColor.Red);
            //}

            if (String.IsNullOrEmpty(itemName) == false)
            {
                Communicator.SendHttpGetRequest("workings_settings", $"apply?name={itemName}");
                ProRob.ProConsole.WriteLine($"[DASHBOARD] CSearchBox_SelectedItemChanged({mSearchBox.SelectedIndex}-{itemName})", ConsoleColor.Red);
            }
        }

        private void CSearchBox_SearchButtonPressed(object sender, EventArgs e)
        {
            Console.WriteLine("[DASHBOARD] CSearchBox_SearchButtonPressed");
            ButtonSearch();

            void ButtonSearch()
            {
                string title = $"{Localization.Search} {Localization.Element.ToLower()}";
                using var keyb = new TouchAlphaNumericKeyboard(title, "");

                keyb.ShowDialog();

                mSearchBox.SearchText = keyb.StringValue;
            }
        }

        private void CSearchBox_ResetButtonPressed(object sender, EventArgs e)
        {
            Console.WriteLine($"CSearchBox_ResetButtonPressed");
            var dialogResult = MachineMessageBox.Show(Localization.Reset, Localization.ResetCurrentWorkingSettings);
            if (dialogResult == DialogResult.OK)
            {
                Communicator.SendHttpGetRequest("workings_settings", "reset");
            }
        }

        private void CSearchBox_SaveWithNameButtonPressed(object sender, EventArgs e)
        {
            Console.WriteLine("[DASHBOARD] CSearchBox_SaveWithNameButtonPressed");

            DialogResult dialogResult = MachineMessageBox.Show(Localization.SaveWithName, Localization.SaveWithNameCurrentWorkingSettings);

            if (dialogResult == DialogResult.OK)
            {
                var keyb = new TouchAlphaNumericKeyboard(Localization.SettingName, "");

                dialogResult = keyb.ShowDialog();

                string name = keyb.StringValue;

                if (dialogResult == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        return;
                    }

                    var parameters = Supervisor.Control.HighLevel.WorkingContext.Parameters.Clone();

                    var workingSetting = new WorkingSetting()
                    {
                        Guid = Guid.NewGuid(),
                        Timestamp = DateTime.Now,
                        Name = name,
                        Parameters = parameters
                    };

                    Communicator.SendHttpPostRequest("workings_settings/add", workingSetting);
                }
            }
        }

        private void CSearchBox_SaveButtonPressed(object sender, EventArgs e)
        {
            Console.WriteLine("[DASHBOARD] CSearchBox_SaveButtonPressed");

            var dialogResult = MachineMessageBox.Show(Localization.Save, Localization.SaveCurrentWorkingSettings);
            if (dialogResult == DialogResult.OK)
            {
                Communicator.SendHttpGetRequest("workings_settings", "save");
            }
        }

        private void CSearchBox_Leave(object sender, EventArgs e)
        {
            Console.WriteLine("CSearchBox_Leave");
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

            if (Supervisor.CradleHelper.CheckIfIsNotInMarchAndShowPopUp())
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
               (hcs == HighLevelControlState.CradleJog && (CradleJogSubState)cs.HighLevelControlSubState != CradleJogSubState.WaitExit) ||
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
                Communicator.SetHighLevelControlState("normal");
            }

            lastTimestampButtonJogPressed = DateTime.UtcNow;
            jogCWStarted = false;
            buttonCWReleased = false;

            cbCradleSync.Enabled = false;
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
                    Communicator.SendHttpGetRequest("cradle/start_jog/cw");
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
                Communicator.SendHttpGetRequest("cradle/stop_jog");
                jogCWStarted = false;
            }

            cbCradleJogACW.Enabled = true;
            cbCradleSync.Enabled = true;
            cbCradleJogCW.Active = false;

            lastTimestampButtonJogPressed = DateTime.UtcNow;

            Console.WriteLine($"EXIT CbCradleJogCW_MouseUp {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");
        }
        #endregion

        #region ACW
        private void CbCradleJogACW_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"ENTER CbCradleJogACW_MouseDown {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");

            if (Supervisor.CradleHelper.CheckIfIsNotInMarchAndShowPopUp())
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
               (hcs == HighLevelControlState.CradleJog && (CradleJogSubState)cs.HighLevelControlSubState != CradleJogSubState.WaitExit) ||
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
                Communicator.SetHighLevelControlState("normal");
            }

            lastTimestampButtonJogPressed = DateTime.UtcNow;
            jogACWStarted = false;
            buttonACWReleased = false;

            cbCradleJogCW.Enabled = false;
            cbCradleSync.Enabled = false;
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
                    Communicator.SendHttpGetRequest("cradle/start_jog/acw");
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
                Communicator.SendHttpGetRequest("cradle/stop_jog");
                jogACWStarted = false;
            }

            cbCradleJogCW.Enabled = true;
            cbCradleSync.Enabled = true;
            cbCradleJogACW.Active = false;

            lastTimestampButtonJogPressed = DateTime.UtcNow;

            Console.WriteLine($"EXIT CbCradleJogACW_MouseUp {DateTime.UtcNow.ToString("HH:mm:ss.fff")}");
        }
        #endregion
        #endregion

        //------------------------------------------------
        // Cradle Scaling Factor
        //------------------------------------------------
        #region Cradle Scaling Factor
        private void CpbsCradleScalingFactor_ValueChanged(object sender, EventArgs e)
        {
            if ((DateTime.UtcNow - LastCradleScalingFactorUpdate) > Machine.UI.Constants.Intervals.MinSliderUpdate)
            {
                LastCradleScalingFactorUpdate = DateTime.UtcNow;

                float value = ((ValueEventArgs<float>)e).Value;
                float scalingFactor = 1.0f + value / 100.0f;

                Communicator.SetVariable($"working_context/scaling_factor", "value", scalingFactor);
            }
        }
        #endregion

        #region Cutter Velocity
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
        #endregion

        //------------------------------------------------
        // Stop Button
        //------------------------------------------------
        #region Stop Button
        private async void CbStop_Click(object sender, EventArgs e)
        {
            Communicator.SendHttpGetRequest("cutter", "stop");
            Communicator.SendHttpGetRequest("signal", "stop/set");

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
            });
        }
        #endregion

        //------------------------------------------------
        // Cut Off
        //------------------------------------------------
        #region Cut Off
        private DateTime lastTimestampCutButtonPressed = DateTime.UtcNow;

        private volatile bool cutOffTaskRunning = false;
        private volatile bool cutOffButtonReleased = false;

        private void CbCutOff_Click(object sender, EventArgs e)
        {
            //--
        }

        private async void CbCutOff_MouseDown(object sender, MouseEventArgs e)
        {
            if (cutOffTaskRunning)
            {
                return;
            }

            if ((DateTime.UtcNow - lastTimestampCutButtonPressed) < TimeSpan.FromSeconds(1))
            {
                return;
            }

            if ((DateTime.UtcNow - lastTimestampButtonJogPressed) < TimeSpan.FromSeconds(1))
            {
                return;
            }

            if (Supervisor.CradleHelper.CheckIfIsNotInMarchAndShowPopUp())
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
            lastTimestampCutButtonPressed = DateTime.UtcNow;

            await Task.Run(() =>
            {
                cutOffTaskRunning = true;

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

                cutOffTaskRunning = false;
            });
        }

        private void CbCutOff_MouseUp(object sender, MouseEventArgs e)
        {
            cutOffButtonReleased = true;
        }
        #endregion

        //------------------------------------------------
        // Straight Roller
        //------------------------------------------------
        #region Straight Roller
        private void CmbStraightRoller_ValueChanged(object sender, MultiButtonsEventArgs e)
        {
#if !DEBUG
            if (Supervisor.CradleHelper.CheckIfIsNotInMarchAndShowPopUp())
            {
                return;
            }

            if (Supervisor.CradleHelper.CheckIfCutterIsOutOfPositionAndShowPopUp())
            {
                return;
            }
#endif
            Communicator.SetVariable($"working_context/straight_roller", "value", cmbStraightRoller.Value > 0);
        }
        #endregion

        //------------------------------------------------
        // Cradle Sync
        //------------------------------------------------
        #region Cradle Sync
        private void CbCradleSync_Click(object sender, EventArgs e)
        {

#if TEST
            cbCradleSync.Active = !cbCradleSync.Active;
            Communicator.SetVariable($"working_mode/set_cradle_sync", "value", cbCradleSync.Active);
            return;
#else
            if (Supervisor.CradleHelper.CheckIfIsNotInMarchAndShowPopUp())
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

            if (cbCradleSync.Active)
            {
                cbCradleSync.Active = false;

                Communicator.SetVariable($"working_mode/set_cradle_sync", "value", cbCradleSync.Active);
                Communicator.SendHttpGetRequest("working_mode/cradle_sync/force_disable_is_true");
            }
            else
            {
                //GPI12 sostituzione check temporale su PhotocellMaterialPresence:
                //bool bol01 = false;
                //if (Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
                //{
                //    bol01 = true;
                //}
                //else
                //{
                //    bol01 = false;
                //}
                //DateTime checkUntilPhotocellMaterialPresence = DateTime.MinValue;
                //checkUntilPhotocellMaterialPresence = DateTime.UtcNow + TimeSpan.FromMilliseconds(Supervisor.Control.HighLevel.Settings.HighLevel.MachineParameters.CheckUntilPhotocellMaterialPresence);   //TimeSpan.FromMilliseconds(250) parametro da mettere nella Cradle per intervallo di check fotocellula presenza materiale
                //while (DateTime.UtcNow < checkUntilPhotocellMaterialPresence)
                //{
                //    // code block to be executed
                //    if (Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
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
                bool c1 = Supervisor.Control.HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
                //////bool c2 = Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence];
                bool c2 = Supervisor.Control.HighLevel.Status.PhotocelMaterialPresenceFiltered;
                //bool c2 = !bol01;
                //GPF25

                //MMIx02
                bool c3 = Supervisor.Control.HighLevel.WorkingContext.Parameters.EnablePhotocellRollPresence && Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.EnableFunctionPhotocellRollPresence.Value;//MMIx05
                bool c4 = Supervisor.Control.HighLevel.Status.PhotocelRollPresenceFiltered;

                bool cond = false;
                bool cond2 = false;

                //presenza materiale
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

                //presenza rotolo
                if (c3 && c4)
                {
                    cond2 = true;
                }
                else if (c3 && !c4)
                {
                    cond2 = false;
                }
                else if (!c3 && c4)
                {
                    cond2 = true;
                }
                else if (!c3 && !c4)
                {
                    cond2 = true;
                }

                if (cond && cond2)
                {
                    cbCradleSync.Active = true;
                    Communicator.SetVariable($"working_mode/set_cradle_sync", "value", cbCradleSync.Active);
                    Communicator.SendHttpGetRequest("working_mode/cradle_sync/force_disable_is_false");
                }
                else if (!cond && cond2)
                {
                    cbCradleSync.Active = false;
                    Communicator.SetVariable($"working_mode/set_cradle_sync", "value", cbCradleSync.Active);
                    MachineMessageBox.Show(Localization.Warning, Localization.MaterialNotPresent);
                }
                else if (cond && !cond2)
                {
                    cbCradleSync.Active = false;
                    Communicator.SetVariable($"working_mode/set_cradle_sync", "value", cbCradleSync.Active);
                    MachineMessageBox.Show(Localization.Warning, Localization.RollNotPresent);
                }
                else if (!cond && !cond2)
                {
                    cbCradleSync.Active = false;
                    Communicator.SetVariable($"working_mode/set_cradle_sync", "value", cbCradleSync.Active);
                    MachineMessageBox.Show(Localization.Warning, Localization.MaterialNotPresent);
                }
                //MMFx02

            }
#endif
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
            if (Supervisor.CradleHelper.CheckIfIsNotInMarchAndShowPopUp())
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

            if (Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitSpoonDown]
                && (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value == true)
                )
            {
                MachineMessageBox.Show(Localization.Warning, Localization.OpenSpoon);
                return;
            }

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
            if (Supervisor.CradleHelper.CheckIfIsNotInMarchAndShowPopUp())
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

            if (Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitSpoonDown] &&
                (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value == true)
                )
            {
                MachineMessageBox.Show(Localization.Warning, Localization.OpenSpoon);
                return;
            }

            mbAlignmentMotorSide.Active = true;
            Communicator.SendLowLevelControlCommand("start_alignment_mt_side");
        }
        #endregion

        //------------------------------------------------
        // Sharpening
        //------------------------------------------------
        #region Sharpening
        private void mbSharpening_Click(object sender, EventArgs e)
        {
            if (Supervisor.CradleHelper.CheckIfIsNotInMarchAndShowPopUp())
            {
                return;
            }

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

        //------------------------------------------------
        // Workings Statistics
        //------------------------------------------------
        #region Workings Statistics
        private void mlWorkingsStatistics_DoubleClick(object sender, EventArgs e)
        {
            Supervisor.SetUIState(Control.StateUI.WorkingsStatistics);
        }
        #endregion

        private void FormDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            //-
            //GPIx25
            stopTaskStopButton = true;
            //GPFx25

            //GPIx243
            stopTaskCheckLicenceViolation = true;
            //GPFx243
        }
    }
}
