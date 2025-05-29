using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.HighLevel.StateMachine
{
    public partial class StateMachineManager
    {
        private void ExecutingPreSetCancellationOperations(ControlState state)
        {
            Console.WriteLine($"ExecutingPreSetCancellationOperations({state})");

            switch (state)
            {
                case ControlState.WaitMarch:
                    {
                        //--
                    }
                    break;

                case ControlState.Normal:
                    {
                        //--
                    }
                    break;

                case ControlState.ManualOperations:
                    {
                        //--
                    }
                    break;

                case ControlState.LoadUnload:
                    {
                        //--
                    }
                    break;

                case ControlState.Null:
                    {
                        //--
                    }
                    break;

                case ControlState.CutOff:
                    {
                        //--
                    }
                    break;

                case ControlState.Sharpening:
                    {
                        //--
                    }
                    break;

                case ControlState.CradleJog:
                    {
                        //--
                    }
                    break;

                case ControlState.CradleJogLoadUnload:
                    {
                        //--
                    }
                    break;

                case ControlState.CradleJogManualOperations:
                    {
                        //--
                    }
                    break;

                case ControlState.CradleRewind:
                    {
                        //--
                    }
                    break;
            }
        }
    }
}

