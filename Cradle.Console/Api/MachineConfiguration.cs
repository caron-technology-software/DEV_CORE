using System;
using System.Web.Http;

using ProRob;

using Caron.Cradle.Control.Database;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("machine_configuration")]
    public class MachineConfigurationController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public MachineConfiguration GetMachineConfiguration()
        {
            ProConsole.WriteLine($"[API] GetMachineConfiguration()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Configuration;
        }

        [HttpPost]
        [Route("")]
        public void SetMachineConfiguration()
        {
            ProConsole.WriteLine($"[API] SetMachineConfiguration()", ConsoleColor.Yellow);

            var json = ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Configuration = ProRob.Json.Deserialize<MachineConfiguration>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Configuration);
        }
    }
}
