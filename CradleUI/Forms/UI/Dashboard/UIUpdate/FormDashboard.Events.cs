using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob.Extensions;

using Caron.Cradle.Control;
using Machine.UI.Communication;
using ProRob.Extensions.Object;

namespace Caron.Cradle.UI
{
    public partial class FormDashboard : FormCradleBase
    {
        private void SystemLocalizationChanged(object sender, EventArgs e)
        {
            Console.WriteLine($"SystemLocalizationChanged");
            UpdateLocalization();
        }

        private void CradleSyncChanged(object sender, EventArgs e)
        {
            Console.WriteLine($"CradleSyncChanged");
            UpdateCradleSyncButton();
        }

        private void WorkingModeChanged(object sender, EventArgs e)
        {
            UpdateLocalization();
        }

        private void MachineStoppedChanged(object sender, EventArgs e)
        {
            UpdateCradleSyncButton();
        }

        private void CutOffEnabledChanged(object sender, EventArgs e)
        {
            UpdateCutOffButton();
        }

        private void MarchEnabledChanged(object sender, EventArgs e)
        {
            UpdateCutOffButton();
        }

        private void WorkingsSettingsChanged(object sender, EventArgs e)
        {
            UpdateWorkingParameters();
            UpdateLocalization();
        }

        private void GuidCurrentWorkingParameterSetChanged(object sender, EventArgs e)
        {
            Console.WriteLine($"GuidCurrentWorkingParameterSetChanged");
            UpdateWorkingParameters();
        }

        private void WorkingParametersChanged(object sender, EventArgs e)
        {
            UpdateWorkingParameters();
        }
    }
}
