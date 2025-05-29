#undef CHECK_INTEGRITY

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;
using ProRob.Extensions.Hashing;
using ProRob.Extensions.String;

using Machine.SoftwareUpdate;
using System.Diagnostics;
using Caron.Cradle;

namespace Cradle.SoftwareUpdate.Builder
{
    class Program
    {
        public static readonly string Version = $"{DateTime.Now.Year:0000}_{DateTime.Now.Month:00}_{DateTime.Now.Day:00}__{DateTime.Now.Hour:00}_{DateTime.Now.Minute:00}__{Constants.ApplicationVersion.Build}__{Constants.ApplicationVersion.Major}_{Constants.ApplicationVersion.Minor}_{Constants.ApplicationVersion.Patch}";

        public static readonly string PathRootFolder = @"C:\CARON";
        public static readonly string MachineFolderName = "machine_cradle";
        public static readonly string BinUtilityFolderName = "bin_utility";
        public static readonly string UpdateFolderName = "update";
        public static readonly string BackupFolderName = "backups";
        public static readonly string BuildFolderName = "build_machine_cradle";
        public static readonly string BundleName = $"cradle_software_update_{Version}.prsw";

        public static readonly string Password = $"{"CARON Technology".GetSHA512Hash()} {"YORK".GetSHA512Hash()} {"WREG-8450-kepd-2048".GetSHA512Hash()}".GetSHA512Hash().ToBase64();

        public static readonly string PathMachineFolder = Path.Combine(PathRootFolder, MachineFolderName);
        public static readonly string PathBundleFile = Path.Combine(PathRootFolder, BuildFolderName, BundleName);
        public static readonly string PathUpdateBundleFolder = Path.Combine(PathMachineFolder, UpdateFolderName);
        public static readonly string UpdateBundleFile = Path.Combine(PathUpdateBundleFolder, BundleName);
        public static readonly string PathBackupFolder = Path.Combine(PathMachineFolder, BackupFolderName);
        public static readonly string PathUpdateFolder = Path.Combine(PathMachineFolder, UpdateFolderName);

        static void Main(string[] args)
        {
            bool ret = false;

            ProConsole.WriteTitle("SOFTWARE UPDATE", ConsoleColor.Red);
            //ProConsole.WriteLine($"Password: {Password}");

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
            // Cleaning
            //------------------------------------------------------------------------
            ProRob.TicToc.Tic();
            ProConsole.WriteLine("[Cleaning]", ConsoleColor.Yellow);
            FileSystem.DeleteDirectory(PathUpdateBundleFolder);
            Directory.CreateDirectory(PathUpdateBundleFolder);
            ProRob.TicToc.Toc();

            //------------------------------------------------------------------------
            // Create Update Bundle
            //------------------------------------------------------------------------
            ProRob.TicToc.Tic();
            ProConsole.WriteLine("[CreateUpdateBundle]", ConsoleColor.Yellow);

            var directoriesToIncludeInUpdate = new List<string>()
            {
              //"assets",
              "bin",
              "bin_low_level_control",
              "bin_server",
              "localizations",
              //"www",
              "scripts"
            };

            ProConsole.WriteLine($"Directories included:", ConsoleColor.Magenta);
            directoriesToIncludeInUpdate.ForEach(x => ProConsole.WriteLine($"\t{x}", ConsoleColor.Magenta));

            ProConsole.PressKeyToContinue();

            var builder = new CreateUpdateBundle(
                PathMachineFolder,
                BinUtilityFolderName,
                directoriesToIncludeInUpdate,
                PathBundleFile,
                Password);

            ProConsole.WriteLine("[CreateSoftwareUpdateBundle]", ConsoleColor.Yellow);
            ret = builder.CreateSoftwareUpdateBundle();

            if (ret)
            {
                ProConsole.WriteTitle("COMPLETED", ConsoleColor.Green);
            }
            else
            {
                ProConsole.WriteTitle("FAILED", ConsoleColor.Red);
            }

            ProRob.TicToc.Toc();

            //------------------------------------------------------------------------
            // Cleaning
            //------------------------------------------------------------------------
            ProRob.TicToc.Tic();
            ProConsole.WriteLine("[Cleaning]", ConsoleColor.Yellow);
            File.Delete(UpdateBundleFile);
            File.Copy(PathBundleFile, UpdateBundleFile);
            ProRob.TicToc.Toc();

            //------------------------------------------------------------------------
            // Restore Update Bundle
            //------------------------------------------------------------------------
            ProRob.TicToc.Tic();
            ProConsole.WriteLine("[RestoreUpdateBundle]", ConsoleColor.Yellow);
            var updater = new RestoreUpdateBundle(
                PathMachineFolder,
                UpdateBundleFile,
                BinUtilityFolderName,
                PathBackupFolder,
                PathUpdateFolder,
                Password);

            ret = updater.UpdateMachine();

            if (ret)
            {
                ProConsole.WriteTitle("COMPLETED", ConsoleColor.Green);

            }
            else
            {
                ProConsole.WriteTitle("FAILED", ConsoleColor.Red);
            }

            ProRob.TicToc.Toc();

            ProConsole.PressKeyToContinue();
        }
    }

}
