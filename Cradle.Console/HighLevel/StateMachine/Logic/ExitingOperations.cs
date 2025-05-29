using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProRob;

namespace Caron.Cradle.Control.HighLevel.StateMachine
{
    public partial class StateMachineManager
    {
        private void ExecutingExitingOperations(ControlState state)
        {
            Console.WriteLine($"ExecutingExitingOperations({state})");

            switch (state)
            {
                case ControlState.WaitMarch:
                    {
                        //----------------------------------------------------
                        // Invio configurazione macchina
                        //----------------------------------------------------
                        ProConsole.WriteLine("[EXITING FROM WAIT MARCH] Sending machine configuration..", ConsoleColor.Yellow);

                        Cradle.Communicator.SetLowLevelControlState(Control.LowLevel.ControlState.WaitCommand);
                        Cradle.Communicator.SetMachineLowLevelSettings(
                            Cradle.HighLevel.Settings.LowLevelMotion,
                            Cradle.HighLevel.Settings.HighLevel.FunctionsEnabled,
                            Cradle.HighLevel.Settings.HighLevel.MachineParameters);
                        Cradle.Communicator.SetScalingFactor(Cradle.HighLevel.WorkingContext.Parameters.CradleScalingFactor);
                        Cradle.Communicator.SetStraightRoller(Cradle.HighLevel.WorkingContext.Parameters.StraightRoller);

                        ChangeUIPagePermission = true;
                        Cradle.HighLevel.Status.JogState = JogState.Stopped;
                    }
                    break;

                case ControlState.Normal:
                    {
                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.ManualOperations:
                    {
                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.LoadUnload:
                    {
                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.Null:
                    {
                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.Emergency:
                    {
                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.Sharpening:
                    {
                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.CutOff:
                    {
                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.CradleJog:
                    {
                        ChangeUIPagePermission = true;
                        Cradle.HighLevel.Status.JogState = JogState.Stopped;
                    }
                    break;

                case ControlState.CradleJogLoadUnload:
                    {
                        ChangeUIPagePermission = true;
                        Cradle.HighLevel.Status.JogState = JogState.Stopped;
                    }
                    break;

                case ControlState.CradleJogManualOperations:
                    {
                        ChangeUIPagePermission = true;
                        Cradle.HighLevel.Status.JogState = JogState.Stopped;
                    }
                    break;

                case ControlState.CradleRewind:
                    {
                        ChangeUIPagePermission = true;
                        Cradle.HighLevel.Status.JogState = JogState.Stopped;
                    }
                    break;
            }
        }
    }
}
