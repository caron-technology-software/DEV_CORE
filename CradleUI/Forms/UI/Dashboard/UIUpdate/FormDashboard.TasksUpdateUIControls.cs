using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Machine.UI.Communication;

using Caron.Cradle.Control;

namespace Caron.Cradle.UI
{
    public partial class FormDashboard : FormCradleBase
    {
        private void TaskUpdateUIControls()
        {
            while (Supervisor.IsRunning)
            {
                if (Supervisor.UI.State == StateUI.Dashboard)
                {
                    try
                    {

                    }
                    catch
                    {
                        //--
                    }
                }

                Thread.Sleep(Machine.UI.Constants.Intervals.UpdateUIControls);

            }//while (Supervisor.IsRunning)
        }

        private void TaskSlowUpdateUIControls()
        {
            while (Supervisor.IsRunning)
            {
                if (Supervisor.UI.State == StateUI.Dashboard)
                {
                    try
                    {
                        UpdateStraightRoller();

                        UpdateWorkingStatistics();
                    }
                    catch
                    {
                        //--
                    }
                }

                Thread.Sleep(Machine.UI.Constants.Intervals.SlowUpdateUIControls);

            }//while (Supervisor.IsRunning)
        }
    }
}
