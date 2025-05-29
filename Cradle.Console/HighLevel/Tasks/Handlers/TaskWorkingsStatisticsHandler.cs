using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

using Machine.Utility;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.StateMachine;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        public void TaskCountingStatisticsHandler(CancellationToken cancellationToken)
        {
            //-------------------------------------
            // Wait State Machine
            //-------------------------------------
            while (StateMachine is null)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            //-------------------------------------
            // Task
            //-------------------------------------
            ProConsole.WriteLine("[ENTERING] TaskCountingStatisticsHandler", ConsoleColor.Green);
            ThreadsStarted++;

            double position = LowLevel.Axes.Cradle.Position;
            bool isCradleInMovement = false;
            bool cradleInSync = false;
            bool workingInProgress = false;
            bool loadUnloadState = false;
            bool cuttingState = false;
            bool isCradleInMovementAndInSync = false;

            double precPosition = position;
            bool precIsCradleInMovement = false;
            bool precCradleInSync = false;
            bool precWorkingInProgress = false;
            bool precLoadUnloadState = false;
            bool precCuttingState = false;
            bool precIsCradleInMovementAndInSync = false;

            DateTime dateTime = DateTime.UtcNow;
            DateTime precDateTime = dateTime;

            while (!cancellationToken.IsCancellationRequested)
            {
                precDateTime = dateTime;
                dateTime = DateTime.UtcNow;

                position = LowLevel.Axes.Cradle.Position;
                isCradleInMovement = Math.Abs(LowLevel.Axes.Cradle.Velocity) > Machine.Constants.Kinematics.MinVelocityToConsiderDeviceInMovement;
                cradleInSync = HighLevel.Status.CradleInSync;
                workingInProgress = HighLevel.WorkingStatus.InProgress;
                loadUnloadState = HighLevel.Status.HighLevelControlState == ControlState.CradleJogLoadUnload;
                cuttingState = HighLevel.Status.HighLevelControlState == ControlState.CutOff;
                isCradleInMovementAndInSync = isCradleInMovement && cradleInSync;

                if (Math.Abs(position - precPosition) > 100)
                {
                    precPosition = position;
                }

                double deltaPosition = position - precPosition;

                // Manipolazione segno
                if (HighLevel.WorkingContext.Parameters.StraightRoller)
                {
                    deltaPosition = -1.0 * deltaPosition;
                }


                TimeSpan deltaTime = dateTime - precDateTime;

#if TEST
                // (TEST) Simulation
                cradleInSync = isCradleInMovement = true;
#endif
                if (HighLevel.WorkingStatus.InProgress)
                {
                    if (cradleInSync)
                    {
                        HighLevel.Working.MaterialSpread += deltaPosition;

                        if (isCradleInMovement)
                        {
                            HighLevel.Working.TotalCradleInSyncAndInMovementTimeCounter += deltaTime;
                        }
                    }

                    if (workingInProgress)
                    {
                        HighLevel.Working.TotalTimeCounter += deltaTime;
                    }
                }

                //-------------------------------------------
                //Machine Events
                //-------------------------------------------

                //#
                if (cradleInSync == true && precCradleInSync == false)
                {
                    DatabaseWorkings.Add(new MachineEvent(MachineEventType.EnterSync));
                }
                else if (cradleInSync == false && precCradleInSync == true)
                {
                    DatabaseWorkings.Add(new MachineEvent(MachineEventType.ExitSync));
                }

                //#
                if (isCradleInMovementAndInSync == true && precIsCradleInMovementAndInSync == false)
                {
                    DatabaseWorkings.Add(new MachineEvent(MachineEventType.StartMovementInSync));
                }
                else if (isCradleInMovementAndInSync == false && precIsCradleInMovementAndInSync == true)
                {
                    DatabaseWorkings.Add(new MachineEvent(MachineEventType.StopMovementInSync));
                }

                //#
                if (cuttingState == true && precCuttingState == false)
                {
                    DatabaseWorkings.Add(new MachineEvent(MachineEventType.StartCutOff));
                }
                else if (cuttingState == false && precCuttingState == true)
                {
                    DatabaseWorkings.Add(new MachineEvent(MachineEventType.StopCutOff));
                }

                //#
                if (loadUnloadState == true && precLoadUnloadState == false)
                {
                    DatabaseWorkings.Add(new MachineEvent(MachineEventType.EnterLoadUnload));
                }
                else if (loadUnloadState == false && precLoadUnloadState == true)
                {
                    DatabaseWorkings.Add(new MachineEvent(MachineEventType.ExitLoadUnload));
                }

                //------------------------------------------------------
                precPosition = position;
                precIsCradleInMovement = isCradleInMovement;
                precCradleInSync = cradleInSync;
                precWorkingInProgress = workingInProgress;
                precLoadUnloadState = loadUnloadState;
                precCuttingState = cuttingState;
                precIsCradleInMovementAndInSync = isCradleInMovementAndInSync;

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            ProConsole.WriteLine("[EXITING] TaskCountingStatisticsHandler", ConsoleColor.Red);
        }
    }
}