#define FORM_WAIT_CONTROL
#undef KILL_WINDOWS_EXPLORER

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using ProRob;
using ProRob.OperatingSystems.Signals;

using Machine;
using Machine.UI.Controls.Forms;
using Machine.Utility;
using Microsoft.Win32;

namespace Cradle.Launcher
{
    //
    // "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\VC\Tools\MSVC\14.26.28801\bin\Hostx64\x64\editbin.exe"  /stack:2097152 Cradle.Launcher.exe
    //

    class Program
    {
        private static readonly TimeSpan WaitInterval = TimeSpan.FromMilliseconds(200);
        private static readonly TimeSpan ClosingApplicationWaitInterval = TimeSpan.FromMilliseconds(30000);

        [STAThread]
        static void Main(string[] args)
        {
            //GPIx175
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
            //GPFx175

            //-----------------------------------------------------------
            // Console
            //-----------------------------------------------------------
            #region Console
#if !DEBUG
            ProConsole.MinimizeConsoleWindow();
            //ProConsole.HideConsoleWindow();
#endif
            #endregion

            //-----------------------------------------------------------
            // Launcher Settings
            //-----------------------------------------------------------
            var launcherSettings = new LauncherSettings()
            {
                PathControl = @"C:\CARON\machine_cradle\bin\control\CradleControl.exe",
                PathUI = @"C:\CARON\machine_cradle\bin\ui\CradleUI.exe",
                PathProxy = @"C:\CARON\machine_cradle\bin\proxy\Cradle.Proxy.exe",
                PathLogo = @"C:\CARON\machine_cradle\assets\launcher_logo.png",
            };

            //-----------------------------------------------------------
            // Init
            //-----------------------------------------------------------
            #region Init
#if KILL_WINDOWS_EXPLORER
            ProcessHelper.ExecuteCmd("taskkill /f /im explorer.exe");
#endif
            #endregion

            //-----------------------------------------------------------
            // Application Info
            //-----------------------------------------------------------
            ApplicationInfo.SetApplicationName("Cradle Launcher");
            ApplicationInfo.SetApplicationVersion(Caron.Cradle.Constants.ApplicationVersion);
            ProConsole.WriteLine(ApplicationInfo.Instance, ConsoleColor.Red);

            //-----------------------------------------------------------
            // Check if process is already running
            //-----------------------------------------------------------
            #region Check if process is already running
            if (Machine.ApplicationManager.CheckIfProcessIsAlreadyRunning(ApplicationInfo.ProcessName, false) == ProcessStatus.Running)
            {
                ProcessHelper.CloseProcess(ApplicationInfo.ProcessName);
                Thread.Sleep(ClosingApplicationWaitInterval);
            }
            #endregion

            //-----------------------------------------------------------
            // Waiting High Level Control (FORM)
            //-----------------------------------------------------------
            #region Waiting Control
            Console.WriteLine("[LAUNCHER] Waiting High Level Control..");

            Func<bool> checkerDelegate = () => Machine.Control.HighLevel.SimpleCommunicator.CheckUI();

#if FORM_WAIT_CONTROL
            Task.Run(() =>
            {
                var form = new FormWait("Waiting Control", Caron.Cradle.Constants.ApplicationVersion.ToString(), checkerDelegate, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500), TimeSpan.FromMinutes(3));

                form.MessageBoxMessage = "Warning";
                form.MessageBoxMessage = "Are you sure to shutdown the machine ?";
                form.TopMost = true;
                form.TopLevel = true;

                form.Picture = Bitmap.FromFile(launcherSettings.PathLogo);
                var dialogResult = form.ShowDialog();

                Console.WriteLine($"FormWait: {dialogResult}");

                if (dialogResult == DialogResult.Abort)
                {
                    Process.GetCurrentProcess().CloseMainWindow();
                }
            });
#else
            Task.Run(() =>
            {
                while (checkerDelegate() == false)
                {
                    Console.WriteLine("Waiting control (Signal UI)..");
                    Thread.Sleep(500);
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
                ProConsole.WriteLine("Exit signal..", ConsoleColor.Magenta);

                HandlingClosing();
            };
            #endregion

            //-----------------------------------------------------------
            // Initialization
            //-----------------------------------------------------------
            #region PriorityClass
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.BelowNormal;
            #endregion

            //-----------------------------------------------------------
            // Close / Kill processes
            //-----------------------------------------------------------
            CloseKillProcesses();

            //-----------------------------------------------------------
            // NGEN
            //-----------------------------------------------------------
            #region NGEN
            ProConsole.WriteLine("[LAUNCHER] Generating native images ..", ConsoleColor.Red);

            Parallel.Invoke(
                () =>
                {
                    ProcessHelper.Execute(@$"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen", $"install {launcherSettings.PathControl}");

                },

                () =>
                {
                    ProcessHelper.Execute(@$"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen", $"install {launcherSettings.PathUI}");
                },

                () =>
                {
                    ProcessHelper.Execute(@$"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen", $"install {launcherSettings.PathProxy}");
                }
            );

            Thread.Sleep(500);
            #endregion

            //-----------------------------------------------------------
            // Launch processes
            //-----------------------------------------------------------
            #region Launch processes
            Console.WriteLine("[LAUNCHER] Starting CONTROL..");
            {
                var psi = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = launcherSettings.PathControl,
                };

                var p = Process.Start(psi);
            }


            // Wait Control
            Thread.Sleep(1000);
            while (!Machine.Control.HighLevel.SimpleCommunicator.CheckControlReady())
            {
                Console.WriteLine("Waiting control (Signal ControlReady)..");
                Thread.Sleep(500);
            }

            Thread.Sleep(1000);
            Console.WriteLine("[LAUNCHER] Starting UI..");
            {
                var psi = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = launcherSettings.PathUI,
                };

                var p = Process.Start(psi);
            }

            Thread.Sleep(1000);
            Console.WriteLine("[LAUNCHER] Starting Proxy..");
            {
                var psi = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = launcherSettings.PathProxy,
                };

                var p = Process.Start(psi);
            }
            #endregion

            //-----------------------------------------------------------
            // Waiting processes (START)
            //-----------------------------------------------------------
            #region Waiting processes (START)
            Console.WriteLine("[LAUNCHER] Waiting all processes..");
            while (!ProcessHelper.IsProcessRunning(Path.GetFileName(launcherSettings.PathControl)) ||
                   !ProcessHelper.IsProcessRunning(Path.GetFileName(launcherSettings.PathUI)) ||
                   !ProcessHelper.IsProcessRunning(Path.GetFileName(launcherSettings.PathProxy)))
            {
                Thread.Sleep(WaitInterval);
            }
            Console.WriteLine("[LAUNCHER] All processes are running..");
            #endregion

            //-----------------------------------------------------------
            // Waiting processes (RUNNING)
            //-----------------------------------------------------------
            WaitProcesses();

            //-----------------------------------------------------------
            // Handling Closing
            //-----------------------------------------------------------
            HandlingClosing();

            //-----------------------------------------------------------
            // Internal Functions
            //-----------------------------------------------------------
            void HandlingClosing()
            {
                CloseKillProcesses();

                Console.Beep();
                Thread.Sleep(100);
                Console.WriteLine("[END]");
            }

            void WaitProcesses()
            {
                #region Waiting processes (RUNNING)
                Console.WriteLine("[LAUNCHER] Waiting all processes..");
                while (ProcessHelper.IsProcessRunning(Path.GetFileName(launcherSettings.PathControl)) &&
                       ProcessHelper.IsProcessRunning(Path.GetFileName(launcherSettings.PathUI)) &&
                       ProcessHelper.IsProcessRunning(Path.GetFileName(launcherSettings.PathProxy)))
                {
                    Thread.Sleep(WaitInterval);
                }

                //Attendo periodo di tempo con un unico processo attivo (permetto lo spegnimento sincronizzato)
                Console.WriteLine("[LAUNCHER] Waiting closing..");
                bool exitCond = false;
                var sw = new Stopwatch();
                sw.Start();
                while (exitCond == false)
                {
                    Thread.Sleep(WaitInterval);

                    bool c1 = !ProcessHelper.IsProcessRunning(Path.GetFileName(launcherSettings.PathControl)) &&
                              !ProcessHelper.IsProcessRunning(Path.GetFileName(launcherSettings.PathUI));

                    bool c2 = sw.Elapsed > ClosingApplicationWaitInterval;

                    if (c1 || c2)
                    {
                        sw.Stop();
                        exitCond = true;
                    }
                }
                Console.WriteLine($"[LAUNCHER] Exiting ({(int)sw.ElapsedMilliseconds} ms)..");
                #endregion
            }

            void CloseKillProcesses()
            {
                #region Close / Kill processes
                var t1 = Task.Run(() =>
                {
                    try
                    {
                        if (ProcessHelper.IsProcessRunning(Path.GetFileName(launcherSettings.PathProxy)))
                        {
                            Console.WriteLine("[LAUNCHER] Killing Proxy..");
                            ProcessHelper.CloseKillProcess(Path.GetFileName(launcherSettings.PathProxy), TimeSpan.FromSeconds(5));
                        }
                    }
                    catch
                    {
                        //--
                    }
                });
                Thread.Sleep(100);

                var t2 = Task.Run(() =>
                {
                    try
                    {
                        if (ProcessHelper.IsProcessRunning(Path.GetFileName(launcherSettings.PathUI)))
                        {
                            Console.WriteLine("[LAUNCHER] Killing UI..");
                            ProcessHelper.CloseKillProcess(Path.GetFileName(launcherSettings.PathUI), TimeSpan.FromSeconds(5));
                        }
                    }
                    catch
                    {
                        //--
                    }
                });
                Thread.Sleep(100);

                var t3 = Task.Run(() =>
                {
                    try
                    {
                        if (ProcessHelper.IsProcessRunning(Path.GetFileName(launcherSettings.PathControl)))
                        {
                            Console.WriteLine("[LAUNCHER] Killing CONTROL..");
                            ProcessHelper.CloseKillProcess(Path.GetFileName(launcherSettings.PathControl), TimeSpan.FromSeconds(5));
                        }
                    }
                    catch
                    {
                        //--
                    }
                });

                Task.WaitAll(t1, t2, t3);

                #endregion
            }
        }
    }
}
