using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProRob.Log;

namespace Caron.Spreader
{
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