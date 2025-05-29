using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

namespace Machine.SoftwareUpdate
{
    internal class Compression
    {
        public static bool UnarchiveEncryptedArchive(string pathMachineRootFolder, string pathMachineUtilityFolder, string pathArchive, string pathOutputFolder, string password, Action<string> consoleOutput = null)
        {
            string path7Zip = Path.Combine(pathMachineUtilityFolder, @"7za\7za.exe");
            string arguments = String.Format(@"x -bso0 -bse0 -bsp1 -bb0 {0} -y -p{1} -o{2}",
                Path.Combine(pathArchive),
                password,
                Path.Combine(pathOutputFolder));

            ProcessHelper.ExecuteShellCommand($"{path7Zip} {arguments}", consoleOutput is null ? true : false, consoleOutput);

            return true;
        }

        public static bool CreateEncryptedArchiveFromFolder(string pathMachineRootFolder, string pathMachineUtilityFolder, string pathFolderInput, string pathArchive, string password, CompressionLevel compressionLevel = CompressionLevel.Normal, Action<string> consoleOutput = null)
        {
            if (File.Exists(pathArchive))
            {
                File.Delete(pathArchive);
            }

            string path7Zip = Path.Combine(pathMachineUtilityFolder, @"7za\7za.exe");
            string arguments = String.Format(@"a -mx{0} -bso0 -bse0 -bsp1 -bb0 -t7z -mhe -r -y -p{1} {2} {3}\*",
                (int)compressionLevel,
                password,
                Path.Combine(pathArchive),
                Path.Combine(pathFolderInput));

            ProcessHelper.ExecuteShellCommand($"{path7Zip} {arguments}", consoleOutput is null ? true : false, consoleOutput);

            return true;
        }
    }
}

