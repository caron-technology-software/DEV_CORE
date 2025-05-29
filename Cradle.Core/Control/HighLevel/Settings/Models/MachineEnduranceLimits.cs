using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;

using Machine.Common;

using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    [Synchronization()]
    public class MachineEnduranceLimits : MachineEndurance
    {
        public MachineEnduranceLimits() : base()
        {
            //--
        }
    }
}
