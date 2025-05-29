using System.Runtime.Remoting.Contexts;

using Machine.Common;
using Machine.Settings;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    [Synchronization()]
    public class UISettings : MachineGroupOfSettings
    {
        [UserAccess(UserType.Distributor)]
        public BooleanMachineSetting ShowWorkingInfoOnDashBoard { get; set; } = new BooleanMachineSetting(false);

        [UserAccess(UserType.Distributor)]
        public BooleanMachineSetting ShowDebugInfo { get; set; } = new BooleanMachineSetting(false);

        [UserAccess(UserType.Distributor)]
        public BooleanMachineSetting EnablePlots { get; set; } = new BooleanMachineSetting(false);

        public UISettings()
        {
            //--
        }
    }
}
