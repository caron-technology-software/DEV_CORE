using System;
using System.Linq;
using System.Text;

using ProRob;
using ProRob.Extensions.Object;

using Caron.Cradle.Control;
using Caron.Cradle.Control.Database;
using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.LowLevel;
//GPIx7
using Machine.Control.LowLevel.MachineController;
//GPFx7
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("external_commands")]
    public class ExternalCommandsController : CradleApiController
    {

        //GPIx7
        #region ShutdownTwincat2

        [HttpGet]
        [Route("shutdown_twincat2")]
        public IActionResult ShutdownTwincat2()
        {
            ProConsole.WriteLine("[API] shutdown_twincat2", ConsoleColor.Yellow);
            MachineControllerManager.ShutdownTwincat2();
            return Ok();
        }

        #endregion
        //GPFx7

        [HttpGet]
        [Route("status")]
        public IActionResult Status()
        {
            ProConsole.WriteLine($"[API] Status()", ConsoleColor.Yellow);

            var status = new ZundConnection()
            {
                Status = MachineController.LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.ZundStatus],
                Error = MachineController.LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.ZundError],
                CradleCutterLock = MachineController.LowLevel.IO.DigitalOutputs[(byte)DigitalOutput.CradleCutterLock],
            };

            return Ok(status);
        }

        [HttpGet]
        [Route("cut")]
        public IActionResult Cut()
        {
            ProConsole.WriteLine($"[API] Cut()", ConsoleColor.Yellow);

            MachineController.HighLevel.Signals.Cut = true;

            return Ok();
        }

        //GPIx23
//        [HttpGet]
//        [Route("shutdownMachine")]
//        public IHttpActionResult ShutdownMachine()
//        {
//            ProConsole.WriteLine($"[API] ShutdownMachine()");

//#if !DEBUG || !TEST

//            //GPIx7
//            if (!MachineControllerApplication.appIsSwitchingOff)
//            {
//                MachineControllerApplication.appIsSwitchingOff = true;
//            }
//            //GPFx7

//            MachineController.Communicator.SendCommand(LowLevel.Command.Emergency);

//#endif

//            Task.Run(() =>
//            {
//                ProcessHelper.CloseKillProcess("CradleUI.exe", TimeSpan.FromSeconds(5));
//                Thread.Sleep(2000);
//                MachineController.Application.Shutdown();
//            });

//            return Ok();
//        }
        //GPFx23

        [HttpGet]
        [Route("cut/status")]
        public IActionResult CutStatus()
        {
            ProConsole.WriteLine($"[API] CutStatus()", ConsoleColor.Yellow);

            if ((CutterState)MachineController.LowLevel.Info.CutterState == CutterState.WaitingCommand)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        [HttpGet]
        [Route("sync/status")]
        public IActionResult GetSync()
        {
            ProConsole.WriteLine($"[API] GetSync()", ConsoleColor.Yellow);

            //if (MachineController.LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false && 
            //    MachineController.HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled)
            //{
            //    MachineController.HighLevel.Signals.EnableSync = false;
            //}

            //GPI20
            return Ok(MachineController.HighLevel.Status.CradleInSync); //Signals.EnableSync);
            //GPF20
        }

        [HttpGet]
        [Route("sync/enable")]
        public IActionResult EnableSync()
        {
            ProConsole.WriteLine($"[API] EnableSync()", ConsoleColor.Yellow);

            MachineController.HighLevel.Signals.EnableSync = true;

            return Ok();
        }

        [HttpGet]
        [Route("sync/disable")]
        public IActionResult DisableSync()
        {
            ProConsole.WriteLine($"[API] DisableSync()", ConsoleColor.Yellow);

            MachineController.HighLevel.Signals.EnableSync = false;

            return Ok();
        }

        //GPIx21
        #region DigitalOutput     
        [HttpGet]
        [Route("set_digital_output")]
        public IActionResult SetDigitalOutput(int index, int value)
        {
            ProConsole.WriteLine($"[API] SetDigitalOutput({index} - {value})", ConsoleColor.Yellow);

            MachineController.Communicator.SetDigitalOutput(index, value > 0);

            return Ok();
        }

        //SetEnableIOSettings
        [HttpGet]
        [Route("set_enable_io_settings")]
        public IActionResult SetEnableIOSettings(bool enable)
        {
            ProConsole.WriteLine($"[API] SetEnableIOSettings({enable})", ConsoleColor.Yellow);

            MachineController.Communicator.SetEnableIOSettings(enable);

            return Ok();
        }
        #endregion
        //GPFx21

    }
}