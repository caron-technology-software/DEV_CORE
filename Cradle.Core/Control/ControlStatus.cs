using System;
using System.Linq;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.Control
{
    public class ControlStatus
    {
        public Control.HighLevel.ControlStatus HighLevel { get; set; }
        public Control.LowLevel.ControlStatus LowLevel { get; set; }

        public ControlStatus()
        {
            HighLevel = new HighLevel.ControlStatus();
            LowLevel = new LowLevel.ControlStatus();
        }
    }
}