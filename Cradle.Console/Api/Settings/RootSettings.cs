﻿using System;
using Microsoft.AspNetCore.Mvc;

using ProRob;

using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.Database;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("settings/root")]
    public class RootSettingsController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public HighLevelSettings GetRootSettings()
        {
            ProConsole.WriteLine($"[API] GetRootSettings()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Settings.HighLevel;
        }

        [HttpPost]
        [Route("")]
        public async void SetRootSettings()
        {
            ProConsole.WriteLine($"[API] SetRootSettings()", ConsoleColor.Yellow);

            var json = await ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Settings.HighLevel = ProRob.Json.Deserialize<HighLevelSettings>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Settings.HighLevel);
        }

        [HttpGet]
        [Route("machine_parameters")]
        public MachineParameters GetMachineParameters()
        {
            ProConsole.WriteLine($"[API] GetMachineParameters()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Settings.HighLevel.MachineParameters;
        }

        [HttpPost]
        [Route("machine_parameters")]
        public async void SetMachineParameters()
        {
            ProConsole.WriteLine($"[API] SetMachineParameters()", ConsoleColor.Yellow);

            var json = await ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Settings.HighLevel.MachineParameters = ProRob.Json.Deserialize<MachineParameters>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Settings.HighLevel);
        }

        [HttpGet]
        [Route("endurance_limits")]
        public MachineEnduranceLimits GetEnduranceLimits()
        {
            ProConsole.WriteLine($"[API] GetEnduranceLimits()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Settings.HighLevel.EnduranceLimits;
        }

        [HttpPost]
        [Route("endurance_limits")]
        public async void SetEnduranceLimits()
        {
            ProConsole.WriteLine($"[API] SetEnduranceLimits()", ConsoleColor.Yellow);

            var json = await ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Settings.HighLevel.EnduranceLimits = ProRob.Json.Deserialize<MachineEnduranceLimits>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Settings.HighLevel);
        }
    }
}
