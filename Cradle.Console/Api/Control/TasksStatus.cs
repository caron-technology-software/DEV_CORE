using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ProRob;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("tasks_status")]
    public class TasksStatusController : CradleApiController
    {
        //----------------------------------------------
        // Alignment
        //----------------------------------------------
        [Route("alignment/on")]
        [HttpGet]
        public void SetAlignmentDuringSpreadProcessActive()
        {
            ProConsole.WriteLine($"[API] SetAlignmentDuringSpreadProcessActive()", ConsoleColor.Yellow);
            MachineController.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = true;
        }

        [Route("alignment/off")]
        [HttpGet]
        public void ResetAlignmentDuringSpreadProcessActive()
        {
            ProConsole.WriteLine($"[API] ResetAlignmentDuringSpreadProcessActive()", ConsoleColor.Yellow);
            MachineController.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = false;
        }
    }
}