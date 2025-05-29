using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProRob;
using ProRob.Extensions.Hashing;
using ProRob.Extensions.String;

using Machine.SoftwareUpdate.Arguments;

namespace Machine.SoftwareUpdate.Updater
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Machine.SoftwareUpdate.Updater");

            UpdateArgs arguments = new UpdateArgs();

            try
            {
                arguments = ProRob.Json.Deserialize<UpdateArgs>(args[0]);
            }
            catch
            {
                Console.WriteLine("FAILED", ConsoleColor.Red);
#if TEST
                ProConsole.PressKeyToContinue();
#endif
                return -1;
            }

            string pathMachineFolder = Path.Combine(arguments.PathRootFolder, arguments.MachineFolderName);
            string pathBackupFolder = Path.Combine(pathMachineFolder, arguments.BackupFolderName);
            string pathUpdateFolder = Path.Combine(pathMachineFolder, arguments.UpdateFolderName);

            //------------------------------------------------------------------------
            // Restore Update Bundle
            //------------------------------------------------------------------------
            Console.WriteLine("[RestoreUpdateBundle]");

            var updater = new RestoreUpdateBundle(
                pathUpdateFolder,
                arguments.UpdateBundleFile,
                arguments.BinUtilityFolderName,
                pathBackupFolder,
                pathUpdateFolder,
                arguments.Password);

            bool ret = updater.UpdateMachine();

            if (ret)
            {
                Console.WriteLine("COMPLETED", ConsoleColor.Green);
#if TEST
                ProConsole.PressKeyToContinue();
#endif
                return 0;
            }
            else
            {
                Console.WriteLine("FAILED", ConsoleColor.Red);
#if TEST
                ProConsole.PressKeyToContinue();
#endif
                return -1;
            }
        }
    }
}
