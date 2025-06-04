
using Machine.Common;
using Machine.Settings;

namespace Caron.Cradle.Control.HighLevel.Settings
{
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
