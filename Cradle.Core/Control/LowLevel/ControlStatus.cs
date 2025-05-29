using System;
using System.Runtime.Remoting.Contexts;

namespace Caron.Cradle.Control.LowLevel
{
    [Synchronization()]
    public class ControlStatus
    {
        public MachineInfo Info { get; private set; } = new MachineInfo();
        public MachineIO IO { get; private set; } = new MachineIO();
        public MachineActions Actions { get; private set; } = new MachineActions();
        public MachineAxes Axes { get; private set; } = new MachineAxes();

        public ControlStatus()
        {
            //--
        }
    }
}
