#undef ENABLE_RESTART

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

using Machine.SoftwareUpdate;
using ProRob;

namespace Cradle.Bootstrapper
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //-----------------------------------------------------------
            // Console
            //-----------------------------------------------------------
            ProConsole.MinimizeConsoleWindow();

            //-----------------------------------------------------------
            // ApplicationInfo
            //-----------------------------------------------------------
            #region ApplicationInfo
            ApplicationInfo.SetApplicationName("CARON Cradle Bootstrapper");
            #endregion

            //------------------------------------------------------------------------
            // Stopping Web Server
            //------------------------------------------------------------------------      
            #region Stopping Web Server
            Console.WriteLine("Stopping Web Server..");
            foreach (Process proc in Process.GetProcessesByName("httpd"))
            {
                proc.Kill();
            }
            #endregion

            //------------------------------------------------------------------------
            // Restore Update Bundle
            //------------------------------------------------------------------------      
            #region Update
            string pathRootFolder = @"c:\caron\";
            string machineFolderName = "machine_cradle";
            string backupFolderName = "backup";
            string updateFolderName = "update";
            string updateBundleFile = @"c:\caron\machine_cradle\update\software_update.prsw";
            string password = "NUQ2ODIzMTZFOUNFQjJEQjgyRUIwQUQ0NjA2MjZBNzdDNzRFOUU3QURDQ0Y3Rjc4OEFGNzE3NzU5NkMyMEYwNEFFMzM1NEMxRjk0QTk0NTZBRkE2NkQ5MDU2QzU2NTRDREVEOUVBOEVBNjFCQUI0MTA3NjgyRDYyMjkwQzRERDc=";

            string pathMachineFolder = Path.Combine(pathRootFolder, machineFolderName);
            string pathBackupFolder = Path.Combine(pathMachineFolder, backupFolderName);
            string pathUpdateFolder = Path.Combine(pathMachineFolder, updateFolderName);
            string binUtilityFolderName = Path.Combine(pathMachineFolder, "bin_utility");

            if (File.Exists(updateBundleFile))
            {
                //GPIx199
                string logsFolderName = "logs";
                string pathLogsFolder = Path.Combine(pathMachineFolder, logsFolderName);
                DirectoryInfo di = new DirectoryInfo(pathLogsFolder);
                try
                {
                    FileInfo[] files = di.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                    }
                    Console.WriteLine("Files deleted successfully");
                }
                catch
                {

                }
                //GPFx199
                ProConsole.ShowConsoleWindow();
                ProConsole.MaximizeConsoleWindow();

                var updater = new RestoreUpdateBundle(
                    pathMachineFolder,
                    updateBundleFile,
                    binUtilityFolderName,
                    pathBackupFolder,
                    pathUpdateFolder,
                    password);

                bool ret = updater.UpdateMachine();

                if (!ret)
                {
                    MessageBox.Show("UPDATE FAILED");
                }
            }
            #endregion

            //------------------------------------------------------------------------
            // Start Launcher
            //------------------------------------------------------------------------    
            Process.Start(@"C:\CARON\machine_cradle\bin\launcher\Cradle.Launcher.exe");
        }
    }
}
