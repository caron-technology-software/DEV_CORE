using System;
using System.Runtime.Remoting.Contexts;

using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.Control.HighLevel
{
    [Synchronization()]
    public class WorkingContext
    {
        public Guid CurrentGuidWorkingParameterSet { get; set; }
        public string CurrentNameWorkingParameterSet { get; set; }
        public WorkingParameters Parameters { get; set; } = new WorkingParameters();

        public WorkingContext()
        {
            //--
        }
    }
}
