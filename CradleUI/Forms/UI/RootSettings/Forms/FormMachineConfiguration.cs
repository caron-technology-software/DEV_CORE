using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob.Extensions.Object;

using Machine.UI.Controls;
using Machine;

using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.UI
{
    public partial class FormMachineConfiguration : FormCradleBase
    {
        private List<MachineButtonLabel> buttons = new List<MachineButtonLabel>();

        private void SetButtonsStatus()
        {
            FunctionsEnabled functionsEnabled = Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled;

            cbCutterPresence.SetActiveWithoutEvent(functionsEnabled.CutterPresence.Value);
            cbEncoderPresence.SetActiveWithoutEvent(functionsEnabled.EnabledEncoder.Value);
            cbReverseEncoder.SetActiveWithoutEvent(functionsEnabled.ReverseEncoder.Value);
            cbTitanPresence.SetActiveWithoutEvent(functionsEnabled.TitanPresence.Value);
            //GPIx101 2)
            cbEnableFunctionPhotocellRollPresence.SetActiveWithoutEvent(functionsEnabled.EnableFunctionPhotocellRollPresence.Value);
            //GPFx101
            cbUserCanEnableEncoder.SetActiveWithoutEvent(functionsEnabled.UserCanEnableEncoder.Value);
        }

        private void SetSettingsChanges()
        {
            var rootSettings = Supervisor.Control.HighLevel.Settings.HighLevel.Clone();
            var functionsEnabled = rootSettings.FunctionsEnabled;

            functionsEnabled.CutterPresence.Value = cbCutterPresence.Active;
            functionsEnabled.EnabledEncoder.Value = cbEncoderPresence.Active;
            functionsEnabled.ReverseEncoder.Value = cbReverseEncoder.Active;
            functionsEnabled.TitanPresence.Value = cbTitanPresence.Active;
            //GPIx101 2)
            functionsEnabled.EnableFunctionPhotocellRollPresence.Value = cbEnableFunctionPhotocellRollPresence.Active;
            //GPFx101
            functionsEnabled.UserCanEnableEncoder.Value = cbUserCanEnableEncoder.Active;

            Machine.UI.Communication.Communicator.SendHttpPostRequest("settings/root", rootSettings);
        }

        private void SetControls()
        {
            //------------------------------------
            // MachineType
            //------------------------------------
            int[] values = ((MachineType[])Enum.GetValues(typeof(MachineType))).Select(x => (int)x).ToArray();
            string[] names = Enum.GetNames(typeof(MachineType)).Select(x => x.Translate()).ToArray();
            var items = new List<Tuple<int, string>>();

            for (int i = 0; i < values.Length; i++)
            {
                items.Add(new Tuple<int, string>(values[i], names[i]));
            }

            mcbMachineType.SetPropertyValues(items);
            mcbMachineType.PropertyValue = (int)(Supervisor.Control.HighLevel.Configuration.MachineType);

            //------------------------------------
            // MachineSerial
            //------------------------------------
            machineSerial.PropertyName = Localization.MachineSerial;
            machineSerial.PropertyValue = Supervisor.Control.HighLevel.Configuration.MachineSerial;
        }

        public FormMachineConfiguration()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            MachineMessageBox.Show(Localization.Warning, Localization.RestartMachineToActivateFunctions);
        }

        private void cbReturn_Click(object sender, EventArgs e)
        {
            cbReturn.PulseButton();
            cbReturn.Active = false;
            Close();
        }

        private void ButtonLabel_ActiveChanged(object sender, EventArgs e)
        {
            SetSettingsChanges();
        }

        private void FormRootSettingsMachineConfiguration_Load(object sender, EventArgs e)
        {
            SuspendLayout();

            cbReturn.Active = false;

            buttons.Add(cbCutterPresence);
            buttons.Add(cbEncoderPresence);
            buttons.Add(cbReverseEncoder);
            buttons.Add(cbTitanPresence);
            //GPIx101 2)
            buttons.Add(cbEnableFunctionPhotocellRollPresence);
            //GPFx101
            buttons.Add(cbUserCanEnableEncoder);

            SetButtonsStatus();
            SetControls();

            foreach (var b in buttons)
            {
                b.ActiveChanged += ButtonLabel_ActiveChanged;
            }

            mcbMachineType.PropertyChanged += McbMachineType_PropertyChanged;
            machineSerial.PropertyChanged += MachineSerial_PropertyChanged;

            ResumeLayout();
            Refresh();
        }

        private void MachineSerial_PropertyChanged(object sender, EventArgs e)
        {
            SaveConfiguration();
        }

        private void McbMachineType_PropertyChanged(object sender, EventArgs e)
        {
            SaveConfiguration();
        }

        private void SaveConfiguration()
        {
            var configuration = Supervisor.Control.HighLevel.Configuration.Clone();

            configuration.MachineType = (MachineType)(mcbMachineType.PropertyValue);
            configuration.MachineSerial = machineSerial.PropertyValue;

            Machine.UI.Communication.Communicator.SendHttpPostRequest("machine_configuration", configuration);
        }
    }
}
