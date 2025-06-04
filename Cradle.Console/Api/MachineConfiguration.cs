using System;
using Microsoft.AspNetCore.Mvc;

using ProRob;

using Caron.Cradle.Control.Database;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("machine_configuration")]
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
        public async void SetMachineConfiguration()
        {
            ProConsole.WriteLine($"[API] SetMachineConfiguration()", ConsoleColor.Yellow);

            var json = await ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Configuration = ProRob.Json.Deserialize<MachineConfiguration>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Configuration);
        }
    }
}
