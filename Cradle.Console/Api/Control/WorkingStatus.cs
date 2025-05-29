using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using ProRob;
using ProRob.Extensions.Object;

using Machine.Utility;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("working_status")]
    public class WorkingStatusController : CradleApiController
    {
        [Route("start")]
        [HttpGet]
        public void StartTimer()
        {
            ProConsole.WriteLine($"[API] StartTimer()", ConsoleColor.Yellow);

            if (MachineController.HighLevel.Working.StartDateTime == DateTime.MinValue)
            {
                MachineController.HighLevel.Working.StartDateTime = DateTime.Now;
            }

            MachineController.HighLevel.WorkingStatus.InProgress = true;
        }

        [Route("pause")]
        [HttpGet]
        public void PauseTimer()
        {
            ProConsole.WriteLine($"[API] PauseTimer()", ConsoleColor.Yellow);

            MachineController.HighLevel.WorkingStatus.InProgress = false;
        }
    }
}

