using System;
using Microsoft.AspNetCore.Mvc;

using ProRob;

using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.Database;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("settings/ui")]
    public class UISettingsController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public UISettings GetUISettings()
        {
            ProConsole.WriteLine($"[API] GetUISettings()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Settings.UI;
        }

        [HttpPost]
        [Route("")]
        public async void SetUISettings()
        {
            ProConsole.WriteLine($"[API] SetUISettings()", ConsoleColor.Yellow);

            var json = await ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Settings.UI = ProRob.Json.Deserialize<UISettings>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Settings.UI);
        }
    }
}
