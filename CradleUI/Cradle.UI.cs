#undef PRESS_A_KEY_TO_EXIT
#undef EXCEPTION_CATCHER

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.ExceptionServices;

using ProRob;
using ProRob.Log;
using ProRob.Extensions.Object;
using ProRob.OperatingSystems.Signals;

using Machine;
using Machine.UI.Common;
using Machine.Utility;
using Machine.UI.Communication;
using Microsoft.Win32;

namespace Caron.Cradle.UI
{
    static class CradleUI
    {
        public static MachineInfo MachineModel = new MachineInfo() { Name = Cradle.Constants.MachineFullName };

        private static bool HighLevelControlReady { get; set; } = false;

        private static Supervisor Supervisor { get; set; } = null;

        [STAThread]
        static void Main()
        {
            //-----------------------------------------------------------
            // Regional setting FIX
            //-----------------------------------------------------------
            #region Regional_setting_FIX
            //se true blocco programma e correggo impostazioni internazionali:
            Boolean bInternationalOptions = false;

            //
            //this is how your key will look like
            //the 2nd argument (true) is indicating that the key is writable
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);

            //check sDecimal regional setting key is the decimal separator:
            if (key.GetValue("sDecimal") == null)
            {
                //code if key Not Exist
                Console.WriteLine($"the registry key sDecimal doesn't exist!!!");
            }
            else
            {
                //code if key Exist
                string data = key.GetValue("sDecimal").ToString();

                if (!data.Equals(","))
                {
                    //adding/editing a value 
                    key.SetValue("sDecimal", ",");
                    Console.WriteLine($"the registry key sDecimal is fixed!");
                    bInternationalOptions = true;
                }
                else
                {
                    Console.WriteLine($"sDecimal key is ok!");
                }
            }

            //check sThousand regional setting key is the decimal separator:
            if (key.GetValue("sThousand") == null)
            {
                //code if key Not Exist
                Console.WriteLine($"the registry key sThousand doesn't exist!!!");
            }
            else
            {
                //code if key Exist
                string data = key.GetValue("sThousand").ToString();  //returns the text found in 'someValue'

                if (!data.Equals("."))
                {
                    //adding/editing a value 
                    key.SetValue("sThousand", "."); //sets 'someData' in 'someValue'
                    Console.WriteLine($"the registry key sThousand is fixed!");
                    bInternationalOptions = true;
                }
                else
                {
                    Console.WriteLine($"sThousand key is ok!");
                }
            }

            //and finally, you close the key
            key.Close();

            if (bInternationalOptions)
            {
                Console.WriteLine("Wrong Regional Options fixed, exiting to restart application!");
                Thread.Sleep(10000);
                return;
            }

            #endregion

            //-----------------------------------------------------------
            // Console
            //-----------------------------------------------------------
            #region Console
#if !DEBUG
            ProConsole.MinimizeConsoleWindow();
            //ProConsole.HideConsoleWindow();
#endif
            ProConsole.AttachConsole();
            #endregion

            //-----------------------------------------------------------
            // ApplicationInfo
            //-----------------------------------------------------------
            #region Application Info
            ApplicationInfo.SetApplicationName("CARON Cradle UI");
            ApplicationInfo.SetApplicationVersion(Cradle.Constants.ApplicationVersion);
            #endregion

            //-----------------------------------------------------------
            // Check if process is already running
            //-----------------------------------------------------------
            #region Check if process is already running
            if (Machine.ApplicationManager.CheckIfProcessIsAlreadyRunning(ApplicationInfo.ProcessName) == ProcessStatus.Running)
            {
                return;
            }
            #endregion

            //-----------------------------------------------------------
            // Communicator
            //-----------------------------------------------------------
            #region Communicator
            Machine.UI.Communication.Communicator.Initialize(
                Machine.Constants.Networking.IPAddressHighLevelControl,
                Machine.Constants.Networking.WebApiPort,
                Machine.Constants.Timeouts.HighLevelControlCommunication);

            #endregion

            //-----------------------------------------------------------
            // ProLogger
            //-----------------------------------------------------------
            #region ProLogger
            ConsoleRedirectWriter consoleRedirectWriter = new ConsoleRedirectWriter();
            consoleRedirectWriter.OnWrite += delegate (string value)
            {
                if (HighLevelControlReady)
                {
                    Task.Run(() =>
                    {
                        Communicator.AddUILog(new LogItem(value));
                    });
                }
            };
            #endregion

            try
            {
                //-----------------------------------------------------------
                // Waiting High Level Control
                //-----------------------------------------------------------
                #region Waiting High Level Control
                while (Communicator.TrySendHttpGetRequest("heartbeat") == false)
                {
                    Console.WriteLine("Waiting control..");
                    Thread.Sleep(250);
                }

                while (Communicator.GetData<bool>("signal/control") == false)
                {
                    Console.WriteLine("Waiting control signal ready..");
                    Thread.Sleep(250);
                }
                Thread.Sleep(500);

                ProConsole.WriteLine("[Cradle.UI] HighLevelControlReady", ConsoleColor.Yellow);
                HighLevelControlReady = true;
                #endregion

                //-----------------------------------------------------------
                // Start Application
                //-----------------------------------------------------------
                ProConsole.WriteLine(ApplicationInfo.Instance, ConsoleColor.Red);

                //-----------------------------------------------------------
                // Localization Settings
                //-----------------------------------------------------------
                #region Localization
                Machine.Localization.Initialize(Cradle.Constants.Path.Data.LocalizationsFile);
                Machine.Localization.MachineLanguage = Communicator.GetData<MachineLanguage>("localization/language/pippo");
                Console.WriteLine($"MachineLanguage: {Machine.Localization.MachineLanguage.ToString()}");
                #endregion

                //-----------------------------------------------------------
                // Exceptions Catcher
                //-----------------------------------------------------------
                #region Exceptions Catcher
#if EXCEPTION_CATCHER
                EventHandler<FirstChanceExceptionEventArgs> eventHandler = delegate (object o, FirstChanceExceptionEventArgs e)
                {
                    string exception = ProRob.Exceptions.Handlers.FirstChanceExceptionLoggerHandler(e);
#if DEBUG
                    ProConsole.WriteLine($"[EXCEPTION_CATCHER: {exception}]", ConsoleColor.DarkYellow);
#endif
                    Task.Run(() =>
                    {
                        Communicator.AddUILog(new LogItem(exception, LogType.Exception));
                    });
                };

                AppDomain.CurrentDomain.FirstChanceException += eventHandler;
#endif
                #endregion

                //-----------------------------------------------------------
                // Windows.Forms and events
                //-----------------------------------------------------------
                #region Windows.Forms and events
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                System.Windows.Forms.Application.ApplicationExit += ApplicationExitHandler;
                #endregion

                //-----------------------------------------------------------
                // Initialization
                //-----------------------------------------------------------
                #region PriorityClass
                System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.AboveNormal;
                #endregion

                //-----------------------------------------------------------
                // Supervisor
                //-----------------------------------------------------------
                Supervisor = new Supervisor();

                //-----------------------------------------------------------
                // Supervisor (Forms)
                //-----------------------------------------------------------
                #region Forms initialization
                FormTransparent formTransparent = new FormTransparent();

                FormMain formMain = new FormMain();

                FormMenu formMenu = new FormMenu();
                FormTopBar formTopBar = new FormTopBar();
                FormActions formActions = new FormActions();

                FormDashboard formDashboard = new FormDashboard();
                FormUserSettings formUserSettings = new FormUserSettings();
                FormManualOperations formManualOperations = new FormManualOperations();
                FormRootSettings formRootSettings = new FormRootSettings();
                FormWorkingsSettings formWorkingsSettings = new FormWorkingsSettings();
                FormLoadUnload formLoadUnload = new FormLoadUnload();

                FormWorkingsStatistics formWorkingsStatistics = new FormWorkingsStatistics();

                FormBroswerInterface formBroswerInterface = new FormBroswerInterface();

                FormLicenses formLicenses = new FormLicenses();
                FormMessages formMessages = new FormMessages();

                Supervisor.SetForms(
                    formTransparent,

                    formMain,
                    formMenu,
                    formActions,
                    formTopBar,

                    formDashboard,

                    formUserSettings,
                    formManualOperations,
                    formRootSettings,
                    formWorkingsSettings,
                    formLoadUnload,
                    formWorkingsStatistics,
                    formBroswerInterface,
                    formLicenses,
                    formMessages
                    );

#if DEBUG || TEST
                formMain.TopMost = false;

#else
                formMain.TopLevel = true;
                formTransparent.TopLevel = true;
#endif
                #endregion

                //-----------------------------------------------------------
                // Tasks
                //-----------------------------------------------------------
                #region Task Heartbeat
#if (!TEST && !DEBUG)
                Task.Run(() =>
                {
                    ProConsole.WriteLine("[ENTERING] Task Heartbeat", ConsoleColor.Green);

                    while (Supervisor.IsRunning)
                    {
                        if (Machine.UI.Communication.Communicator.TrySendHttpGetRequest("heartbeat", Machine.UI.Constants.Intervals.Heartbeat) == false)
                        {
                            Supervisor.ShutdownUI();

                            while (Supervisor.DeinitializationCompleted == false)
                            {
                                Thread.Sleep(Machine.UI.Constants.Intervals.WaitGeneric);
                            }

                            Thread.Sleep(Machine.UI.Constants.Intervals.WaitAfterUIDeinitialization);

                            System.Windows.Forms.Application.Restart();
                        }

                        Thread.Sleep(Machine.UI.Constants.Intervals.Heartbeat);
                    }
                    ProConsole.WriteLine("[EXITING] Task Heartbeat", ConsoleColor.Red);
                });
#endif
                #endregion

                #region Task DEBUG
#if DEBUG || TEST
                Task.Run(() =>
                {
                    while (Supervisor.IsRunning)
                    {
#pragma warning disable 162
                        if (false)
                        {
                            bool l1 = Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)Control.LowLevel.DigitalInput.LimitOverturningMotorSideLoad];
                            bool l2 = Supervisor.Control.LowLevel.IO.DigitalInputs[(byte)Control.LowLevel.DigitalInput.LimitOverturningOperatorSideLoad];
                            Console.WriteLine($"l1:{l1} l2:{l2}");
                        }

                        if (false)
                        {
                            Console.WriteLine($"CradleScalingFactor: {Supervisor.Control.HighLevel.WorkingContext.Parameters.CradleScalingFactor}");
                            Console.WriteLine($"CradleInSync:{Supervisor.Control.HighLevel.Status.CradleInSync}");
                        }
#pragma warning restore 162
                        Thread.Sleep(Machine.Constants.Intervals.LongWaitSleep);
                    }
                });
#endif
                #endregion

                //-----------------------------------------------------------
                // Exit Signal
                //-----------------------------------------------------------
                #region Exit Signal
                ExitSignal.Instance.Exit += (e, a) =>
                {
                    Console.WriteLine("[Exit Signal] Received event..");

                    if (Supervisor != null)
                    {
                        Supervisor.ShutdownUI();
                    }

                    HandlingClosingOperations();

                    ProConsole.WriteLine("[Application.Exit] from Exit Signal", ConsoleColor.Red);

                    return;
                };
                #endregion

                //-----------------------------------------------------------
                // UI Start
                //-----------------------------------------------------------
                System.Windows.Forms.Application.Run(formMain);

                //-----------------------------------------------------------
                // Handling close operations
                //-----------------------------------------------------------
                HandlingClosingOperations();

                ProConsole.WriteLine("[Application.Exit]", ConsoleColor.Red);
            }
            catch (System.Exception e)
            {
                #region Exceptions Handling
                var exception = Json.Serialize(ProRob.Exceptions.ExceptionDataLog.Compose(e));
                Console.WriteLine($"[IRREVERSIBLE ERROR]\n{exception}");
                Communicator.AddUILog(new LogItem(exception, LogType.IrreversibleException));
                MessageBox.Show(ApplicationInfo.ApplicationName, "IRREVERSIBLE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                #endregion
            }

            void HandlingClosingOperations()
            {
                Console.WriteLine("[Handling closing operations] Waiting deinitialization..");

                while (Supervisor.DeinitializationCompleted == false)
                {
                    Thread.Sleep(Machine.UI.Constants.Intervals.WaitGeneric);
                }

                //consoleRedirectWriter.Close();
                //consoleRedirectWriter.Dispose();

                Console.Beep();
                Thread.Sleep(100);
                Console.WriteLine("[END]");
            }
        }

        private static void ApplicationExitHandler(object sender, EventArgs e)
        {
            ProConsole.WriteLine("[ApplicationExitHandler]", ConsoleColor.Red);

            #region PRESS_A_KEY_TO_EXIT
#if PRESS_A_KEY_TO_EXIT
            Console.WriteLine("Press a key to exit..");
            Console.ReadKey();
#endif
            #endregion
        }
    }
}