using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine
{
    public enum ApplicationStatus : int
    {
        Initializing = 0,
        Running,
        Stopped,
        Restarting,

        //Industrial PC
        Rebooting,
        Shutdowing
    }
}
