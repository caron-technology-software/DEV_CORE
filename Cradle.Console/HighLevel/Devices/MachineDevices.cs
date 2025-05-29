using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Caron.Cradle.Control.LowLevel.Communication;

namespace Caron.Cradle.Control.HighLevel.Devices
{
    public partial class MachineDevices
    {
        private Communicator Communicator { get; set; }
        private LowLevel.ControlStatus LowLevel { get; set; }
        private HighLevel.ControlStatus HighLevel { get; set; }

        internal ElectricDrives ElectricDrives { get; set; }
        internal Cradle Cradle { get; set; }

        public MachineDevices(LowLevel.ControlStatus lowLevelControlStatus, HighLevel.ControlStatus highLevelControlStatus, Communicator communicator)
        {
            LowLevel = lowLevelControlStatus;
            HighLevel = highLevelControlStatus;
            Communicator = communicator;

            ElectricDrives = new ElectricDrives(LowLevel, HighLevel, Communicator);
            Cradle = new Cradle(LowLevel, HighLevel, Communicator);
        }
    }
}
