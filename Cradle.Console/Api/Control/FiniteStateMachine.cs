using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using ProRob;

using Caron.Cradle.Control.HighLevel;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("state_machine")]
    public class FiniteStateMachineController : CradleApiController
    {
        [HttpGet]
        [Route("current")]
        public string GetState()
        {
            return MachineController.StateMachine.ControlState.ToString();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult SetHighLevelControlState(string state)
        {
            state = state.ToLower();

            ProConsole.WriteLine($"[API] ControlState::SetState({state})", ConsoleColor.Yellow);

            switch (state)
            {
                case "null":
                    MachineController.StateMachine.SetState(ControlState.Null);
                    break;

                //GPIx21
                case "io_config":
                    MachineController.StateMachine.SetState(ControlState.IOConfig);
                    break;
                //GPFx21

                case "emergency":
                    MachineController.StateMachine.SetState(ControlState.Emergency);
                    break;

                case "normal":
                    MachineController.StateMachine.SetState(ControlState.Normal);
                    break;

                case "manual_operations":
                    MachineController.StateMachine.SetState(ControlState.ManualOperations);
                    break;

                case "load_unload":
                    MachineController.StateMachine.SetState(ControlState.LoadUnload);
                    break;

                case "wait_march":
                    MachineController.StateMachine.SetState(ControlState.WaitMarch);
                    break;

                case "cutoff":
                    MachineController.StateMachine.SetState(ControlState.CutOff);
                    break;

                case "sharpening":
                    MachineController.StateMachine.SetState(ControlState.Sharpening);
                    break;

                case "cradle_jog":
                    MachineController.StateMachine.SetState(ControlState.CradleJog);
                    break;

                case "cradle_rewind":
                    MachineController.StateMachine.SetState(ControlState.CradleRewind);
                    break;

                case "cradle_jog_load_unload":
                    MachineController.StateMachine.SetState(ControlState.CradleJogLoadUnload);
                    break;
            }

            return Ok();
        }

        [HttpGet]
        [Route("sub_state")]
        public string GetSubState()
        {
            return MachineController.StateMachine.GetStringSubState();
        }
    }
}
