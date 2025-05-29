using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
//GPIx7
using Machine.Control.LowLevel.MachineController;
//GPFx7
using ProRob;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("application")]
    public class ApplicationController : CradleApiController
    {
        [HttpGet]
        [Route("restart")]
        public void Restart()
        {
            ProConsole.WriteLine($"[API] Restart()", ConsoleColor.Yellow);

            MachineController.Application.Restart();
        }

        [HttpGet]
        [Route("close")]
        public IHttpActionResult Close()
        {
            ProConsole.WriteLine($"[API] Shutdown()", ConsoleColor.Yellow);

#if !DEBUG || !TEST

            //GPIx7
            if (!MachineControllerApplication.appIsSwitchingOff)
            {
                MachineControllerApplication.appIsSwitchingOff = true;
            }
            //GPFx7

            MachineController.Communicator.SendCommand(LowLevel.Command.Emergency);
#endif
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                MachineController.Application.Stop();
            });

            return Ok();
        }

        [HttpGet]
        [Route("shutdown")]
        public IHttpActionResult ShutdownIndustrialPC()
        {
            ProConsole.WriteLine($"[API] ShutdownIndustrialPC()", ConsoleColor.Yellow);
#if !DEBUG || !TEST

            //GPIx7
            if (!MachineControllerApplication.appIsSwitchingOff)
            {
                MachineControllerApplication.appIsSwitchingOff = true;
            }
            //GPFx7

            MachineController.Communicator.SendCommand(LowLevel.Command.Emergency);

            //GPIx7         questo comando lancia lo shutdown del twincat2:     [togliere rem per mandare comando al twincat2 di spegnimento] 
            //ProConsole.WriteLine("[API] shutdown_twincat2", ConsoleColor.Yellow);
            //MachineControllerManager.ShutdownTwincat2();
            //GPFx7
#endif

            Task.Run(() =>
            {
                ProcessHelper.CloseKillProcess("CradleUI.exe", TimeSpan.FromSeconds(5));
                Thread.Sleep(2000);
                MachineController.Application.Shutdown();
            });

            return Ok();
        }

        [HttpGet]
        [Route("reboot")]
        public IHttpActionResult RebootIndustrialPC()
        {
            ProConsole.WriteLine($"[API] RebootIndustrialPC()", ConsoleColor.Yellow);
#if !DEBUG || !TEST

            MachineController.Communicator.SendCommand(LowLevel.Command.Emergency);
#endif
            Task.Run(() =>
            {
                ProcessHelper.CloseKillProcess("CradleUI.exe", TimeSpan.FromSeconds(5));
                Thread.Sleep(2000);
                MachineController.Application.Reboot();
            });

            return Ok();
        }
    }
}
