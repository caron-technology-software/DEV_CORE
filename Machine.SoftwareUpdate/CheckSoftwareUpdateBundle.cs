using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using ProRob;

namespace Machine.SoftwareUpdate
{
    public class CheckSoftwareUpdateBundle
    {
        public string PathMachineRootFolder { get; private set; } = String.Empty;
        public string PathMachineUtilityFolder { get; private set; } = String.Empty;

        public CheckSoftwareUpdateBundle(
            string pathMachineRootFolder,
            string binUtilityFolder)
        {
            PathMachineRootFolder = pathMachineRootFolder;
            PathMachineUtilityFolder = Path.Combine(PathMachineRootFolder, binUtilityFolder);
        }

        public bool CheckIntegrity(string pathArchive, string password)
        {
            pathArchive = Path.Combine(pathArchive);

            if (File.Exists(pathArchive) == false)
            {
                return false;
            }

            string path7Zip = Path.Combine(PathMachineUtilityFolder, @"7za\7za.exe");
            string arguments = String.Format($"t -p{password} {pathArchive}");

            var output = ProcessHelper.ExecuteShellCommand($"{path7Zip} {arguments}");

            int exitCode = output.Item2;

            return exitCode == 0;
        }
    }
}
