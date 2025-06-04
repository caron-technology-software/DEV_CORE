using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;

using ProRob;
using ProRob.Extensions.Object;

using Caron.Cradle.Control;
using Caron.Cradle.Control.Database;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("endurance")]
    public class MachineEnduranceController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public HighLevel.Settings.MachineEndurance GetMachineEndurance()
        {
            ProConsole.WriteLine($"[API] GetMachineEndurance()", ConsoleColor.Yellow);

            return MachineController.HighLevel.MachineEndurance;
        }

        [HttpPost]
        [Route("")]
        public IActionResult SetMachineEndurance([FromBody] HighLevel.Settings.MachineEndurance machineEndurance)
        {
            ProConsole.WriteLine($"[API] SetMachineEndurance()", ConsoleColor.Yellow);

            MachineController.HighLevel.MachineEndurance = machineEndurance.Clone();

            Console.WriteLine(machineEndurance);

            DatabaseSettings.Update(machineEndurance);

            return Ok(DateTime.Now);
        }
    }
}