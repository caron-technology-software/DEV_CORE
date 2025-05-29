using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;

using ProRob;
using ProRob.Extensions.Object;

using Machine.Utility;

using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.Database;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("workings_settings")]
    public class WorkingsSettingsController : CradleApiController
    {
        [Route("")]
        [HttpGet]
        public WorkingsSettings GetWorkingsSettings()
        {
            ProConsole.WriteLine($"[API] GetWorkingsSettings()", ConsoleColor.Yellow);

            return MachineController.HighLevel.WorkingsSettings;
        }

        [Route("current")]
        [HttpGet]
        public IHttpActionResult GetCurrentWorkingsSettings()
        {
            ProConsole.WriteLine($"[API] GetCurrentWorkingsSettings()", ConsoleColor.Yellow);

            var name = MachineController.HighLevel.WorkingContext.CurrentNameWorkingParameterSet;
            var parameters = MachineController.HighLevel.WorkingContext.Parameters;

            return Ok(new { Name = name, Parameters = parameters });
        }

        [Route("by_name")]
        [HttpGet]
        public IHttpActionResult GetWorkingsSettingsByName(string name)
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

        [Route("")]
        [HttpPost]
        public HttpStatusCode SetWorkingsSettings([FromBody] WorkingsSettings workingsSettings)
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

        [Route("names")]
        [HttpGet]
        public IHttpActionResult GetWorkingsSettingsNames()
        {
            ProConsole.WriteLine($"[API] GetWorkingsSettingsNames()", ConsoleColor.Yellow);

            return Ok(MachineController.HighLevel.WorkingsSettings.Items.Select(x => x.Name));
        }

        [Route("apply")]
        [HttpGet]
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

        [Route("save")]
        [HttpGet]
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

        [Route("add_default")]
        [HttpGet]
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

        [Route("add")]
        [HttpPost]
        public HttpStatusCode AddWorkingSetting()
        {
            ProConsole.WriteLine($"[API] AddWorkingSetting", ConsoleColor.Yellow);

            var json = ProRob.WebApi.Helpers.GetContentFromBody(Request);
            var setting = ProRob.Json.Deserialize<WorkingSetting>(json);

            bool exist = true;

            exist = MachineController.HighLevel.WorkingsSettings.Items.Where(x => x.Name == setting.Name).Count() > 0;
            if (exist)
            {
                return HttpStatusCode.Conflict;
            }

            exist = MachineController.HighLevel.WorkingsSettings.Items.Where(x => x.Guid == setting.Guid).Count() > 0;
            if (exist)
            {
                return HttpStatusCode.Conflict;
            }

            MachineController.HighLevel.WorkingsSettings.Items.Add(setting);

            ApplyWorkingSetting(setting.Name);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

            return HttpStatusCode.OK;
        }

        [Route("rename_by_id")]
        [HttpGet]
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

        [Route("rename")]
        [HttpGet]
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

        [Route("reset")]
        [HttpGet]
        public HttpStatusCode ResetCurrentWorkingSettingToCurrentSelectedGuid()
        {
            ProConsole.WriteLine($"[API] ResetCurrentWorkingSettingToCurrentSelectedSetting", ConsoleColor.Yellow);

            ApplyWorkingSetting(MachineController.HighLevel.WorkingContext.CurrentNameWorkingParameterSet);

            return HttpStatusCode.OK;
        }

        [Route("")]
        [HttpDelete]
        public HttpStatusCode DeleteWorkingSetting(string name)
        {
            ProConsole.WriteLine($"[API] DeleteWorkingSetting({name})", ConsoleColor.Yellow);

            var count = MachineController.HighLevel.WorkingsSettings.Items.RemoveAll(x => x.Name == name);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

            return count > 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        [Route("delete")]
        [HttpGet]
        public HttpStatusCode DeleteFromGetRequestWorkingSetting(string name)
        {
            ProConsole.WriteLine($"[API] DeleteFromGetRequestWorkingSetting({name})", ConsoleColor.Yellow);

            var count = MachineController.HighLevel.WorkingsSettings.Items.RemoveAll(x => x.Name == name);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

            return count > 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        [Route("delete_by_id")]
        [HttpGet]
        public HttpStatusCode DeleteWorkingSettingByID(Guid guid)
        {
            ProConsole.WriteLine($"[API] DeleteWorkingSetting({guid})", ConsoleColor.Yellow);

            var count = MachineController.HighLevel.WorkingsSettings.Items.RemoveAll(x => x.Guid == guid);

            DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

            return count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound;
        }
    }
}