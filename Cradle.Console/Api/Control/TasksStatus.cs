using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using ProRob;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("tasks_status")]
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