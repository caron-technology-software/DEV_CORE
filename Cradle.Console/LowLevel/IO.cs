using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace Caron.Cradle.Control.LowLevel
{
    public class IOManager
    {
        private Control.LowLevel.ControlStatus LowLevelStatus { get; set; }

        public IOManager(Control.LowLevel.ControlStatus lowLevelStatus)
        {
            this.LowLevelStatus = lowLevelStatus;
        }

        public bool GetDigitalInput(DigitalInput input)
        {
            return LowLevelStatus.IO.DigitalInputs[(byte)input];
        }
    }
}
