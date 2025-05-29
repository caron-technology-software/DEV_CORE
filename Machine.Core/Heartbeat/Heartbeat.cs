using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProRob;

namespace Machine
{
    public class Heartbeat
    {
        public string ApplicationName { get; set; }
        public ApplicationVersion ApplicationVersion { get; set; }
        public TimeSpan Uptime { get; set; }

        public Heartbeat()
        {
            //--
        }
    }
}
