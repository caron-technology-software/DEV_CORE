using System;
using System.Linq;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    public class MachineSettings
    {
        public string SoftwareVersion { get; set; }
        public HighLevelSettings HighLevel { get; set; } = new HighLevelSettings();
        public LowLevelMotionSettings LowLevelMotion { get; set; } = new LowLevelMotionSettings();
        public UISettings UI { get; set; } = new UISettings();

        public MachineSettings()
        {
            //--
        }
    }
}