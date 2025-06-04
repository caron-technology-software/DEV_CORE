#define ENABLE_FORCE_DISABLE_ROUTE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ProRob;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("working_mode")]
    public class WorkingModeController : CradleApiController
    {
        [Route("get_cradle_sync")]
        [HttpGet]
        public bool GetCradleSync()
        {
            ProConsole.WriteLine($"[API] GetCradleSync()", ConsoleColor.Yellow);

            return MachineController.HighLevel.Status.CradleInSync;
        }

        [Route("set_cradle_sync")]
        [HttpGet]
        public void SetCradleSync(bool value)
        {
            ProConsole.WriteLine($"[API] SetCradleSync({value})", ConsoleColor.Yellow);

            MachineController.Devices.Cradle.SetSync(value);
        }

#if ENABLE_FORCE_DISABLE_ROUTE
        [Route("cradle_sync/force_disable_is_true")]
        [HttpGet]
        public void SetForceDisableCradleSyncIsTrue()
        {
            ProConsole.WriteLine($"[API] SetForceDisableCradleSyncIsTrue", ConsoleColor.Yellow);
            MachineController.HighLevel.Status.ForceDisableCradleInSync = true;
            MachineController.HighLevel.Status.PromiseToEnableCradleInSyncAfterClick = false;//MMIx02
            Console.WriteLine($"ForceDisableCradleInSync:{MachineController.HighLevel.Status.ForceDisableCradleInSync}");
        }

        [Route("cradle_sync/force_disable_is_false")]
        [HttpGet]
        public void SetForceDisableCradleSyncIsFalse()
        {
            ProConsole.WriteLine($"[API] SetForceDisableCradleSyncIsFalse", ConsoleColor.Yellow);
            MachineController.HighLevel.Status.ForceDisableCradleInSync = false;
            MachineController.HighLevel.Status.PromiseToEnableCradleInSyncAfterClick = true;//MMIx02
            Console.WriteLine($"ForceDisableCradleInSync:{MachineController.HighLevel.Status.ForceDisableCradleInSync}");
        }
#endif
    }
}