using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

using ProRob;

using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("command")]
    public class LowLevelActionsController : CradleApiController
    {
        [HttpGet]
        [Route("stop")]
        public IHttpActionResult StopAllActions()
        {
            ProConsole.WriteLine("[API] StopAllActions", ConsoleColor.Yellow);

            MachineController.Devices.ElectricDrives.StopAllActions();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("autocentering")]
        public IHttpActionResult Autocentering()
        {
            ProConsole.WriteLine("[API] Autocentering", ConsoleColor.Yellow);

            MachineController.Devices.ElectricDrives.StartTaskAutoCentering();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("material_alignment")]
        public IHttpActionResult MaterialAlignment()
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
        public IHttpActionResult StopTaskAlignment()
        {
            ProConsole.WriteLine("[API] StopTaskAlignment", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopTaskAlignment();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("start_alignment_mt_side")]
        public IHttpActionResult StartAlignmentMotorSide()
        {
            ProConsole.WriteLine("[API] start_alignment_mt_side", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartAlignmentMotorSide();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_alignment_mt_side")]
        public IHttpActionResult StopAlignmentMotorSide()
        {
            ProConsole.WriteLine("[API] stop_alignment_mt_side", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopAlignmentMotorSide();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("start_alignment_op_side")]
        public IHttpActionResult StartAlignmentOperatorSide()
        {
            ProConsole.WriteLine("[API] start_alignment_op_side", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartAlignmentOperatorSide();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_alignment_op_side")]
        public IHttpActionResult StopAlignmentOperatorSide()
        {
            ProConsole.WriteLine("[API] stop_alignment_op_side", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopAlignmentOperatorSide();

            return Ok(DateTime.Now);
        }
        #endregion

        #region Spoon
        [HttpGet]
        [Route("spoon_up")]
        public IHttpActionResult SpoonUp()
        {
            ProConsole.WriteLine("[API] spoon_up", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.SpoonUp();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("spoon_down")]
        public IHttpActionResult SpoonDown()
        {
            ProConsole.WriteLine("[API] spoon_down", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.SpoonDown();

            return Ok(DateTime.Now);
        }
        #endregion

        #region Cradle Up/Down
        [HttpGet]
        [Route("start_cradle_up")]
        public IHttpActionResult StartCradleUp()
        {
            ProConsole.WriteLine("[API] start_cradle_up", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartCradleUp();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_cradle_up")]
        public IHttpActionResult StopCradleUp()
        {
            ProConsole.WriteLine("[API] stop_cradle_up", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopCradleUp();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("start_cradle_down")]
        public IHttpActionResult StartCradleDown()
        {
            ProConsole.WriteLine("[API] start_cradle_down", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartCradleDown();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_cradle_down")]
        public IHttpActionResult StopCradleDown()
        {
            ProConsole.WriteLine("[API] stop_cradle_down", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopCradleDown();

            return Ok(DateTime.Now);
        }
        #endregion

        #region Titan
        [HttpGet]
        [Route("start_titan_up")]
        public IHttpActionResult StartTitanUp()
        {
            ProConsole.WriteLine("[API] StartTitanUp", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartTitanUp();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_titan_up")]
        public IHttpActionResult StopTitanUp()
        {
            ProConsole.WriteLine("[API] StopTitanUp", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopTitanUp();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("start_titan_down")]
        public IHttpActionResult StartTitanDown()
        {
            ProConsole.WriteLine("[API] StartTitanDown", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StartTitanDown();

            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("stop_titan_down")]
        public IHttpActionResult StopTitanDown()
        {
            ProConsole.WriteLine("[API] StopTitanDown", ConsoleColor.Yellow);
            MachineController.Devices.ElectricDrives.StopTitanDown();

            return Ok(DateTime.Now);
        }
        #endregion
    }
}
