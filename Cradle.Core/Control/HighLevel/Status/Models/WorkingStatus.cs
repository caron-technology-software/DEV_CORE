using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.Control.HighLevel
{
    [Synchronization()]
    public class WorkingStatus
    {
        public bool InProgress { get; set; } = false;

        public WorkingStatus()
        {
            //--
        }
    }
}
