using System;
using System.Web.Http;

using ProRob;

using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("cutter")]
    public class CutterController : CradleApiController
    {
        [HttpGet]
        [Route("cut_off/operator_side")]
        public void CutOffOperatorSide()
        {
            float velocity = 1.0f;

            ProConsole.WriteLine($"[API] CutOffOperatorSide({velocity})", ConsoleColor.Yellow);
            MachineController.Communicator.SendCommand(Command.SetControlState, (byte)ControlState.CutOff);
            MachineController.Communicator.SendCommand(Command.CutVersusOperatorSide, velocity);
        }

        [HttpGet]
        [Route("cut_off/motor_side")]
        public void CutOffMotorSide()
        {
            float velocity = (float)MachineController.HighLevel.WorkingContext.Parameters.CutterVelocity;

            ProConsole.WriteLine($"[API] CutOffMotorSide({velocity})", ConsoleColor.Yellow);
            MachineController.Communicator.SendCommand(Command.SetControlState, (byte)ControlState.CutOff);
            MachineController.Communicator.SendCommand(Command.CutVersusMotorSide, velocity);
        }

        [HttpGet]
        [Route("sharpening/enable")]
        public void SharpeningEnable()
        {
            ProConsole.WriteLine($"[API] SharpeningEnable", ConsoleColor.Yellow);
            MachineController.HighLevel.Status.SharpeningEnabled = true;
        }

        [HttpGet]
        [Route("sharpening/disable")]
        public void SharpeningDisable()
        {
            ProConsole.WriteLine($"[API] SharpeningDisable", ConsoleColor.Yellow);
            MachineController.HighLevel.Status.SharpeningEnabled = false;
        }

        [HttpGet]
        [Route("stop")]
        public void CutterStop()
        {
            ProConsole.WriteLine($"[API] CutterStop()", ConsoleColor.Yellow);
            MachineController.Communicator.SendCommand(Command.CutterStop);
        }
    }
}
