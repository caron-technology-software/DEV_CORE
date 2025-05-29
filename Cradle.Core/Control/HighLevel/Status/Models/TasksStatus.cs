using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.HighLevel
{
    [Synchronization()]
    public class TasksStatus
    {
        public bool AlignmentDuringSpreadProcessActive { get; set; }
        public bool AutoCenteringActivationStatus { get; set; }

        public TasksStatus()
        {
            //--
        }
    }
}
