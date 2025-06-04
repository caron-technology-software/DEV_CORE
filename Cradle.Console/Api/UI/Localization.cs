using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ProRob;

using Machine;
using Machine.Utility;
using ProRob.WebApi;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("localization")]
    public class LocalizationsController : CradleApiController
    {
        [HttpGet]
        [Route("language")]
        public MachineLanguage GetLocalizationSettings()
        {
            ProConsole.WriteLine($"[API] GetLocalizationSettings()", ConsoleColor.Yellow);

            return MachineController.MachineLanguage;
        }

        [HttpGet]
        [Route("set_language")]
        public void SetLocalizationSettings(MachineLanguage machineLanguage)
        {
            ProConsole.WriteLine($"[API] SetLocalizationSettings()", ConsoleColor.Yellow);

            var localizationSettings = new LocalizationSettings()
            {
                MachineLanguage = machineLanguage
            };

            MachineData.Save(Cradle.Constants.Path.Settings.Localization, localizationSettings);
        }
    }
}
