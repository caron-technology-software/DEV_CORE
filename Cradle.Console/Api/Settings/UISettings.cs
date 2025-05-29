using System;
using System.Web.Http;

using ProRob;

using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.Database;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("settings/ui")]
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
        public void SetUISettings()
        {
            ProConsole.WriteLine($"[API] SetUISettings()", ConsoleColor.Yellow);

            var json = ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Settings.UI = ProRob.Json.Deserialize<UISettings>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Settings.UI);
        }
    }
}
