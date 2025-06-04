using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

using ProRob;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("cradle")]
    public class CradleController : CradleApiController
    {
        private void WaitLowLevelCommandState()
        {
            int counter = 0;
            while (MachineController.LowLevel.Info.MachineState != (byte)LowLevel.ControlState.WaitCommand)
            {
                counter++;
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            ProConsole.WriteLine($"[API] WaitLowLevelCommandState()=>counter:{counter}", ConsoleColor.DarkMagenta);
        }

        //----------------------------------------------
        // Jog
        //----------------------------------------------
        #region Jog
        [Route("start_jog/cw")]
        [HttpGet]
        public void StartJogCW()
        {
            ProConsole.WriteLine($"[API] StartJogCW()", ConsoleColor.Yellow);

            MachineController.Devices.ElectricDrives.SpoonUp();

            MachineController.HighLevel.Status.JogState = JogState.CwMode;
            MachineController.StateMachine.SetState(HighLevel.ControlState.CradleJog);
        }

        [Route("start_jog/acw")]
        [HttpGet]
        public void StartJogACW()
        {
            ProConsole.WriteLine($"[API] StartJogACW()", ConsoleColor.Yellow);

            MachineController.Devices.ElectricDrives.SpoonUp();

            MachineController.HighLevel.Status.JogState = JogState.AcwMode;
            MachineController.StateMachine.SetState(HighLevel.ControlState.CradleJog);
        }

        [Route("stop_jog")]
        [HttpGet]
        public void StopJog()
        {
            ProConsole.WriteLine($"[API] StopJog()", ConsoleColor.Yellow);

            MachineController.Communicator.StopJog();
            MachineController.HighLevel.Status.JogState = JogState.Stopped;
            WaitLowLevelCommandState();
            MachineController.StateMachine.SetState(HighLevel.ControlState.Normal);
        }
        #endregion

        //----------------------------------------------
        // Jog Load/Unload
        //----------------------------------------------
        #region Jog Load/Unload
        [Route("start_jog_load_unload/cw")]
        [HttpGet]
        public void StartJogLoadUnloadCW()
        {
            ProConsole.WriteLine($"[API] StartJogLoadUnloadCW()", ConsoleColor.Yellow);

            MachineController.Devices.ElectricDrives.SpoonUp();

            MachineController.HighLevel.Status.JogState = JogState.CwMode;
            MachineController.StateMachine.SetState(HighLevel.ControlState.CradleJogLoadUnload);
        }

        [Route("start_jog_load_unload/acw")]
        [HttpGet]
        public void StartJogLoadUnloadACW()
        {
            ProConsole.WriteLine($"[API] StartJogACW()", ConsoleColor.Yellow);

            MachineController.Devices.ElectricDrives.SpoonUp();

            MachineController.HighLevel.Status.JogState = JogState.AcwMode;
            MachineController.StateMachine.SetState(HighLevel.ControlState.CradleJogLoadUnload);
        }

        [Route("stop_jog_load_unload")]
        [HttpGet]
        public void StopJogLoadUnload()
        {
            ProConsole.WriteLine($"[API] StopJogLoadUnload()", ConsoleColor.Yellow);

            MachineController.Communicator.StopJog();
            MachineController.HighLevel.Status.JogState = JogState.Stopped;
            WaitLowLevelCommandState();
            MachineController.StateMachine.SetState(HighLevel.ControlState.LoadUnload);
        }
        #endregion

        //----------------------------------------------
        // Jog Manual Operations
        //----------------------------------------------
        #region Jog Manual Operations
        [Route("start_jog_manual_operations/cw")]
        [HttpGet]
        public void StartJogManualOperationsCW()
        {
            ProConsole.WriteLine($"[API] StartJogManualOperationsCW()", ConsoleColor.Yellow);

            MachineController.Devices.ElectricDrives.SpoonUp();

            MachineController.HighLevel.Status.JogState = JogState.CwMode;
            MachineController.StateMachine.SetState(HighLevel.ControlState.CradleJogManualOperations);
        }

        [Route("start_jog_manual_operations/acw")]
        [HttpGet]
        public void StartJogManualOperationsACW()
        {
            ProConsole.WriteLine($"[API] StartJogManualOperationsCW()", ConsoleColor.Yellow);

            MachineController.Devices.ElectricDrives.SpoonUp();

            MachineController.HighLevel.Status.JogState = JogState.AcwMode;
            MachineController.StateMachine.SetState(HighLevel.ControlState.CradleJogManualOperations);
        }

        [Route("stop_jog_manual_operations")]
        [HttpGet]
        public void StopJogManualOperations()
        {
            ProConsole.WriteLine($"[API] StopJogManualOperations()", ConsoleColor.Yellow);

            MachineController.Communicator.StopJog();
            MachineController.HighLevel.Status.JogState = JogState.Stopped;
            WaitLowLevelCommandState();
            MachineController.StateMachine.SetState(HighLevel.ControlState.ManualOperations);
        }
        #endregion

        //----------------------------------------------
        // Rewind
        //----------------------------------------------
        #region Rewind
        [Route("start_rewind")]
        [HttpGet]
        public void StartRewind()
        {
            ProConsole.WriteLine($"[API] StartRewind()", ConsoleColor.Yellow);

            MachineController.Devices.ElectricDrives.SpoonUp();

            MachineController.HighLevel.Status.JogState = JogState.CwMode;
            MachineController.StateMachine.SetState(HighLevel.ControlState.CradleRewind);
        }

        [Route("stop_rewind")]
        [HttpGet]
        public void StopRewind()
        {
            ProConsole.WriteLine($"[API] StopRewind()", ConsoleColor.Yellow);

            MachineController.Communicator.StopJog();
            MachineController.HighLevel.Status.JogState = JogState.Stopped;
            WaitLowLevelCommandState();
            MachineController.StateMachine.SetState(HighLevel.ControlState.LoadUnload);
        }
        #endregion
    }
}