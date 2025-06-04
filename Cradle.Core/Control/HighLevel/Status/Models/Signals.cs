using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.HighLevel
{
    public class Signals
    {
        public bool ControlReady { get; set; } = false;
        public bool UI { get; set; } = false;
        public bool BoostrapperUIChecker { get; set; } = false;
        public bool DeleteSettingsAtShutdown { get; set; } = false;
        public bool Cut { get; set; } = false;
        public bool EnableSync { get; set; } = false;
        public bool Stop { get; set; } = false;
        public Signals()
        {
            //--
        }
    }
}
