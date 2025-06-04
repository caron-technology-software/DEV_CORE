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
    [Route("settings")]
    public class SettingsController : CradleApiController
    {
        [Route("")]
        [HttpGet]
        public HighLevelSettings GetSettings()
        {
            ProConsole.WriteLine($"[API] GetSettings()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Settings.HighLevel;
        }

        [Route("")]
        [HttpPost]
        public HttpStatusCode SetSettings([FromBody] HighLevelSettings settings)
        {
            ProConsole.WriteLine($"[API] SetSettings()", ConsoleColor.Yellow);

            if (settings is null)
            {
                Console.WriteLine("Errors on SetSettings(null)");

                return HttpStatusCode.BadRequest;
            }

            try
            {
                MachineController.HighLevel.Settings.HighLevel = settings;

                DatabaseSettings.Update(MachineController.HighLevel.Settings.HighLevel);
            }
            catch
            {
                Console.WriteLine("Errors on SetSettings()");

                return HttpStatusCode.InternalServerError;
            }

            Console.WriteLine($"\t[DONE]");

            return HttpStatusCode.OK;
        }
    }
}