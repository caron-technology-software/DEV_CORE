
using Machine.Common;
using Machine.Settings;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    public class FunctionsEnabled
    {
        public BooleanMachineSetting CutterPresence { get; set; } = new BooleanMachineSetting(false);
        public BooleanMachineSetting EnabledEncoder { get; set; } = new BooleanMachineSetting(true);
        public BooleanMachineSetting ReverseEncoder { get; set; } = new BooleanMachineSetting(false);
        public BooleanMachineSetting TitanPresence { get; set; } = new BooleanMachineSetting(false);
        //GPIx101 2)
        public BooleanMachineSetting EnableFunctionPhotocellRollPresence { get; set; } = new BooleanMachineSetting(false);
        //GPFx101
        public BooleanMachineSetting UserCanEnableEncoder { get; set; } = new BooleanMachineSetting(true);

        public FunctionsEnabled()
        {
            // --
        }
    }
}
