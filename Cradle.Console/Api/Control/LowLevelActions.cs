using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ProRob;

using Caron.Cradle.Control.LowLevel;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("command")]
    public class LowLevelActionsController : CradleApiController
    {
        [HttpGet("stop")]
        public IActionResult StopAllActions()
        {
            ProConsole.WriteLine("[API] StopAllActions", ConsoleColor.Yellow);

            MachineController.Devices.ElectricDrives.StopAllActions();

            return Ok(DateTime.Now);
        }

        [HttpGet("autocentering")]
        public IActionResult Autocentering()
        {
            ProConsole.WriteLine("[API] Autocentering", ConsoleColor.Yellow);

            MachineController.Devices.ElectricDrives.StartTaskAutoCentering();

            return Ok(DateTime.Now);
        }


        [HttpGet]
        [Route("material_alignment")]
        public IActionResult MaterialAlignment()
        {
            ProConsole.WriteLine("[API] (START) MaterialAlignment", ConsoleColor.Yellow);

            //GPI12 sostituzione check temporale su PhotocellMaterialPresence:
            //bool bol01 = false;
            //if (MachineController.LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            //{
            //    bol01 = true;
            //}
            //else
            //{
            //    bol01 = false;
            //}
            //DateTime checkUntilPhotocellMaterialPresence = DateTime.MinValue;
            //checkUntilPhotocellMaterialPresence = DateTime.UtcNow + TimeSpan.FromMilliseconds(MachineController.HighLevel.Settings.HighLevel.MachineParameters.CheckUntilPhotocellMaterialPresence);   //TimeSpan.FromMilliseconds(250) parametro da mettere nella Cradle per intervallo di check fotocellula presenza materiale
            ////GPI18
            //while ((DateTime.UtcNow < checkUntilPhotocellMaterialPresence) && (!MachineControllerApplication.NoInitCheckPhotocell))
            ////GPF18
            //{
            //    // code block to be executed
            //    if (MachineController.LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            //    {
            //        bol01 = true;
            //    }
            //    else
            //    {
            //        bol01 = false;
            //        break;
            //    }
            //}
            //GPF12

            //GPI25
            //////if (MachineController.LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            if (MachineController.HighLevel.Status.PhotocelMaterialPresenceFiltered == false)
            //if (bol01)
            //GPF25
            {
                return Ok(DateTime.Now);
            }

            MachineController.Devices.ElectricDrives.StartAlignmentMaterialWithExitCondition();

            return Ok(DateTime.Now);
        }

        #region Alignment
        [HttpGet]
        [Route("stop_task_alignment")]
        public IActionResult StopTaskAlignment()
        {
            ProConsole.WriteLine("[API] StopTaskAlignment", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopTaskAlignment();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("start_alignment_mt_side")]
        public IActionResult StartAlignmentMotorSide()
        {
            ProConsole.WriteLine("[API] start_alignment_mt_side", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartAlignmentMotorSide();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_alignment_mt_side")]
        public IActionResult StopAlignmentMotorSide()
        {
            ProConsole.WriteLine("[API] stop_alignment_mt_side", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopAlignmentMotorSide();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("start_alignment_op_side")]
        public IActionResult StartAlignmentOperatorSide()
        {
            ProConsole.WriteLine("[API] start_alignment_op_side", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartAlignmentOperatorSide();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_alignment_op_side")]
        public IActionResult StopAlignmentOperatorSide()
        {
            ProConsole.WriteLine("[API] stop_alignment_op_side", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopAlignmentOperatorSide();

            return Ok(DateTime.Now);
        }
        #endregion

        #region Spoon
        [HttpGet]
        [Route("spoon_up")]
        public IActionResult SpoonUp()
        {
            ProConsole.WriteLine("[API] spoon_up", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.SpoonUp();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("spoon_down")]
        public IActionResult SpoonDown()
        {
            ProConsole.WriteLine("[API] spoon_down", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.SpoonDown();

            return Ok(DateTime.Now);
        }
        #endregion

        #region Cradle Up/Down
        [HttpGet]
        [Route("start_cradle_up")]
        public IActionResult StartCradleUp()
        {
            ProConsole.WriteLine("[API] start_cradle_up", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartCradleUp();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_cradle_up")]
        public IActionResult StopCradleUp()
        {
            ProConsole.WriteLine("[API] stop_cradle_up", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopCradleUp();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("start_cradle_down")]
        public IActionResult StartCradleDown()
        {
            ProConsole.WriteLine("[API] start_cradle_down", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartCradleDown();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_cradle_down")]
        public IActionResult StopCradleDown()
        {
            ProConsole.WriteLine("[API] stop_cradle_down", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopCradleDown();

            return Ok(DateTime.Now);
        }
        #endregion

        #region Titan
        [HttpGet]
        [Route("start_titan_up")]
        public IActionResult StartTitanUp()
        {
            ProConsole.WriteLine("[API] StartTitanUp", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartTitanUp();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_titan_up")]
        public IActionResult StopTitanUp()
        {
            ProConsole.WriteLine("[API] StopTitanUp", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopTitanUp();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("start_titan_down")]
        public IActionResult StartTitanDown()
        {
            ProConsole.WriteLine("[API] StartTitanDown", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartTitanDown();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_titan_down")]
        public IActionResult StopTitanDown()
        {
            ProConsole.WriteLine("[API] StopTitanDown", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopTitanDown();

            return Ok(DateTime.Now);
        }
        #endregion
    }
}
