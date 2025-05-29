using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.HighLevel.StateMachine
{
    //[TODO], [REFACTORING]
    public partial class StateMachineManager
    {
        public object GetSubState()
        {
            switch (Cradle.StateMachine.ControlState)
            {
                case ControlState.Null:
                    {
                        return Cradle.StateMachine.NullSubState;
                    }

                case ControlState.WaitMarch:
                    {
                        return Cradle.StateMachine.WaitMarchSubState;
                    }

                case ControlState.LoadUnload:
                    {
                        return Cradle.StateMachine.LoadUnloadSubState;
                    }

                //GPIx21
                case ControlState.IOConfig:
                    {
                        return Cradle.StateMachine.IOConfigSubState;
                    }
                //GPFx21

                case ControlState.ManualOperations:
                    {
                        return Cradle.StateMachine.ManualOperationsSubState;
                    }

                case ControlState.Normal:
                    {
                        return Cradle.StateMachine.NormalSubState;
                    }

                case ControlState.Sharpening:
                    {
                        return Cradle.StateMachine.SharpeningSubState;
                    }

                case ControlState.CutOff:
                    {
                        return Cradle.StateMachine.CutOffSubState;
                    }

                case ControlState.CradleJog:
                    {
                        return Cradle.StateMachine.CradleJogSubState;
                    }

                case ControlState.CradleRewind:
                    {
                        return Cradle.StateMachine.CradleRewindSubState;
                    }

                case ControlState.CradleJogLoadUnload:
                    {
                        return Cradle.StateMachine.CradleJogLoadUnloadSubState;
                    }

            } //switch

            return null;
        }

        public string GetStringSubState()
        {
            switch (Cradle.StateMachine.ControlState)
            {
                case ControlState.Null:
                    {
                        return Cradle.StateMachine.NullSubState.ToString();
                    }

                case ControlState.Emergency:
                    {
                        return Cradle.StateMachine.EmergencySubstate.ToString();
                    }

                case ControlState.WaitMarch:
                    {
                        return Cradle.StateMachine.WaitMarchSubState.ToString();
                    }

                case ControlState.LoadUnload:
                    {
                        return Cradle.StateMachine.LoadUnloadSubState.ToString();
                    }

                case ControlState.Normal:
                    {
                        return Cradle.StateMachine.NormalSubState.ToString();
                    }

                case ControlState.ManualOperations:
                    {
                        return Cradle.StateMachine.ManualOperationsSubState.ToString();
                    }

                case ControlState.Sharpening:
                    {
                        return Cradle.StateMachine.SharpeningSubState.ToString();
                    }

                case ControlState.CutOff:
                    {
                        return Cradle.StateMachine.CutOffSubState.ToString();
                    }

                case ControlState.CradleJog:
                    {
                        return Cradle.StateMachine.CradleJogSubState.ToString();
                    }

                case ControlState.CradleJogLoadUnload:
                    {
                        return Cradle.StateMachine.CradleJogLoadUnloadSubState.ToString();
                    }

                case ControlState.CradleJogManualOperations:
                    {
                        return Cradle.StateMachine.CradleJogManualOperationsSubState.ToString();
                    }

                case ControlState.CradleRewind:
                    {
                        return Cradle.StateMachine.CradleRewindSubState.ToString();
                    }
            } //switch

            return "unknown";
        }
    }
}
