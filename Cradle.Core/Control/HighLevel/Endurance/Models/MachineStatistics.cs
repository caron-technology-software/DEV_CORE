using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Machine.Common;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    public class MachineStatistics
    {
        [UserAccess(UserType.Manufacturer, UserType.Root)]
        public uint NumberPowerOn { get; set; }

        [UserAccess(UserType.Manufacturer, UserType.Root)]
        public uint NumberPowerOff { get; set; }

        //GPIx243 da mettere nelle form per la visualizzazione limit e status (vedi com'è fatto per "NumberPowerOff"):
        [UserAccess(UserType.Manufacturer, UserType.Root)]
        public uint EthercatCode { get; set; }
        //GPFx243

        public MachineStatistics()
        {
            //--
        }
    }
}
