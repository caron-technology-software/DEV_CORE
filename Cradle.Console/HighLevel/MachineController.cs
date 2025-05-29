using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using System.Diagnostics;

using ProRob;
using ProRob.Log;
using ProRob.Extensions.Collections;

using Machine;
using Machine.Utility;
using Machine.UI.Controls;

using Caron.Cradle.Control.LowLevel.Communication;
using Caron.Cradle.Control.DataCollections;
using Caron.Cradle.Control.HighLevel.Devices;
using Caron.Cradle.Control.HighLevel.StateMachine;
using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.Database;
using Machine.Common;
using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.Control.HighLevel
{
    [Synchronization()]
    public partial class MachineController : IDisposable
    {
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;

        private volatile int threadsStarted = 0;
        private int ThreadsStarted { get => threadsStarted; set => threadsStarted = value; }

        public readonly DateTime StartUpTime = DateTime.Now;

        private readonly DateTime startUpTtcDateTime = DateTime.UtcNow;
        public TimeSpan Uptime => DateTime.UtcNow - startUpTtcDateTime;

        private LowLevelControlStatusGrabber LowLevelControlStatusGrabber { get; set; }
        public float AvaragePacketsForSecond => LowLevelControlStatusGrabber.AvaragePacketsForSecond;
        public int PacketsReceived => LowLevelControlStatusGrabber.PacketsReceived;
        public int CommunicationErrors => LowLevelControlStatusGrabber.CommunicationErrors;

        //GPIx164
        public TimeSpan TimeSpanFromLastDataPacketReceived { get => LowLevelControlStatusGrabber.TimeSpanFromLastDataPacketReceived; }
        //GPFx164

        public ApplicationManager Application { get; set; } = new ApplicationManager();

        public LowLevel.ControlStatus LowLevel { get; set; }
        public HighLevel.ControlStatus HighLevel { get; private set; }
        public MachineDevices Devices { get; private set; }
        public Communicator Communicator { get; set; }
        public TimeSeries TimeSeries { get; private set; }
        public StateMachineManager StateMachine { get; set; }
        public DbLog Log { get; set; }
        public MachineLanguage MachineLanguage { get; private set; }

        //public MachineController(ProLogLiteDB lowLevelLogger, ProLogLiteDB highLevelLogger, ProLogLiteDB uiLogger)
        public MachineController(ProLogLiteTXT lowLevelLogger, ProLogLiteTXT highLevelLogger, ProLogLiteTXT uiLogger)
        {
            //---------------------------------
            // INITIALIZATION
            //---------------------------------
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            LowLevel = new LowLevel.ControlStatus();
            HighLevel = new HighLevel.ControlStatus();

            //---------------------------------
            // LOAD SETTINGS
            //---------------------------------
            #region Load Settings
            Console.WriteLine($"Loading root settings..");
            HighLevel.Settings.HighLevel = MachineData.Read<Settings.HighLevelSettings>(Constants.Path.Settings.SettingsFile);

            Console.WriteLine($"Loading low level settings..");
            HighLevel.Settings.LowLevelMotion = MachineData.Read<LowLevelMotionSettings>(Constants.Path.Settings.LowLevelMotionSettingsFile);

            Console.WriteLine($"Loading UI settings..");
            HighLevel.Settings.UI = MachineData.Read<UISettings>(Constants.Path.Settings.UISettingsFile);

            Console.WriteLine($"Loading workings settings..");
            HighLevel.WorkingsSettings = MachineData.Read<WorkingsSettings>(Constants.Path.Settings.WorkingsSettingsFile);

            Console.WriteLine($"Loading workings statistics..");
            HighLevel.Working = MachineData.Read<Working>(Constants.Path.Settings.WorkingsStatisticsFile);

            Console.WriteLine($"Loading machine configuration..");
            HighLevel.Configuration = MachineData.Read<MachineConfiguration>(Constants.Path.Settings.MachineConfigurationFile);

            Console.WriteLine($"Loading machine endurance..");
            HighLevel.MachineEndurance = MachineData.Read<MachineEndurance>(Constants.Path.Settings.MachineEndurance);

            Console.WriteLine($"Loading working context..");
            HighLevel.WorkingContext = MachineData.Read<WorkingContext>(Constants.Path.Settings.WorkingContextFile);
            #endregion

            //-----------------------------------------------------------
            // Gestione trigger settings
            //-----------------------------------------------------------
            #region Gestione trigger settings
            if (HighLevel.MachineEndurance.WorkingHours.MachineMaintenanceHours < TimeSpan.FromMinutes(5).TotalHours)
            {
                HighLevel.MachineEndurance.Cutter.NumberOfCutOff = 0;
            }
            #endregion

            //-----------------------------------------------------------
            // Localization Settings
            //-----------------------------------------------------------
            #region Localization
            Machine.Localization.Initialize(Constants.Path.Data.LocalizationsFile);
            Machine.Localization.MachineLanguage = MachineData.Read<LocalizationSettings>(Constants.Path.Settings.Localization).MachineLanguage;
            MachineLanguage = Machine.Localization.MachineLanguage;
            Console.WriteLine($"MachineLanguage: {Machine.Localization.MachineLanguage.ToString()}");
            #endregion

            //---------------------------------
            // LOG
            //---------------------------------
            #region Log
            Log = new DbLog(lowLevelLogger, highLevelLogger, uiLogger);

            //Log.LowLevel.RemoveOldLogs(TimeSpan.FromDays(Machine.Constants.Maintenance.NumberOfDaysToKeepLogs));
            //Log.HighLevel.RemoveOldLogs(TimeSpan.FromDays(Machine.Constants.Maintenance.NumberOfDaysToKeepLogs));
            //Log.UI.RemoveOldLogs(TimeSpan.FromDays(Machine.Constants.Maintenance.NumberOfDaysToKeepLogs));
            #endregion

            //---------------------------------
            // Maintenance
            //---------------------------------
            #region Maintenance
            Maintenance.RemoveOldFiles(Constants.Path.LogsFolder, Machine.Constants.Maintenance.NumberOfDaysToKeepLogs);
            #endregion

            //---------------------------------
            // DB UPDATES
            //---------------------------------
            #region DB Updates
            Console.WriteLine($"Updating data..");

            HighLevel.MachineEndurance.Statistics.NumberPowerOn++;
            DatabaseSettings.Update(HighLevel.MachineEndurance);

            var totalPowerOnTime = TimeSpan.FromHours(HighLevel.MachineEndurance.WorkingHours.PowerOnHours);
            ProConsole.WriteLine($"Total power on time: {totalPowerOnTime.TotalDays.ToString("0.00")} hours");
            #endregion

            //---------------------------------
            // INITIALIZATION (after settings loaded)
            //---------------------------------
            TimeSeries = new TimeSeries();
            Communicator = new Communicator(Machine.Constants.Networking.IPAddressLowLevelControl, Machine.Constants.Networking.LowLevelControlTcpPort, LowLevel);
            LowLevelControlStatusGrabber = new LowLevelControlStatusGrabber(LowLevel, TimeSeries);
            Devices = new MachineDevices(LowLevel, HighLevel, Communicator);

            //---------------------------------
            // START
            //---------------------------------
            Application.Start();

            //---------------------------------
            // STATE MACHINE MANAGER
            //---------------------------------
            StateMachine = new StateMachineManager(this);

            //---------------------------------
            // TASKS
            //---------------------------------
            #region Tasks
            {
                int threadsCounter = 0;

                threadsCounter++;
                Task.Run(() =>
                {
                    TaskHighLevelControlStatusUpdater(cancellationToken);
                });

                threadsCounter++;
                Task.Run(() =>
                {
                    TaskCountingStatisticsHandler(cancellationToken);
                });

                threadsCounter++;
                Task.Run(() =>
                {
                    TaskMarchHandler(cancellationToken);
                });

#if !TEST
                threadsCounter++;
                Task.Run(() =>
                {
                    TaskAlignementHandler(cancellationToken);
                });

                threadsCounter++;
                Task.Run(() =>
                {
                    TaskEmergencyHandler(cancellationToken);
                });

                threadsCounter++;
                Task.Run(() =>
                {
                    TaskZundHandler(cancellationToken);
                });

                threadsCounter++;
                Task.Run(() =>
                {
                    TaskZundCutterLockHandler(cancellationToken);
                });
#endif
                //--------------------------
                // Wait start
                //--------------------------
                while (ThreadsStarted != threadsCounter)
                {
                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                }
            }

            ProConsole.WriteLine("[Tasks initialization completed]", ConsoleColor.Yellow);
            #endregion

            //---------------------------------
            // Errors
            //---------------------------------
            #region Errors
            //GPIx243
            if (EnduranceLimit.Check(HighLevel.Settings.HighLevel.EnduranceLimits.WorkingHours.WorkingFakeHours, HighLevel.MachineEndurance.WorkingHours.WorkingFakeHours))
            {
                Console.WriteLine("[ETHERCAT ERROR]");
                HighLevel.Errors.EtherCat = true;

                HighLevel.MachineEndurance.Statistics.EthercatCode = (uint)MachineController.EthercutCodeError; // 135;
            }
            //GPFx243
            #endregion

            //---------------------------------
            // Send settings to low level
            //---------------------------------
            Communicator.SetMachineLowLevelSettings(
                HighLevel.Settings.LowLevelMotion,
                HighLevel.Settings.HighLevel.FunctionsEnabled,
                HighLevel.Settings.HighLevel.MachineParameters);

            Communicator.SetStraightRoller(HighLevel.WorkingContext.Parameters.StraightRoller);

            //GPIx164
            //---------------------------------
            // Heartbeat
            //---------------------------------
            Task.Run(() =>
            {
                var edgeHeartbeat = new EdgeVariable();

                edgeHeartbeat.Update(false);  //aggiunto se no l'heartbeat non funziona, lo stesso per lo SPREADER (non manda il comando Command.HeartbeatEnable)
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        edgeHeartbeat.Update(HighLevel.HeartbeatEnabled);

                        if (edgeHeartbeat.RisingEdge)
                        {
                            ProConsole.WriteTitle("[HEARTBEAT ENABLED]", ConsoleColor.Red);
                            Communicator.SendCommand(Command.HeartbeatEnable);
                        }
                        else if (edgeHeartbeat.FallingEdge)
                        {
                            ProConsole.WriteTitle("[HEARTBEAT DISABLED]", ConsoleColor.Red);
                            Communicator.SendCommand(Command.HeartbeatDisable);
                        }

                        if (edgeHeartbeat.Value && HighLevel.Errors.EmergencyStatus == false)
                        {
                            Communicator.SendCommand(Command.HearbeatPing);
                        }
                    }
                    catch
                    {
                        // --
                    }

                    Thread.Sleep(Machine.Constants.Intervals.Heartbeat);
                }
            });
            //GPFx164

            //---------------------------------
            // Control Ready
            //---------------------------------
            HighLevel.Signals.ControlReady = true;

            //Reset errori (generati durante la fase iniziale di attesa controllore)
            LowLevelControlStatusGrabber.ResetCommunicationErrors();
        }

        public void Close()
        {
            LowLevelControlStatusGrabber.Stop();

            cancellationTokenSource.Cancel();
            Thread.Sleep(1000);

            Console.WriteLine($"[Cradle] Close()");
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                }

                if (LowLevelControlStatusGrabber != null)
                {
                    LowLevelControlStatusGrabber.Dispose();
                    LowLevelControlStatusGrabber = null;
                }

                if (StateMachine != null)
                {
                    StateMachine.Dispose();
                    StateMachine = null;
                }

                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                }
            }
        }
        #endregion
    }
}
