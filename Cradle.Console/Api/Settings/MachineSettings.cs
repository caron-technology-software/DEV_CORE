using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;

using ProRob;
using ProRob.Extensions.Object;

using Machine.Utility;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.Database;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("machine_settings")]
    public class MachineSettingsController : CradleApiController
    {
        [Route("")]
        [HttpGet]
        public MachineSettings GetMachineSettings()
        {
            ProConsole.WriteLine($"[API] GetMachineSettings()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Settings;
        }

        [Route("")]
        [HttpPost]
        public HttpStatusCode SetMachineSettings([FromBody] MachineSettings settings)
        {
            ProConsole.WriteLine($"[API] SetMachineSettings()", ConsoleColor.Yellow);

            if (settings is null)
            {
                Console.WriteLine("Errors on SetSettings(null)");

                return HttpStatusCode.BadRequest;
            }

            try
            {
                MachineController.HighLevel.Settings = settings;

                DatabaseSettings.Update(MachineController.HighLevel.Settings.HighLevel);
                DatabaseSettings.Update(MachineController.HighLevel.Settings.UI);
                DatabaseSettings.Update(MachineController.HighLevel.Settings.LowLevelMotion);
            }
            catch
            {
                Console.WriteLine("Errors on SetSettings()");

                return HttpStatusCode.InternalServerError;
            }

            Console.WriteLine($"\t[DONE]");

            return HttpStatusCode.OK;
        }

        [Route("reset")]
        [HttpGet]
        public HttpStatusCode ResetSettings()
        {
            ProConsole.WriteLine($"[API] ResetSettings()", ConsoleColor.Yellow);

            try
            {
                var settings = ProRob.Json.Deserialize<MachineSettings>(System.IO.File.ReadAllText(Constants.Path.Settings.DefaultSettingsFile));

                MachineController.HighLevel.Settings = settings;

                DatabaseSettings.Update(MachineController.HighLevel.Settings.HighLevel);
                DatabaseSettings.Update(MachineController.HighLevel.Settings.UI);
                DatabaseSettings.Update(MachineController.HighLevel.Settings.LowLevelMotion);
            }
            catch
            {
                Console.WriteLine("Errors on ResetSettings()");

                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }
    }
}