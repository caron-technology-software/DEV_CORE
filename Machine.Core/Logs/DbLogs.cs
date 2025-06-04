using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProRob.Log;

namespace Machine
{
    public class DbLog
    {
        public ProLogLiteTXT LowLevel { get; set; }
        public ProLogLiteTXT HighLevel { get; set; }
        public ProLogLiteTXT UI { get; set; }

        public DbLog(ProLogLiteTXT lowLevel, ProLogLiteTXT highLevel, ProLogLiteTXT ui)
        {
            LowLevel = lowLevel;
            HighLevel = highLevel;
            UI = ui;
        }
    }
}