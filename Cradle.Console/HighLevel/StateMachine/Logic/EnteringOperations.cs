using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caron.Cradle.Control.HighLevel;
using ProRob;

namespace Caron.Cradle.Control.HighLevel.StateMachine
{
    public partial class StateMachineManager
    {
        private void ExecutingEnteringOperations(ControlState state)
        {
            Console.WriteLine($"ExecutingEnteringOperations({state})");

            switch (state)
            {
                case ControlState.WaitMarch:
                    {
                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.Normal:
                    {
                        Cradle.Devices.ElectricDrives.StopAllActions();
                        Cradle.Devices.ElectricDrives.StopTaskAlignment();
                        Cradle.Devices.ElectricDrives.StopTaskAutoCentering();

                        Cradle.Devices.Cradle.SetEnteringNormalStateSync();

                        Cradle.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = true;

                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.ManualOperations:
                    {
                        Cradle.Devices.ElectricDrives.StopTaskAlignment();
                        Cradle.Devices.ElectricDrives.StopTaskAutoCentering();

                        Cradle.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = false;

                        Cradle.Devices.Cradle.SetLowLevelStateToWaitCommand();

                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.LoadUnload:
                    {
                        Cradle.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = false;

                        Cradle.Devices.ElectricDrives.StopTaskAlignment();
                        Cradle.Devices.ElectricDrives.StopAllActions();

                        Cradle.Devices.Cradle.SetLowLevelStateToWaitCommand();

                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.Null:
                    {
                        Cradle.Devices.ElectricDrives.StopAllActions();
                        Cradle.Devices.ElectricDrives.StopTaskAlignment();
                        Cradle.Devices.ElectricDrives.StopTaskAutoCentering();

                        Cradle.Devices.Cradle.SetLowLevelStateToWaitCommand();

                        Cradle.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = false;

                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.Emergency:
                    {
                        Cradle.Devices.ElectricDrives.StopAllActions();
                        Cradle.Devices.ElectricDrives.StopTaskAlignment();
                        Cradle.Devices.ElectricDrives.StopTaskAutoCentering();

                        Machine.Control.LowLevel.Communicator.SendShutdownCommand();

                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.CutOff:
                    {
                        Cradle.Devices.ElectricDrives.StopTaskAlignment();
                        Cradle.Devices.ElectricDrives.StopTaskAutoCentering();

                        Cradle.Devices.Cradle.SetLowLevelStateToWaitCommand();

                        Cradle.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = false;

                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.Sharpening:
                    {
                        Cradle.Devices.ElectricDrives.StopAllActions();
                        Cradle.Devices.ElectricDrives.StopTaskAlignment();
                        Cradle.Devices.ElectricDrives.StopTaskAutoCentering();

                        Cradle.Devices.Cradle.SetLowLevelStateToWaitCommand();

                        Cradle.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = false;

                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.CradleJog:
                    {
                        Cradle.Devices.ElectricDrives.StopAllActions();
                        Cradle.Devices.ElectricDrives.StopTaskAlignment();
                        Cradle.Devices.ElectricDrives.StopTaskAutoCentering();

                        Cradle.Devices.Cradle.SetLowLevelStateToWaitCommand();

                        Cradle.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = false;

                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.CradleJogManualOperations:
                    {
                        Cradle.Devices.ElectricDrives.StopAllActions();
                        Cradle.Devices.ElectricDrives.StopTaskAlignment();
                        Cradle.Devices.ElectricDrives.StopTaskAutoCentering();

                        Cradle.Devices.Cradle.SetLowLevelStateToWaitCommand();

                        Cradle.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = false;

                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.CradleJogLoadUnload:
                    {
                        Cradle.Devices.ElectricDrives.StopAllActions();
                        Cradle.Devices.ElectricDrives.StopTaskAlignment();
                        Cradle.Devices.ElectricDrives.StopTaskAutoCentering();

                        Cradle.Devices.Cradle.SetLowLevelStateToWaitCommand();

                        Cradle.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = false;

                        ChangeUIPagePermission = true;
                    }
                    break;

                case ControlState.CradleRewind:
                    {
                        Cradle.Devices.ElectricDrives.StopAllActions();
                        Cradle.Devices.ElectricDrives.StopTaskAlignment();
                        Cradle.Devices.ElectricDrives.StopTaskAutoCentering();

                        Cradle.Devices.Cradle.SetLowLevelStateToWaitCommand();

                        Cradle.HighLevel.TasksStatus.AlignmentDuringSpreadProcessActive = false;

                        ChangeUIPagePermission = true;
                    }
                    break;
            }
        }
    }
}