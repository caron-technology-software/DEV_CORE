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

using FontAwesome.Sharp;

using Machine.UI.Controls;

using Caron.Cradle.Control;
using Caron.Cradle.Control.HighLevel;

namespace Caron.Cradle.UI
{
    public partial class FormWorkingsStatistics : FormCradleBase
    {
        public bool WorkingInProgress => Supervisor.Control.HighLevel.WorkingStatus.InProgress;
        public FormWorkingsStatistics()
        {
            InitializeComponent();
        }

        private void FormWorkingsStatistics_Load(object sender, EventArgs e)
        {
            UpdateUIForm();

            //-------------------------------------------------------
            // Localization
            //-------------------------------------------------------
            mbNew.Text = Localization.New;
            mbSave.Text = Localization.Save;
            mbWorkingName.Text = Localization.WorkingName;
            mbMaterialCode.Text = Localization.MaterialCode;

            //-------------------------------------------------------
            // Events
            //-------------------------------------------------------
            Supervisor.Events.WorkingProgressChanged += WorkingProgressChanged;

            //-------------------------------------------------------
            // UpdateUIForm
            //-------------------------------------------------------
            UpdateUIForm();

            //-------------------------------------------------------
            // Tasks UI Update
            //-------------------------------------------------------
            #region Tasks UI Update
            Task.Run(() =>
            {
                TaskUpdateUIControls();
            });
            #endregion
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                //-------------------------------
                // Material Update
                //-------------------------------
                string material = Supervisor.Control.HighLevel.WorkingContext.CurrentNameWorkingParameterSet;
                Machine.UI.Communication.Communicator.SetVariable("working", "material", material);
            }
        }

        private void WorkingProgressChanged(object sender, EventArgs e)
        {
            UpdateUIForm();
        }

        protected override void UpdateUIForm()
        {
            if (WorkingInProgress)
            {
                mbPlayPause.Text = Localization.Pause;
                mbPlayPause.IconChar = IconChar.Pause;
            }
            else
            {
                mbPlayPause.Text = Localization.Start;
                mbPlayPause.IconChar = IconChar.Play;
            }

            mlWorkingsStatistics.Text = Supervisor.Control.HighLevel.Working
                .ToString()
                .TrimEnd(Environment.NewLine.ToCharArray());
        }

        private void TaskUpdateUIControls()
        {
            while (Supervisor.IsRunning)
            {
                if (Supervisor.UI.State == StateUI.WorkingsStatistics)
                {
                    try
                    {
                        this?.Invoke((MethodInvoker)delegate ()
                        {
                            UpdateUIForm();
                        });
                    }
                    catch
                    {
                        //--
                    }
                }

                Thread.Sleep(Machine.UI.Constants.Intervals.UpdateUIControls);

            }//while (Supervisor.IsRunning)
        }

        private void cbReturn_Click(object sender, EventArgs e)
        {
            cbReturn.PulseButton();
            cbReturn.Active = false;

            Supervisor.SetPrecedentUIState();
        }

        private void mlWorkingsStatistics_Click(object sender, EventArgs e)
        {
            var dialogResult = MachineMessageBox.Show(Localization.Warning, Localization.ResetCounting);

            if (dialogResult == DialogResult.OK)
            {
                Machine.UI.Communication.Communicator.SendHttpGetRequest("working/reset");
            }
        }

        private void mbWorkingName_Click(object sender, EventArgs e)
        {
            string title = $"{Localization.WorkingName}";

            using (var keyb = new TouchAlphaNumericKeyboard(title, ""))
            {
                keyb.TopMost = true;
                keyb.ShowDialog();

                string workingName = keyb.StringValue;

                Machine.UI.Communication.Communicator.SetVariable("working", "set_working_name", workingName);
            }
        }

        private void mbMaterialCode_Click(object sender, EventArgs e)
        {
            string title = $"{Localization.MaterialCode}";

            using (var keyb = new TouchAlphaNumericKeyboard(title, "", TouchKeyboardType.AllKeys))
            {
                keyb.TopMost = true;
                keyb.ShowDialog();

                string material = keyb.StringValue;

                Machine.UI.Communication.Communicator.SetVariable("working", "set_material_code", material);
            }
        }

        private void mbPlayPause_Click(object sender, EventArgs e)
        {
            if (WorkingInProgress)
            {
                Machine.UI.Communication.Communicator.SendHttpGetRequest("working_status/pause");
            }
            else
            {
                Machine.UI.Communication.Communicator.SendHttpGetRequest("working_status/start");
            }
        }

        private void mbSave_Click(object sender, EventArgs e)
        {
            Machine.UI.Communication.Communicator.SendHttpGetRequest("working/save");
        }

        private void mbNew_Click(object sender, EventArgs e)
        {
            Machine.UI.Communication.Communicator.SendHttpGetRequest("working/new");
        }
    }
}
