using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caron.Cradle.UI
{
    public partial class FormMachineConfiguration : FormCradleBase
    {
        protected override void UpdateUIForm()
        {
            UpdateUIControls();
        }

        private void UpdateLocalization()
        {
            labelTitle.Text = Localization.MachineConfiguration;

            cbCutterPresence.PropertyName = Localization.CutterPresence;
            cbEncoderPresence.PropertyName = Localization.EnabledEncoder;
            cbReverseEncoder.PropertyName = Localization.ReverseEncoder;
            cbTitanPresence.PropertyName = Localization.TitanPresence;
            //GPIx101 2)
            cbEnableFunctionPhotocellRollPresence.PropertyName = Localization.EnableFunctionPhotocellRollPresence;
            //GPFx101
            cbUserCanEnableEncoder.PropertyName = Localization.UserCanEnableEncoder;

            mcbMachineType.PropertyName = Localization.MachineType;
        }

        private void UpdateUIControls()
        {
            try
            {
                this?.Invoke((MethodInvoker)delegate ()
                {
                    SetButtonsStatus();
                    SetControls();
                    UpdateLocalization();
                });
            }
            catch
            {
                Console.WriteLine("[FormSettingsRootEnableDisableFunctions] EXCEPTION");
            }
        }
    }
}