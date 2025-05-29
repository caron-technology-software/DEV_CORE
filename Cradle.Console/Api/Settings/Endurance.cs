using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
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
    [RoutePrefix("endurance")]
    public class EnduranceController : CradleApiController
    {
        [Route("reset")]
        [HttpGet]
        public void ResetEnduranceSettings()
        {
            ProConsole.WriteLine($"[API] ResetEnduranceSettings()", ConsoleColor.Yellow);

            MachineController.HighLevel.MachineEndurance = new MachineEndurance();

            DatabaseSettings.Update(MachineController.HighLevel.MachineEndurance);
        }
    }
}