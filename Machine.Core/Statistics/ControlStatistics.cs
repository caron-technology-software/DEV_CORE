using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.Models
{
    public class ControlStatistics
    {
        public TimeSpan Uptime { get; set; }
        public int TotalMillisecondsApplicationUptime { get; set; }
        public int NumberOfApiRequests { get; set; }
        public double AveragePacketsForSecond { get; set; }
        public int PacketsReceived { get; set; }
        public int CommunicationErrors { get; set; }
        public double AverageCpuUtilization { get; set; }
        public int TotalMillisecondsProcessorTime { get; set; }
        public int LowLevelTotalCommandsReceived { get; set; }
        public ControlStatistics()
        {
            //--
        }
    }
}
