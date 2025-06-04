using Machine.Common;
using Machine.Settings;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    public class HighLevelSettings
    {
        public FunctionsEnabled FunctionsEnabled;
        public MachineParameters MachineParameters;
        public MachineEnduranceLimits EnduranceLimits;

        public HighLevelSettings()
        {
            FunctionsEnabled = new FunctionsEnabled();
            MachineParameters = new MachineParameters();
            EnduranceLimits = new MachineEnduranceLimits();
        }
    }
}
