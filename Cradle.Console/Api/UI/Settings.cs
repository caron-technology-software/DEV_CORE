using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using ProRob;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("settings")]
    public class SettingController : CradleApiController
    {
        [HttpGet]
        [Route("reset")]
        public void ResetSettings()
        {
            ProConsole.WriteLine($"[API] ResetSettings()", ConsoleColor.Yellow);

            MachineController.HighLevel.Signals.DeleteSettingsAtShutdown = true;
        }
    }
}
