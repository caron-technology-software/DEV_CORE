using System;

using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.Control.HighLevel
{
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
