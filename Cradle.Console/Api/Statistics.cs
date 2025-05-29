using System;
using System.Linq;
using System.Text;
using System.Web.Http;

using ProRob;

using Machine.Models;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("statistics")]
    public class StatisticsController : CradleApiController
    {
        private static DateTime lastRequestDatetime = DateTime.UtcNow;
        private static int lastRequestTotalMillisecondsProcessorTime = 0;

        [HttpGet]
        [Route("")]
        public ControlStatistics GetStatistics()
        {
            //ProConsole.WriteLine("[API] GetStatistics()", ConsoleColor.Yellow);

            var statistics = new ControlStatistics
            {
                Uptime = MachineController.Uptime,
                NumberOfApiRequests = NumberOfRequests,
                TotalMillisecondsApplicationUptime = ApplicationInfo.TotalMillisecondsApplicationUptime,
                AveragePacketsForSecond = MachineController.AvaragePacketsForSecond,
                PacketsReceived = MachineController.PacketsReceived,
                CommunicationErrors = MachineController.CommunicationErrors,
                AverageCpuUtilization = ApplicationInfo.AverageCpuUtilization,
                TotalMillisecondsProcessorTime = (int)ApplicationInfo.TotalProcessorTime.TotalMilliseconds
            };

            return statistics;
        }

        [HttpGet]
        [Route("window")]
        public object GetWindowStatistics()
        {
            ProConsole.WriteLine("[API] GetWindowStatistics()", ConsoleColor.Yellow);

            var windowCpuTime = (int)ApplicationInfo.TotalProcessorTime.TotalMilliseconds - lastRequestTotalMillisecondsProcessorTime;
            var windowElapsedTime = (DateTime.UtcNow - lastRequestDatetime).TotalMilliseconds;
            double windowCpuUtilization = (double)windowCpuTime / windowElapsedTime * 100.0;

            lastRequestDatetime = DateTime.UtcNow;
            lastRequestTotalMillisecondsProcessorTime = (int)ApplicationInfo.TotalProcessorTime.TotalMilliseconds;

            return new { WindowElapsedTime = windowElapsedTime, WindowCpuUtilization = windowCpuUtilization };
        }

        [HttpGet]
        [Route("requests")]
        public int GetNumberOfRequests()
        {
            return NumberOfRequests;
        }
    }
}
