using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;
using ProRob.OperatingSystems.Signals;

using Machine;

namespace Cradle.Proxy
{
    class Program
    {
        static bool IsRunning { get; set; } = true;
        static void Main(string[] args)
        {
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
            // Application Info
            //-----------------------------------------------------------
            #region ApplicationInfo
            ApplicationInfo.SetApplicationName("CARON Cradle Proxy");
            ApplicationInfo.SetApplicationVersion(Caron.Cradle.Constants.ApplicationVersion);
            ProConsole.WriteLine(ApplicationInfo.Instance, ConsoleColor.Red);
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
            // Initialization
            //-----------------------------------------------------------
            #region PriorityClass
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.BelowNormal;
            #endregion

            //-----------------------------------------------------------
            // WebApi
            //-----------------------------------------------------------
            #region Web Api
#if !TEST
            //string serverUri = $"http://{Machine.Constants.Networking.IPAddressHighLevelControl}:{Machine.Constants.Networking.WebApiProxyPort}/";
            string serverUri = $"http://*:{Machine.Constants.Networking.WebApiProxyPort}/";
#else
            string serverUri = $"http://*:{Machine.Constants.Networking.WebApiProxyPort}/";
#endif
            var cts = new CancellationTokenSource();
            var apiTask = Task.Run(() => Caron.Cradle.Control.Api.RunAsync(serverUri, cts.Token));

            Console.WriteLine($"Server started at {serverUri}");
            #endregion

            //-----------------------------------------------------------
            // Exit Signal
            //-----------------------------------------------------------
            #region Exit Signal
            ExitSignal.Instance.Exit += (e, a) =>
            {
                ProConsole.WriteLine("Exit signal..", ConsoleColor.Magenta);

                HandleExit();
            };
            #endregion

            //-----------------------------------------------------------
            // Wait
            //-----------------------------------------------------------
            #region Wait
            while (IsRunning)
            {
                Thread.Sleep(100);
            };
            #endregion

            HandleExit();

            //-----------------------------------------------------------
            // INTERNAL FUNCTIONS
            //-----------------------------------------------------------
            void HandleExit()
            {
                Console.WriteLine("Exiting...");

                cts.Cancel();
                apiTask.Wait();

                Console.Beep();
                Thread.Sleep(100);
                Console.WriteLine("[END]");
            }
        }
    }
}
