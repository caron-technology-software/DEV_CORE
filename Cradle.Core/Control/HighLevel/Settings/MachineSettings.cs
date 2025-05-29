using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    [Synchronization()]
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