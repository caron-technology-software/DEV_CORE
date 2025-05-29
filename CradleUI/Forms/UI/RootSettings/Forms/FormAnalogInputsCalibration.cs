using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob.Extensions.Object;

using Machine.UI.Communication;
using Machine.UI.Controls;

namespace Caron.Cradle.UI
{
    public partial class FormAnalogInputsCalibration : FormCradleBase
    {
        public float MinValue { get; private set; }
        public float MaxValue { get; private set; }

        public FormAnalogInputsCalibration()
        {
            InitializeComponent();

            #region Buttons
            mbReturn.StateChangeActivated = false;
            mbDancer.Active = true;
            mbDancer.Enabled = false;
            #endregion

            #region Localizations
            labelTitle.Text = Localization.AnalogInputsCalibration;
            mlDancer.Text = $"{Localization.Calibration} {Localization.Dancer.ToLower()}";
            mlApply.Text = Localization.Apply;
            mlReset.Text = Localization.Reset;
            #endregion
        }

        private void FormMachineCalibration_Load(object sender, EventArgs e)
        {
            //--
        }

        protected override void OnVisibleChanged(EventArgs e)
        {

            if (Visible)
            {
                //Settings parameters
                MinValue = float.MaxValue;
                MaxValue = float.MinValue;

                Task.Run(() =>
                {
                    bool running = true;

                    try
                    {
                        while (running)
                        {
                            //---------------------------------------
                            // Running condition
                            //---------------------------------------
                            try
                            {
                                this?.Invoke((MethodInvoker)delegate ()
                                {
                                    running = Visible;
                                });
                            }
                            catch
                            {
                                running = false;
                            }

                            //---------------------------------------
                            // Logic
                            //---------------------------------------
                            float value = Supervisor.Control.LowLevel.IO.AnalogInputs[(byte)0] / 32768.0f * 10.0f;

                            MinValue = Math.Min(MinValue, value);
                            MaxValue = Math.Max(MaxValue, value);

                            double oldMinValue = Supervisor.Control.HighLevel.Settings.LowLevelMotion.General.MinDancerValue.Value;
                            double oldMaxValue = Supervisor.Control.HighLevel.Settings.LowLevelMotion.General.MaxDancerValue.Value;

                            this?.Invoke((MethodInvoker)delegate ()
                            {
                                mlInputValue.Text = $"{Localization.Value}: {value.ToString("0.000")} V";
                                mlPrecInputValue.Text = $"{Localization.PrecedentValue}: {oldMinValue.ToString("0.00")} - {oldMaxValue.ToString("0.00")} V";

                                mlMinValue.Text = $"{Localization.MinValue}: {MinValue.ToString("0.00")} V";
                                mlMaxValue.Text = $"{Localization.MaxValue}: {MaxValue.ToString("0.00")} V";

                            });

                            //---------------------------------------
                            // Wait
                            //---------------------------------------
                            Thread.Sleep(50);
                        }
                    }
                    catch
                    {
                        running = false;
                    }
                });
            }

            base.OnVisibleChanged(e);
        }

        private void mbReturn_Click(object sender, EventArgs e)
        {
            mbReturn.PulseButton();
            mbReturn.Active = false;

            Close();
        }

        private void mlApply_Click(object sender, EventArgs e)
        {
            var dialogResult = MachineMessageBox.Show(Localization.Warning, Localization.QuestionApplyNewValues);

            if (dialogResult == DialogResult.OK)
            {
                var general = Supervisor.Control.HighLevel.Settings.LowLevelMotion.General.Clone();
                general.MinDancerValue.Value = (float)MinValue;
                general.MaxDancerValue.Value = (float)MaxValue;

                Communicator.SendHttpPostRequest("settings/low_level/general", general);
            }
        }

        private void mlReset_Click(object sender, EventArgs e)
        {
            MinValue = float.MaxValue;
            MaxValue = float.MinValue;
        }
    }
}
