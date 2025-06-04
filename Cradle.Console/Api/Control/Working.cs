using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using ProRob;
using ProRob.Extensions.Object;
using ProRob.WebApi;

using Machine.Utility;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.Settings;

using TicToc = ProRob.WebApi.TicToc;
using Microsoft.AspNetCore.Mvc;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("working")]
    public class WorkingController : CradleApiController
    {
        [HttpGet("get_current_Working")]
        public Working GetCurrentWorking()
        {
            //ProConsole.WriteLine($"[API] GetCurrentWorking()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Working;
        }

        [Route("set_working_name")]
        [HttpGet]
        public void SetWorkingName(string workingName)
        {
            ProConsole.WriteLine($"[API] SetWorkingName({workingName})", ConsoleColor.Yellow);

            if (workingName is null)
            {
                workingName = string.Empty;
            }

            MachineController.HighLevel.Working.WorkingName = workingName;
        }

        [Route("set_material")]
        [HttpGet]
        public void SetMaterial(string material)
        {
            ProConsole.WriteLine($"[API] SetMaterial({material})", ConsoleColor.Yellow);

            if (material is null)
            {
                material = string.Empty;
            }

            MachineController.HighLevel.Working.Material = material;
        }


        [Route("set_material_code")]
        [HttpGet]
        public void SetMaterialCode(string materialCode)
        {
            ProConsole.WriteLine($"[API] SetMaterialCode({materialCode})", ConsoleColor.Yellow);

            if (materialCode is null)
            {
                materialCode = string.Empty;
            }

            MachineController.HighLevel.Working.MaterialCode = materialCode;
        }

        [Route("new")]
        [HttpGet]
        public void New()
        {
            ProConsole.WriteLine($"[API] New", ConsoleColor.Yellow);

            var ws = new Working();
            ws.StartDateTime = DateTime.Now;
            ws.Material = MachineController.HighLevel.WorkingContext.CurrentNameWorkingParameterSet;
            MachineController.HighLevel.Working = ws.Clone();
        }

        [Route("save")]
        [HttpGet]
        public void Save()
        {
            ProConsole.WriteLine($"[API] Save", ConsoleColor.Yellow);

            MachineController.HighLevel.Working.StopDateTime = DateTime.Now;
            DatabaseWorkings.Add(MachineController.HighLevel.Working);

            MachineController.HighLevel.WorkingStatus.InProgress = false;
            MachineController.HighLevel.Working = new Working();
        }

        [Route("add_load_unload_time")]
        [HttpGet]
        public void AddLoadUnloadTime(long milliseconds)
        {
            ProConsole.WriteLine($"[API] AddLoadUnloadTime({milliseconds} ms)", ConsoleColor.Yellow);

            MachineController.HighLevel.Working.TotalTimeLoadUnloadCounter += TimeSpan.FromMilliseconds(milliseconds);
        }

        [Route("reset")]
        [HttpGet]
        public void ResetWorkingsStatistics()
        {
            ProConsole.WriteLine($"[API] ResetWorkingsStatistics()", ConsoleColor.Yellow);

            var ws = MachineController.HighLevel.Working;
            ws.MaterialSpread = 0.0;
            ws.TotalTimeCounter = TimeSpan.FromMilliseconds(0);
            ws.TotalCradleInSyncAndInMovementTimeCounter = TimeSpan.FromMilliseconds(0);
            ws.TotalTimeLoadUnloadCounter = TimeSpan.FromMilliseconds(0);
        }

        [Route("reset_material_spread")]
        [HttpGet]
        public void ResetMaterialSpread()
        {
            ProConsole.WriteLine($"[API] ResetMaterialSpread()", ConsoleColor.Yellow);

            MachineController.HighLevel.Working.MaterialSpread = 0.0;
        }

        [Route("count")]
        [HttpGet]
        [TicToc]
        public int Count()
        {
            ProConsole.WriteLine($"[API] Count", ConsoleColor.Yellow);

            return DatabaseWorkings.GetCount();
        }

        [Route("list")]
        [HttpGet]
        [TicToc]
        public async Task<IEnumerable<Working>> List()
        {
            ProConsole.WriteLine($"[API] List", ConsoleColor.Yellow);

            return await Task.Run(() => DatabaseWorkings.GetList().OrderBy(x => x.WorkingName));
        }
    }
}

