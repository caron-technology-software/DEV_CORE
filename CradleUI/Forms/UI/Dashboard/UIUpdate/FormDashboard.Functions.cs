using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob;
using ProRob.Extensions.Object;
using ProRob.Extensions.Hashing;

using Machine;
using Machine.UI.Controls;

using Caron.Cradle.Control;
using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.UI
{
    public partial class FormDashboard : FormCradleBase
    {
        protected override void UpdateUIForm()
        {
            if (Supervisor.UI.State == StateUI.Dashboard)
            {
                Console.WriteLine($"[DASHBOARD] UpdateUIForm ({DateTime.Now})");

                UpdateUIControls();
            }
        }

        private bool CheckCutterOutOfPosition()
        {
            var di = Supervisor.Control.LowLevel.IO.DigitalInputs;

            if (di[(byte)DigitalInput.LimitCutterOperatorSide] == false)
            {
                MachineMessageBox.Show(Localization.Warning, Localization.CutterOutOfPosition);
                return true;
            }

            return false;
        }

        private void UpdateUIControls()
        {
            try
            {
                UpdateCutOffButton();
                UpdateStraightRoller();
                UpdateCradleSyncButton();
                UpdateSearchBox();
                UpdateSearchBoxIndex();
                UpdateSearchBoxItems();
                UpdateWorkingParameters();
                UpdateCutterVelocitySlider();
                UpdateCradleScalingFactorSlider();
                UpdateLocalization();
            }
            catch
            {
                Console.WriteLine("ERRORS UpdateUIControls");
            }
        }

        private void UpdateWorkingStatistics()
        {
            this?.Invoke((MethodInvoker)delegate ()
            {
                if (Supervisor.Control.HighLevel.Settings.UI.ShowWorkingInfoOnDashBoard.Value)
                {
                    mpStatistics.Visible = true;
                    mlWorkingsStatistics.Text = Supervisor.Control.HighLevel.Working.ToString().TrimEnd(Environment.NewLine.ToCharArray());
                }
                else
                {
                    mpStatistics.Visible = false;
                }
            });
        }

        private void UpdateLocalization()
        {
            this?.Invoke((MethodInvoker)delegate ()
            {
                mlAlignment.Text = Localization.ManualCradleAlignment;
                mlWorkingModeTitle.Text = Localization.WorkingMode;
                mlWorkingMode.Text = Supervisor.Control.HighLevel.WorkingContext.Parameters.WorkingMode.Translate();
                mlCradleJog.Text = Localization.CradleJog;
                mlSync.Text = Localization.Sync;
                mlMaterialRegulation.Text = Localization.MaterialRegolation;
                mlCutter.Text = Localization.Cutter;
                mlCutterVelocity.Text = Localization.CutterVelocity;

                UpdateStraightRoller();
            });
        }

        private void UpdateStraightRoller()
        {
            this?.Invoke((MethodInvoker)delegate ()
            {
                try
                {
                    cmbStraightRoller.Enabled = Supervisor.Control.HighLevel.Status.MachineStopped;
                    cmbStraightRoller.SetValueWithoutEvent(Supervisor.Control.HighLevel.WorkingContext.Parameters.StraightRoller ? 1 : 0);

                    if (cmbStraightRoller.Value > 0)
                    {
                        mlStraightRoller.Text = $"{Localization.StraightRoller} {Localization.Straight}";
                    }
                    else
                    {
                        mlStraightRoller.Text = $"{Localization.StraightRoller} {Localization.Reverse}";
                    }
                }
                catch
                {
                    //--
                }
            });

        }

        private void UpdateSearchBoxIndex()
        {
            var control = Supervisor.Control.HighLevel.Clone();

            var workingsSettings = control.WorkingsSettings;
            var guid = control.WorkingContext.CurrentGuidWorkingParameterSet;
            int index = workingsSettings.Items.FindIndex(x => x.Guid == guid);

            try
            {
                this?.Invoke((MethodInvoker)delegate ()
                {
                    mSearchBox.SetIndexWithoutEvent(index);
                });
            }
            catch
            {
                Console.WriteLine("ERROR:GuidCurrentWorkingParameterSetChanged");
            }
        }

        private void UpdateSearchBox()
        {
            var context = Supervisor.Control.HighLevel.WorkingContext;
            var workingsSettings = Supervisor.Control.HighLevel.WorkingsSettings;
            var workingParametersFromContext = context.Parameters;

            var query = workingsSettings.Items.Where(x => x.Guid == context.CurrentGuidWorkingParameterSet).Select(x => x.Parameters);

            int n = query.Count();

            if (n == 1)
            {
                var workingParametersFromGuid = query.ElementAt(0);

                bool settingsChanged = !string.Equals(workingParametersFromContext.GetSHA1Hash(), workingParametersFromGuid.GetSHA1Hash());

                mSearchBox.SettingsChanged = settingsChanged;
            }
        }

        private void UpdateSearchBoxItems()
        {
            this?.Invoke((MethodInvoker)delegate ()
            {
                try
                {
                    var control = Supervisor.Control.HighLevel.Clone();
                    var settingsNames = control.WorkingsSettings.Items.Select(x => x.Name).ToArray();
                    var itemsNames = mSearchBox.Items;

                    if (!settingsNames.Equals(itemsNames))
                    {
                        int currentIndex = itemsNames.ToList().FindIndex(x => x == control.WorkingContext.CurrentNameWorkingParameterSet);

                        mSearchBox.ClearItems();
                        mSearchBox.AddItemsRange(settingsNames, currentIndex);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[EXCEPTION] message:{e.Message} source:{e.Source}");
                }
            });
        }

        private void UpdateWorkingParameters()
        {
            try
            {
                this?.Invoke((MethodInvoker)delegate ()
                {
                    UpdateCradleSyncButton();
                    UpdateSearchBox();
                    UpdateSearchBoxIndex();
                    UpdateSearchBoxItems();
                    UpdateCradleScalingFactorSlider();
                    UpdateCutterVelocitySlider();
                    UpdateStraightRoller();
                });
            }
            catch
            {
                Console.WriteLine("ERROR:WorkingParametersChanged");
            }
        }

        private void UpdateCradleScalingFactorSlider()
        {
            double velocity = Supervisor.Control.HighLevel.WorkingContext.Parameters.CradleScalingFactor;
            float sliderValue = (float)((velocity - 1.0) * 100.0);

            this?.Invoke((MethodInvoker)delegate ()
            {
                cpbsCradleScalingFactor.SetValueWithoutEvent(sliderValue);
            });
        }

        private void UpdateCutterVelocitySlider()
        {
            float velocity = (float)(Supervisor.Control.HighLevel.WorkingContext.Parameters.CutterVelocity * 100.0);

            this?.Invoke((MethodInvoker)delegate ()
            {
                mpbsCutterVelocity.SetValueWithoutEvent(velocity);
            });
        }

        private void UpdateCradleSyncButton() //
        {
            this?.Invoke((MethodInvoker)delegate ()
            {
                cbCradleSync.Active = Supervisor.Control.HighLevel.Status.CradleInSync;
                cbCradleSync.Enabled = Supervisor.Control.HighLevel.Status.MachineStopped;
            });
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
                        //cbStop.Enabled = false;
                        cbCutOff.Active = false;
                        cbCutOff.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cut_off_disabled;
                        return;
                    }

                    //In caso di pressione tasto emergenza, ripristino stato
                    if (Supervisor.Control.HighLevel.Status.HighLevelControlState == Control.HighLevel.ControlState.Normal &&
                        Supervisor.Control.LowLevel.Info.MachineState == (byte)Control.LowLevel.ControlState.WaitMarch)
                    {
                        Console.WriteLine($"[DASHBOARD] => UpdateCutOffButton(after Emergency) coe: {Supervisor.Control.HighLevel.Status.CutOffEnabled} cbr:{cutOffButtonReleased}");
                        cutOffButtonReleased = true;

                        cbCutOff.Enabled = true;
                        cbCutOff.Active = false;
                        cbCutOff.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cut_off_green;
                        cbCutOff.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.cut_off_gray;
                    }

                    //Cut Off
                    bool c1 = Supervisor.Control.LowLevel.Info.MachineState == (byte)ControlState.CutOff;
                    bool c2 = Supervisor.Control.HighLevel.Status.CutOffEnabled;

                    //cbStop.Enabled = true;

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
    }
}