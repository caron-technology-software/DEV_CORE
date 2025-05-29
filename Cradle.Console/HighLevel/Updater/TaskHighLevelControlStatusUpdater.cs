using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

using ProRob;

using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        void TaskHighLevelControlStatusUpdater(CancellationToken cancellationToken)
        {
            ProConsole.WriteLine("[ENTERING] TaskHighLevelControlStatusUpdater", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
            ThreadsStarted++;

            //GPIx164
            int oneMessageOnly1 = 0;
            int oneMessageOnly2 = 0;
            //GPFx164

            int nOutputs = Enum.GetNames(typeof(DigitalOutput)).Count();
            int nInputs = Enum.GetNames(typeof(DigitalInput)).Count();

            //------------------------------------------------
            // Machine Endurance
            //------------------------------------------------
            int statisticsCounter = 0;
            Stopwatch sw = new Stopwatch();

            bool[] precDigitalOutputs = new bool[nOutputs];
            bool[] precDigitalInputs = new bool[nInputs];

            bool precCradleInSync = false;
            bool precSharpeningEnabled = false;

            //GPI25
            uint iterWithPhotocelMaterialPresence = 0;
            //GPF25
            //MMIx02
            uint iterWithPhotocelRollPresence = 0;
            //MMFx02

            //------------------------------------------------
            // Wait StateMachine
            //------------------------------------------------
            while (StateMachine is null)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            ProConsole.WriteLine("[TaskHighLevelControlStatusUpdate] Starting loop..", ConsoleColor.Yellow);
            //sw.Start(); //GPIx93

            while (!cancellationToken.IsCancellationRequested)
            {
                //----------------------------------------------------------------
                // Conditions
                //----------------------------------------------------------------
                #region Conditions
                bool isCradleStopped = Math.Abs(LowLevel.Axes.Cradle.Velocity) < 5.0;
                bool isTableStopped = Math.Abs(LowLevel.Axes.Table.Velocity) < 5.0;
                bool inMarch = LowLevel.IO.MachineInputs[(int)MachineInput.MarchEnabled];
                bool cradleCommandSpeedAtZero = (LowLevel.Axes.Cradle.DriverCommandSpeed == 0);
                bool cutterWaitingCommand = (LowLevel.Info.CutterState == (byte)CutterState.WaitingCommand);
                bool spoonIsDown = LowLevel.IO.DigitalInputs[(int)DigitalInput.LimitSpoonDown];
                bool cutOffEnabled = isCradleStopped && isTableStopped && inMarch && cutterWaitingCommand;// && spoonIsDown; // && cradleCommandSpeedAtZero;
                bool isMachineStopped = isCradleStopped && isTableStopped;// && cradleCommandSpeedAtZero;
                #endregion

                //----------------------------------------------------------------
                // Status
                //----------------------------------------------------------------
                #region Status
                HighLevel.Status.HighLevelControlState = StateMachine.ControlState;
                HighLevel.Status.HighLevelControlSubState = StateMachine.GetSubState();
                HighLevel.Status.CutOffEnabled = cutOffEnabled;
                HighLevel.Status.InMarch = inMarch;
                HighLevel.Status.MachineStopped = isMachineStopped;
                HighLevel.Status.LowLevelControlState = (LowLevel.ControlState)LowLevel.Info.MachineState;
                #endregion

                //GPI25
                #region PhotocelMaterialPresence  
                if (!LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence])
                {
                    iterWithPhotocelMaterialPresence++;
                }
                else
                {
                    iterWithPhotocelMaterialPresence = 0;
                }

                //GPIy1  diviso per 15 CheckUntilPhotocellMaterialPresence:
                //////il Threshold è parametrizzato con CheckUntilPhotocellMaterialPresence:
                if (iterWithPhotocelMaterialPresence > (HighLevel.Settings.HighLevel.MachineParameters.CheckUntilPhotocellMaterialPresence / 15) )
                //if (iterWithPhotocelMaterialPresence > ThresholdItersPhotocelMaterialPresenceFiltered)
                {
                    //HighLevel.Status.PhotocelMaterialPresenceFiltered = true;
                    HighLevel.Status.PhotocelMaterialPresenceFiltered = false;
                }
                else
                {
                    //HighLevel.Status.PhotocelMaterialPresenceFiltered = false;
                    HighLevel.Status.PhotocelMaterialPresenceFiltered = true;
                }
                #endregion
                //GPF25

                //MMIx02
                #region PhotocellRollPresence  
                if (!LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellRollPresence])
                {
                    iterWithPhotocelRollPresence++;
                }
                else
                {
                    iterWithPhotocelRollPresence = 0;
                }
                //dividendo per 15 si ottiene il valore filtrato in millisecondi
                if (iterWithPhotocelRollPresence > (HighLevel.Settings.HighLevel.MachineParameters.CheckUntilPhotocelRollPresence / 15))
                {
                    HighLevel.Status.PhotocelRollPresenceFiltered = false;
                }
                else
                {
                    HighLevel.Status.PhotocelRollPresenceFiltered = true;
                }
                #endregion
                //MMFx02

                //----------------------------------------------------------------
                // Logic
                //----------------------------------------------------------------
                #region Logic
                if (HighLevel.Status.SharpeningEnabled == true && precSharpeningEnabled == false)
                {
                    HighLevel.Status.CradleSyncStatusBeforeSharpeningEnabled = HighLevel.Status.CradleInSync;
                }

                if (HighLevel.Status.SharpeningEnabled == false && precSharpeningEnabled == true)
                {
                    HighLevel.Status.CradleInSync = HighLevel.Status.CradleSyncStatusBeforeSharpeningEnabled;
                }
                #endregion

                //----------------------------------------------------------------
                // Errors
                //----------------------------------------------------------------
                #region Errors
                HighLevel.Errors.CommunicationError = CommunicationErrors;
                #endregion

                //----------------------------------------------------------------
                // Emergency
                //----------------------------------------------------------------
                #region Emergency
                if (HighLevel.Signals.ControlReady)
                {
                    //GPIx164
                    var timestamp = Constants.Errors.TimeSpanFromLastDataPacketReceived;

                    if (TimeSpanFromLastDataPacketReceived > timestamp)
                    {
                        Console.WriteLine($"TimeSpanFromLastDataPacketReceived: {TimeSpanFromLastDataPacketReceived}");

                        HighLevel.Errors.EmergencyStatus = true;

                        LowLevel.Info.HeartBeatState = true;
                        if (oneMessageOnly1 == 0)
                        {
                            ProConsole.WriteLine($"[EmergencyStatus Heartbeat] LowLevel.Info.HeartBeatState={LowLevel.Info.HeartBeatState}", ConsoleColor.Red);
                            oneMessageOnly1 = 1;
                        }

                        //HighLevel.Errors.FatalErrorDescription = Localization.ErrorHighLevelCommunication;   
                    }
                    else
                    {
                        LowLevel.Info.HeartBeatState = false;
                    }
                    //GPfx164

                    //ProConsole.WriteLine($"LowLevel.Info.EmergencyState:{LowLevel.Info.EmergencyState}", ConsoleColor.Red);  //GPIx164
                    if (LowLevel.IO.MachineInputs[(byte)MachineInput.DriverFault] || 
                        HighLevel.Errors.CommunicationError > 50 ||
                        LowLevel.Info.EmergencyState > 0
                        )
                    {
                        HighLevel.Errors.EmergencyStatus = true;
                        //GPIx164
                        bool b01 = (HighLevel.Errors.CommunicationError > 50);
                        if (oneMessageOnly2 == 0)
                        {
                            ProConsole.WriteLine($"[EmergencyStatus] LowLevel.IO.MachineInputs[(byte)MachineInput.DriverFault]:{LowLevel.IO.MachineInputs[(byte)MachineInput.DriverFault]} (HighLevel.Errors.CommunicationError > 50):{b01} LowLevel.Info.EmergencyState:{LowLevel.Info.EmergencyState}", ConsoleColor.Red);
                            oneMessageOnly2 = 1;
                        }
                        //Legenda LowLevel.Info.EmergencyState:
                        //NoErrors = 0,
                        //DriverFault = 1,
                        //EtherCATFault = 2,
                        //PositionError = 3,
                        //HeartbeatError = 4
                        //GPFx164
                    }

                    if (HighLevel.Errors.EmergencyStatus)
                    {
                        if (StateMachine.ControlState != ControlState.Emergency)
                        {
                            StateMachine.SetStateFromTask(ControlState.Emergency);
                        }
                    }
                }
                #endregion

                //----------------------------------------------------------------
                // Endurance / Statistics
                //----------------------------------------------------------------
                #region Endurance

                #region IO
                for (int i = 0; i < nInputs; i++)
                {
                    if (LowLevel.IO.DigitalInputs[i] != precDigitalInputs[i])
                    {
                        HighLevel.MachineEndurance.DigitalInputsToggles[i]++;
                        precDigitalInputs[i] = LowLevel.IO.DigitalInputs[i];
                    }
                }

                for (int i = 0; i < nOutputs; i++)
                {
                    if (LowLevel.IO.DigitalOutputs[i] != precDigitalOutputs[i])
                    {
                        HighLevel.MachineEndurance.DigitalOutputsToggles[i]++;
                        precDigitalOutputs[i] = LowLevel.IO.DigitalOutputs[i];
                    }
                }
                #endregion

                #region Hours
                //Logica: utilizzo un StopWatch per assicurarmi la correttezza delle tempistiche
#if TEST
                if (true)
#else
                //GPIx93
                if (HighLevel.Status.CradleInSync && Math.Abs(LowLevel.Axes.Cradle.Velocity) > 1.0f)
                {
                    sw.Start();
                }
                else
                {
                    sw.Stop();
                }
                if (HighLevel.Status.CradleInSync && Math.Abs(LowLevel.Axes.Cradle.Velocity) > 1.0f)
#endif
                {
                    statisticsCounter++;

                    if (statisticsCounter > 1_000)
                    {
                        statisticsCounter = 0;
                        var elapsedTime = sw.Elapsed;
                        sw.Restart();

                        HighLevel.MachineEndurance.WorkingHours.WorkingWithCradleInSyncHours += elapsedTime.TotalHours;
                        HighLevel.MachineEndurance.WorkingHours.MachineMaintenanceHours += elapsedTime.TotalHours;
                    }
                }
                //GPFx93
                #endregion

                #endregion

                //----------------------------------------------------------------
                // Precedent Values
                //----------------------------------------------------------------
                precCradleInSync = HighLevel.Status.CradleInSync;
                precSharpeningEnabled = HighLevel.Status.SharpeningEnabled;

                //----------------------------------------------------------------
                // Time Series
                //----------------------------------------------------------------
                DataCollectionsUpdater();

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            ProConsole.WriteLine("[EXITING] TaskHighLevelControlStatusUpdater", ConsoleColor.Red);
        }
    }
}
