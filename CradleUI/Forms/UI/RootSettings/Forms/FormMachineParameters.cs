using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Machine.UI.Common;
using Machine.UI.Controls;
using Machine.UI.Communication;

using Caron.Cradle.Control.HighLevel.Settings;
using Machine.UI.Controls.Extensions;
using Machine.Common;

namespace Caron.Cradle.UI
{
    public partial class FormMachineParameters : FormCradleBase
    {
        private readonly List<MachineEditableItemsListbox> listboxes = new List<MachineEditableItemsListbox>();
        private readonly List<MachineButton> buttons = new List<MachineButton>();
        private readonly List<Label> labels = new List<Label>();

        private SettingsManager<MachineParameters> machineParametersSettingsManager;
        private SettingsManager<UISettings> uiSettingsManager;
        private SettingsManager<AxisSettings> axisSettingsManager;
        private SettingsManager<MotionEncoderSettings> encoderSettingsManager;
        private SettingsManager<MotionDancerSettings> dancerSettingsManager;
        private SettingsManager<MotionEncoderDancerSettings> encoderDancerSettingsManager;

        private int GetNumberOfElementsToConsider()
        {
            switch (Supervisor.UI.CurrentUserTypeLogged)
            {
                case Machine.Common.UserType.Null:
                case Machine.Common.UserType.User:
                    return 2;

                case Machine.Common.UserType.Distributor:
                case Machine.Common.UserType.Manufacturer:
                case Machine.Common.UserType.Root:
                    return 6;
            }

            return 1;
        }

        public FormMachineParameters()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            listboxes.Add(clMachineParameters);
            listboxes.Add(clUI);
            listboxes.Add(clAxis);
            listboxes.Add(clEncoder);
            listboxes.Add(clDancer);
            listboxes.Add(clEncoderDancer);

            buttons.Add(mbMachineParameters);
            buttons.Add(mbUI);
            buttons.Add(mbAxis);
            buttons.Add(mbEncoder);
            buttons.Add(mbDancer);
            buttons.Add(mbEncoderDancer);

            labels.Add(mlMachineParameters);
            labels.Add(mlUI);
            labels.Add(mlAxis);
            labels.Add(mlEncoder);
            labels.Add(mlDancer);
            labels.Add(mlEncoderDancebar);

            mlMachineParameters.Text = Localization.MachineParameters;
            mlUI.Text = Localization.UI;
            mlEncoder.Text = Localization.Encoder;
            mlDancer.Text = Localization.Dancer;
            mlEncoderDancebar.Text = Localization.EncoderDancebar;
            mlAxis.Text = Localization.AxisParameters;

            mbMachineParameters.StateChangeActivated = false;
            mbUI.StateChangeActivated = false;
            mbEncoder.StateChangeActivated = false;
            mbDancer.StateChangeActivated = false;
            mbEncoderDancer.StateChangeActivated = false;
            mbAxis.StateChangeActivated = false;
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            var settingListBoxLocation = new Point(350, 157);
            var settingListBoxSize = new Size(645, 532);

            clMachineParameters.Location = settingListBoxLocation;
            clMachineParameters.Size = settingListBoxSize;

            clUI.Location = settingListBoxLocation;
            clUI.Size = settingListBoxSize;

            clEncoder.Location = settingListBoxLocation;
            clEncoder.Size = settingListBoxSize;

            clDancer.Location = settingListBoxLocation;
            clDancer.Size = settingListBoxSize;

            clEncoderDancer.Location = settingListBoxLocation;
            clEncoderDancer.Size = settingListBoxSize;

            clAxis.Location = settingListBoxLocation;
            clAxis.Size = settingListBoxSize;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                TopMost = true;

                buttons.SetAllToInvisible();
                buttons.SetRangeToVisible(0, GetNumberOfElementsToConsider());
                buttons[0].Active = true;

                labels.SetAllToInvisible();
                labels.SetRangeToVisible(0, GetNumberOfElementsToConsider());
                labels[0].Visible = true;

                listboxes.SetAllToInvisible();
                listboxes[0].Visible = true;

                Refresh();
            }
        }

        private void FormMachineParameters_Load(object sender, EventArgs e)
        {
            UpdateSettings(Supervisor.UI.CurrentUserTypeLogged);
        }

        private void UpdateSettings(UserType userType)
        {
            //-------------------------------------------------------
            // SuspendLayout
            //-------------------------------------------------------
            SuspendLayout();

            machineParametersSettingsManager = new SettingsManager<MachineParameters>(clMachineParameters, "settings/root/machine_parameters", Machine.Localization.GetTranslation, userType, Localization.WriteAccessNotAllowed);
            uiSettingsManager = new SettingsManager<UISettings>(clUI, "settings/ui", Machine.Localization.GetTranslation, userType, Localization.WriteAccessNotAllowed);
            encoderSettingsManager = new SettingsManager<MotionEncoderSettings>(clEncoder, "settings/low_level/motion_encoder", Machine.Localization.GetTranslation, userType, Localization.WriteAccessNotAllowed);
            dancerSettingsManager = new SettingsManager<MotionDancerSettings>(clDancer, "settings/low_level/motion_dancer", Machine.Localization.GetTranslation, userType, Localization.WriteAccessNotAllowed);
            encoderDancerSettingsManager = new SettingsManager<MotionEncoderDancerSettings>(clEncoderDancer, "settings/low_level/motion_encoder_dancer", Machine.Localization.GetTranslation, userType, Localization.WriteAccessNotAllowed);
            axisSettingsManager = new SettingsManager<AxisSettings>(clAxis, "settings/low_level/axis", Machine.Localization.GetTranslation, userType, Localization.WriteAccessNotAllowed);

            //-------------------------------------------------------
            // ResumeLayout
            //-------------------------------------------------------
            ResumeLayout();
        }

        private void cbReturn_Click(object sender, EventArgs e)
        {
            cbReturn.PulseButton();
            cbReturn.Active = false;

            Communicator.TrySendHttpGetRequest("settings/low_level/set_to_controller");
            Close();
        }

        private void mbMachineParameters_Click(object sender, EventArgs e)
        {
            ExecuteActionOnElements(0);
        }

        private void mbUI_Click(object sender, EventArgs e)
        {
            ExecuteActionOnElements(1);
        }

        private void mbAxis_Click(object sender, EventArgs e)
        {
            ExecuteActionOnElements(2);
        }

        private void mbEncoder_Click(object sender, EventArgs e)
        {
            ExecuteActionOnElements(3);
        }

        private void mbDancer_Click(object sender, EventArgs e)
        {
            ExecuteActionOnElements(4);
        }

        private void mbEncoderWithDancer_Click(object sender, EventArgs e)
        {
            ExecuteActionOnElements(5);
        }

        private void ExecuteActionOnElements(int index)
        {
            listboxes.SetAllToInvisible();
            listboxes[index].Visible = true;
            buttons.SetAllToInactive();
            buttons[index].Active = true;
        }
    }
}
