using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Machine.Common;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    public class MachineEndurance
    {
        [UserAccess(UserType.Manufacturer, UserType.Root)]
        public uint[] DigitalOutputsToggles { get; set; } = new uint[Enum.GetNames(typeof(DigitalOutput)).Count()];

        [UserAccess(UserType.Manufacturer, UserType.Root)]
        public uint[] DigitalInputsToggles { get; set; } = new uint[Enum.GetNames(typeof(DigitalInput)).Count()];

        public CutterEndurance Cutter { get; set; } = new CutterEndurance();

        public MachineStatistics Statistics { get; set; } = new MachineStatistics();

        public MachineWorkingHours WorkingHours { get; set; } = new MachineWorkingHours();

        public MachineEndurance()
        {
            //--
        }
    }
}
