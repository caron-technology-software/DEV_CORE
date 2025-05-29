using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

using ProRob;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.Database;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("working_context")]
    public class WorkingContextController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public WorkingContext GetWorkingContext()
        {
            ProConsole.WriteLine($"[API] GetWorkingContext()", ConsoleColor.Yellow);

            return MachineController.HighLevel.WorkingContext;
        }

        [HttpPost]
        [Route("")]
        public void SetWorkingContext()
        {
            ProConsole.WriteLine($"[API] SetWorkingContext()", ConsoleColor.Yellow);

            var json = ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.WorkingContext = ProRob.Json.Deserialize<WorkingContext>(json);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingContext);
        }

        [HttpGet]
        [Route("parameters")]
        public WorkingParameters GetWorkingParameters()
        {
            ProConsole.WriteLine($"[API] GetWorkingParameters()", ConsoleColor.Yellow);

            return MachineController.HighLevel.WorkingContext.Parameters;
        }

        [HttpPost]
        [Route("parameters")]
        public void SetWorkingParameters()
        {
            ProConsole.WriteLine($"[API] SetWorkingParameters()", ConsoleColor.Yellow);

            var json = ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.WorkingContext.Parameters = ProRob.Json.Deserialize<WorkingParameters>(json);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingContext);
        }

        [Route("scaling_factor")]
        [HttpGet]
        public void SetScalingFactor(float value)
        {
            ProConsole.WriteLine($"[API] SetScalingFactor({value.ToString("0.000")})", ConsoleColor.Yellow);

            MachineController.HighLevel.WorkingContext.Parameters.CradleScalingFactor = value;

            MachineController.Communicator.SetScalingFactor(value);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingContext);
        }

        [Route("straight_roller")]
        [HttpGet]
        public void SetStraightRoller(bool value)
        {
            ProConsole.WriteLine($"[API] SetStraightRoller({value})", ConsoleColor.Yellow);

            MachineController.HighLevel.WorkingContext.Parameters.StraightRoller = value;

            MachineController.Communicator.SetStraightRoller(value);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingContext);
        }
    }
}
