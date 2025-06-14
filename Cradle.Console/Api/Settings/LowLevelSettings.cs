﻿using System;

using ProRob;

using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.Database;
using Microsoft.AspNetCore.Mvc;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("settings/low_level")]
    public class LowLevelSettingsController : CradleApiController
    {
        [HttpGet("set_to_controller")]
        public void SetLowLevelSettings()
        {
            ProConsole.WriteLine($"[API] SetLowLevelSettings()", ConsoleColor.Yellow);

            var lowLevelMotion = MachineController.HighLevel.Settings.LowLevelMotion;
            var functionsEnabled = MachineController.HighLevel.Settings.HighLevel.FunctionsEnabled;
            var machineParameters = MachineController.HighLevel.Settings.HighLevel.MachineParameters;

            MachineController.Communicator.SetMachineLowLevelSettings(lowLevelMotion, functionsEnabled, machineParameters);
        }

        #region General     
        [Route("general")]
        [HttpGet]
        public GeneralCradleSettings GetGeneralCradleSettings()
        {
            ProConsole.WriteLine($"[API] GetGeneralCradleSettings()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Settings.LowLevelMotion.General;
        }

        [HttpPost]
        [Route("general")]
        public async Task<IActionResult> SetGeneralCradleSettings()
        {
            ProConsole.WriteLine($"[API] SetGeneralCradleSettings()", ConsoleColor.Yellow);

            var json = await ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Settings.LowLevelMotion.General = ProRob.Json.Deserialize<GeneralCradleSettings>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Settings.LowLevelMotion);

            return Ok();
        }
        #endregion

        #region Axis
        [Route("axis")]
        [HttpGet]
        public AxisSettings GetAxisSettings()
        {
            ProConsole.WriteLine($"[API] GetAxisSettings()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Settings.LowLevelMotion.Axis;
        }

        [HttpPost]
        [Route("axis")]
        public async Task<IActionResult> SetAxisSettings()
        {
            ProConsole.WriteLine($"[API] SetAxisSettings()", ConsoleColor.Yellow);

            var json = await ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Settings.LowLevelMotion.Axis = ProRob.Json.Deserialize<AxisSettings>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Settings.LowLevelMotion);

            return Ok();
        }
        #endregion

        #region MotionEncoderSettings
        [Route("motion_encoder")]
        [HttpGet]
        public MotionEncoderSettings GetMotionEncoderSettings()
        {
            ProConsole.WriteLine($"[API] GetMotionEncoderSettings()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Settings.LowLevelMotion.Encoder;
        }

        [HttpPost]
        [Route("motion_encoder")]
        public async Task<IActionResult> SetMotionEncoderSettings()
        {
            ProConsole.WriteLine($"[API] SetMotionDancerSettings()", ConsoleColor.Yellow);

            var json = await ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Settings.LowLevelMotion.Encoder = ProRob.Json.Deserialize<MotionEncoderSettings>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Settings.LowLevelMotion);

            return Ok();
        }
        #endregion

        #region MotionDancerSettings     
        [Route("motion_dancer")]
        [HttpGet]
        public MotionDancerSettings GetMotionDancerSettings()
        {
            ProConsole.WriteLine($"[API] GetMotionDancerSettings()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Settings.LowLevelMotion.Dancer;
        }

        [HttpPost]
        [Route("motion_dancer")]
        public async Task<IActionResult> SetMotionDancerSettings()
        {
            ProConsole.WriteLine($"[API] SetMotionDancerSettings()", ConsoleColor.Yellow);

            var json = await ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Settings.LowLevelMotion.Dancer = ProRob.Json.Deserialize<MotionDancerSettings>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Settings.LowLevelMotion);

            return Ok();
        }
        #endregion

        #region MotionEncoderDancerSettings
        [Route("motion_encoder_dancer")]
        [HttpGet]
        public MotionEncoderDancerSettings GetMotionDancerEncoderSettings()
        {
            ProConsole.WriteLine($"[API] GetMotionDancerEncoderSettings()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Settings.LowLevelMotion.EncoderDancer;
        }

        [HttpPost]
        [Route("motion_encoder_dancer")]
        public async Task<IActionResult> SetMotionDancerEncoderSettings()
        {
            ProConsole.WriteLine($"[API] SetMotionDancerEncoderSettings()", ConsoleColor.Yellow);

            var json = await ProRob.WebApi.Helpers.GetContentFromBody(Request);

            MachineController.HighLevel.Settings.LowLevelMotion.EncoderDancer = ProRob.Json.Deserialize<MotionEncoderDancerSettings>(json);

            DatabaseSettings.Update(MachineController.HighLevel.Settings.LowLevelMotion);

            return Ok();
        }

        #endregion
    }
}
