using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob.Extensions.Object;

namespace Caron.Cradle.UI
{
    public partial class FormUserSettings : FormCradleBase
    {
        private void WorkingsSettingsChanged(object sender, EventArgs e)
        {
            UpdateUIControls();
        }

        private void RootSettingsChanged(object sender, EventArgs e)
        {
            UpdateUIControls();
        }

        private void CutterVelocityValueChanged(object sender, EventArgs e)
        {
            double value = (double)mpbsCutterVelocity.Value / 100.0;

            var context = Supervisor.Control.HighLevel.WorkingContext.Clone();
            context.Parameters.CutterVelocity = value;

            UpdateMachineContext(context);
        }

        private void Button_ActiveChanged(object sender, EventArgs e)
        {
            var fe = Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled;

            if (fe.EnabledEncoder.Value && fe.UserCanEnableEncoder.Value)
            {
                if (cbEncoderEnabled.Active == false && cbDanceBarEnabled.Active == false)
                {
                    cbDanceBarEnabled.Active = true;
                }
            }

            if (fe.UserCanEnableEncoder.Value == false)
            {
                cbEncoderEnabled.Active = true;
            }

            SetSettingsChanges();
        }
    }
}
