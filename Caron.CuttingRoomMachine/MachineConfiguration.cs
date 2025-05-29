using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron
{
    public class MachineConfiguration
    {
        public MachineType MachineType { get; set; }
        public string MachineSerial { get; set; }
        public MachineConfiguration()
        {
            //--
        }

        public bool IsLeftMachine => this.MachineType == MachineType.LeftMachine;
        public bool IsRightMachine => this.MachineType == MachineType.RightMachine;
    }
}
