﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using ProRob;
using ProRob.Extensions.Object;

using Machine.Utility;

using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.Database;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using ProRob.WebApi;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("workings_settings")]
    public class WorkingsSettingsController : CradleApiController
    {
        [HttpGet("")]
        public WorkingsSettings GetWorkingsSettings()
        {
            ProConsole.WriteLine($"[API] GetWorkingsSettings()", ConsoleColor.Yellow);

            return MachineController.HighLevel.WorkingsSettings;
        }

        [HttpGet("current")]
        public IActionResult GetCurrentWorkingsSettings()
        {
            ProConsole.WriteLine($"[API] GetCurrentWorkingsSettings()", ConsoleColor.Yellow);

            var name = MachineController.HighLevel.WorkingContext.CurrentNameWorkingParameterSet;
            var parameters = MachineController.HighLevel.WorkingContext.Parameters;

            return Ok(new { Name = name, Parameters = parameters });
        }

        [HttpGet("by_name")]
        public IActionResult GetWorkingsSettingsByName(string name)
        {
            ProConsole.WriteLine($"[API] GetWorkingsSettings({name})", ConsoleColor.Yellow);

            var query = MachineController.HighLevel.WorkingsSettings.Items.Where(x => x.Name == name);

            if (query != null && query.Count() > 0)
            {
                return Ok(query.First());
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("")]
        public HttpStatusCode SetWorkingsSettings([Microsoft.AspNetCore.Mvc.FromBody] WorkingsSettings workingsSettings)
        {
            ProConsole.WriteLine($"[API] SetWorkingsSettings()", ConsoleColor.Yellow);

            if (workingsSettings is null || workingsSettings.Items.Count == 0)
            {
                Console.WriteLine("Errors on SetWorkingsSettings(null)");

                return HttpStatusCode.BadRequest;
            }

            try
            {
                MachineController.HighLevel.WorkingsSettings = workingsSettings;

                DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);
            }
            catch
            {
                Console.WriteLine("Errors on SetWorkingsSettings()");

                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }

        [HttpGet("names")]
        public IActionResult GetWorkingsSettingsNames()
        {
            ProConsole.WriteLine($"[API] GetWorkingsSettingsNames()", ConsoleColor.Yellow);

            return Ok(MachineController.HighLevel.WorkingsSettings.Items.Select(x => x.Name));
        }

        [HttpGet("apply")]
        public HttpStatusCode ApplyWorkingSetting(string name)
        {
            ProConsole.WriteLine($"[API] ApplyWorkingSetting({name})", ConsoleColor.Yellow);

            var setting = MachineController.HighLevel.WorkingsSettings.Items.Where(x => x.Name == name).FirstOrDefault().Clone();

            if (setting is null)
            {
                return HttpStatusCode.BadRequest;
            }

            var context = MachineController.HighLevel.WorkingContext.Clone();
            context.CurrentGuidWorkingParameterSet = setting.Guid;
            context.CurrentNameWorkingParameterSet = name;
            context.Parameters = setting.Parameters;

            MachineController.HighLevel.WorkingContext = context;

            MachineController.HighLevel.Working.Material = context.CurrentNameWorkingParameterSet;

            ProConsole.WriteLine($"[API] ApplyWorkingSetting => guid:({MachineController.HighLevel.WorkingContext.CurrentGuidWorkingParameterSet})", ConsoleColor.Yellow);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingContext);

            if (MachineController.HighLevel.Status.MachineStopped)
            {
                bool preSyncStatus = MachineController.HighLevel.Status.CradleInSync;

                if (preSyncStatus)
                {
                    MachineController.Devices.Cradle.SetSync(false);
                }

                //---------------------------------
                // Send settings to low level
                //---------------------------------
                MachineController.Communicator.SetMachineLowLevelSettings(
                    MachineController.HighLevel.Settings.LowLevelMotion,
                    MachineController.HighLevel.Settings.HighLevel.FunctionsEnabled,
                    MachineController.HighLevel.Settings.HighLevel.MachineParameters);

                MachineController.Communicator.SetStraightRoller(MachineController.HighLevel.WorkingContext.Parameters.StraightRoller);

                if (preSyncStatus)
                {
                    MachineController.Devices.Cradle.SetSync(true);
                }
            }

            return HttpStatusCode.OK;
        }

        [HttpGet("save")]
        public HttpStatusCode SaveCurrentSettings()
        {
            ProConsole.WriteLine($"[API] SaveCurrentSettings", ConsoleColor.Yellow);

            HighLevel.WorkingContext context = MachineController.HighLevel.WorkingContext.Clone();

            var setting = MachineController.HighLevel.WorkingsSettings.Items.Where(x => x.Guid == context.CurrentGuidWorkingParameterSet).First();
            setting.Guid = Guid.NewGuid();
            setting.Timestamp = DateTime.Now;
            setting.Parameters = context.Parameters;

            ApplyWorkingSetting(context.CurrentNameWorkingParameterSet);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

            return HttpStatusCode.OK;
        }

        [HttpGet("add_default")]
        public HttpStatusCode AddDefaultWorkingSetting()
        {
            ProConsole.WriteLine($"[API] AddDefaultWorkingSetting", ConsoleColor.Yellow);

            var setting = new WorkingSetting
            {
                Parameters = new WorkingParameters(),
                Name = $"DEFAULT ({DateTime.Now.ToString("HHmmssff")})"
            };

            MachineController.HighLevel.WorkingsSettings.Items.Add(setting);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

            return HttpStatusCode.OK;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddWorkingSetting()
        {
            ProConsole.WriteLine($"[API] AddWorkingSetting", ConsoleColor.Yellow);

            var json = await Helpers.GetContentFromBody(Request);
            var setting = ProRob.Json.Deserialize<WorkingSetting>(json);

            bool exist = true;

            exist = MachineController.HighLevel.WorkingsSettings.Items.Where(x => x.Name == setting.Name).Count() > 0;
            if (exist)
            {
                return Conflict("A setting with this name already exists.");
            }

            exist = MachineController.HighLevel.WorkingsSettings.Items.Where(x => x.Guid == setting.Guid).Count() > 0;
            if (exist)
            {
                return Conflict("A setting with this GUID already exists.");
            }

            MachineController.HighLevel.WorkingsSettings.Items.Add(setting);

            ApplyWorkingSetting(setting.Name);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

            return Ok();
        }

        [HttpGet("rename_by_id")]
        public HttpStatusCode RenameWorkingSettingByID(Guid guid, string name)
        {
            ProConsole.WriteLine($"[API] RenameWorkingSetting({guid},{name})", ConsoleColor.Yellow);

            bool exist = MachineController.HighLevel.WorkingsSettings.Items.Where(x => x.Name == name).Count() > 0;

            if (exist)
            {
                return HttpStatusCode.BadRequest;
            }

            var s = MachineController.HighLevel.WorkingsSettings.Items.Where(x => x.Guid == guid);

            if (s.Count() > 0)
            {
                s.First().Name = name;

                DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

                return HttpStatusCode.OK;
            }

            return HttpStatusCode.BadRequest;
        }

        [HttpGet("rename")]
        public HttpStatusCode RenameWorkingSetting(string name, string newName)
        {
            ProConsole.WriteLine($"[API] RenameWorkingSetting({name},{newName})", ConsoleColor.Yellow);

            bool exist = MachineController.HighLevel.WorkingsSettings.Items.Where(x => x.Name == name).Count() > 0;

            if (exist == false)
            {
                return HttpStatusCode.BadRequest;
            }

            var ws = MachineController.HighLevel.WorkingsSettings.Items.Where(x => x.Name == name);

            if (ws.Count() > 0)
            {
                ws.First().Name = newName;

                DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

                return HttpStatusCode.OK;
            }

            return HttpStatusCode.BadRequest;
        }

        [HttpGet("reset")]
        public HttpStatusCode ResetCurrentWorkingSettingToCurrentSelectedGuid()
        {
            ProConsole.WriteLine($"[API] ResetCurrentWorkingSettingToCurrentSelectedSetting", ConsoleColor.Yellow);

            ApplyWorkingSetting(MachineController.HighLevel.WorkingContext.CurrentNameWorkingParameterSet);

            return HttpStatusCode.OK;
        }

        [HttpDelete("")]
        public HttpStatusCode DeleteWorkingSetting(string name)
        {
            ProConsole.WriteLine($"[API] DeleteWorkingSetting({name})", ConsoleColor.Yellow);

            var count = MachineController.HighLevel.WorkingsSettings.Items.RemoveAll(x => x.Name == name);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

            return count > 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        [HttpGet("delete")]
        public HttpStatusCode DeleteFromGetRequestWorkingSetting(string name)
        {
            ProConsole.WriteLine($"[API] DeleteFromGetRequestWorkingSetting({name})", ConsoleColor.Yellow);

            var count = MachineController.HighLevel.WorkingsSettings.Items.RemoveAll(x => x.Name == name);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

            return count > 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        [HttpGet("delete_by_id")]
        public HttpStatusCode DeleteWorkingSettingByID(Guid guid)
        {
            ProConsole.WriteLine($"[API] DeleteWorkingSetting({guid})", ConsoleColor.Yellow);

            var count = MachineController.HighLevel.WorkingsSettings.Items.RemoveAll(x => x.Guid == guid);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

            return count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound;
        }
    }
}