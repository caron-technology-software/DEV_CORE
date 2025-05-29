using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;

using ProRob.Log;

namespace Caron.Spreader
{
    [Synchronization()]
    public class TxtLog
    {
        public ProLogLiteTXT LowLevel { get; set; }
        public ProLogLiteTXT HighLevel { get; set; }
        public ProLogLiteTXT UI { get; set; }

        public TxtLog(ProLogLiteTXT lowLevel, ProLogLiteTXT highLevel, ProLogLiteTXT ui)
        {
            LowLevel = lowLevel;
            HighLevel = highLevel;
            UI = ui;
        }
    }
}