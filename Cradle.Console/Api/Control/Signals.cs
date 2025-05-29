using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

using ProRob;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("signal")]
    public class UISignalController : CradleApiController
    {
        [HttpGet]
        [Route("control")]
        public bool GetSignalControl()
        {
            return MachineController.HighLevel.Signals.ControlReady;
        }

        [HttpGet]
        [Route("ui")]
        public bool GetSignalUI()
        {
            if (MachineController is null)
            {
                return false;
            }

            return MachineController.HighLevel.Signals.UI;
        }

        [HttpGet]
        [Route("ui/set")]
        public void SetSignalUI()
        {
            if (MachineController is null)
            {
                return;
            }

            ProConsole.WriteLine($"[API] SetSignalUI()", ConsoleColor.Yellow);
            MachineController.HighLevel.Signals.UI = true;
        }

        [HttpGet]
        [Route("bootstrapper_ui_checker")]
        public bool GetSignalBoostrapperUIChecker()
        {
            if (MachineController is null)
            {
                return false;
            }

            return MachineController.HighLevel.Signals.BoostrapperUIChecker;
        }

        [HttpGet]
        [Route("bootstrapper_ui_checker/set")]
        public void SetSignalBoostrapperUIChecker()
        {
            if (MachineController is null)
            {
                return;
            }

            ProConsole.WriteLine($"[API] SetSignalBoostrapperUIChecker()", ConsoleColor.Yellow);
            MachineController.HighLevel.Signals.BoostrapperUIChecker = true;
        }

        [HttpGet]
        [Route("stop/set")]
        public void SetStopSignal()
        {
            if (MachineController is null)
            {
                return;
            }

            ProConsole.WriteLine($"[API] SetStopSignal()", ConsoleColor.Yellow);
            MachineController.HighLevel.Signals.Stop = true;
        }
    }
}
