using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;

using ProRob.Log;

namespace Machine
{
    [Synchronization()]
    public class DbLog
    {
        //public ProLogLiteDB LowLevel { get; set; }
        public ProLogLiteTXT LowLevel { get; set; }
        //public ProLogLiteDB HighLevel { get; set; }
        public ProLogLiteTXT HighLevel { get; set; }
        //public ProLogLiteDB UI { get; set; }
        public ProLogLiteTXT UI { get; set; }

        //public DbLog(ProLogLiteDB lowLevel, ProLogLiteDB highLevel, ProLogLiteDB ui)
        public DbLog(ProLogLiteTXT lowLevel, ProLogLiteTXT highLevel, ProLogLiteTXT ui)
        {
            LowLevel = lowLevel;
            HighLevel = highLevel;
            UI = ui;
        }
    }
}