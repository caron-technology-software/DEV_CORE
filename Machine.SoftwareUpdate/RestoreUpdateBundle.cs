#undef EXECUTE_BACKUP

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using ProRob;
using System.Threading;

namespace Machine.SoftwareUpdate
{
    public class RestoreUpdateBundle
    {
        public string PathMachineRootFolder { get; private set; }
        public string PathSoftwareUpdateBundle { get; private set; }
        private string Password { get; set; }
        private string BinUtilityFolder { get; set; }

        public string PathMachineUtilityFolder { get; private set; }
        public string PathMachineBackupFolder { get; private set; }
        public string PathMachineUpdateFolder { get; private set; }

        public RestoreUpdateBundle(
            string pathMachineRootFolder,
            string pathSoftwareUpdateBundle,
            string binUtilityFolder,
            string backupFolder,
            string updateFolder,
            string password)
        {
            PathMachineRootFolder = Path.Combine(pathMachineRootFolder);
            PathSoftwareUpdateBundle = Path.Combine(pathSoftwareUpdateBundle);
            Password = password;

            BinUtilityFolder = binUtilityFolder;
            PathMachineUtilityFolder = Path.Combine(PathMachineRootFolder, BinUtilityFolder);
            PathMachineBackupFolder = Path.Combine(PathMachineRootFolder, backupFolder);
            PathMachineUpdateFolder = Path.Combine(PathMachineRootFolder, updateFolder);
        }


        public bool UpdateMachine()
        {
            if (!File.Exists(PathSoftwareUpdateBundle))
            {
                return false;
            }

            ProRob.TicToc.Tic();
            ProConsole.WriteLine("[CheckIntegrity]", ConsoleColor.Yellow);
            var checkerUpdate = new SoftwareUpdate.CheckSoftwareUpdateBundle(PathMachineRootFolder, BinUtilityFolder);
            bool ret = checkerUpdate.CheckIntegrity(PathSoftwareUpdateBundle, Password);
            ProRob.TicToc.Toc();

            if (ret)   
            {
                ProConsole.WriteLine("SUCCESS", ConsoleColor.Green);
            }
            else
            {
                ProConsole.WriteLine("FAILED", ConsoleColor.Red);
                return false;
            }

            //Unarchive final stage
            ProConsole.WriteLine($"[*] UNARCHIVING STAGE 1/2]", ConsoleColor.Yellow);
            Compression.UnarchiveEncryptedArchive(PathMachineRootFolder, PathMachineUtilityFolder, PathSoftwareUpdateBundle, PathMachineUpdateFolder, Password, SoftwareUpdateInternal.ProgressConsoleOutputHandler);

            //Unarchive first stage
            ProConsole.WriteLine($"[*] UNARCHIVING STAGE 2/2]", ConsoleColor.Yellow);
            var manifests = SoftwareUpdateInternal.GetManifestFromFolder(PathMachineUpdateFolder);

            foreach (var manifest in manifests)
            {
                ProConsole.WriteLine($"[*] PROCESSING MANIFEST: {manifest.Folder}", ConsoleColor.Yellow);

                var pathArchive = Path.Combine(PathMachineUpdateFolder, $"machine_{manifest.Folder}_data.encrypted_archive");
                string pathTmpUpdateFolder = Path.Combine(PathMachineUpdateFolder, manifest.Folder);

                Compression.UnarchiveEncryptedArchive(PathMachineRootFolder, PathMachineUtilityFolder, pathArchive, pathTmpUpdateFolder, Password, SoftwareUpdateInternal.ProgressConsoleOutputHandler);

                //Checks
                ProConsole.WriteLine($"Checking signature ({manifest.Folder})..", ConsoleColor.Cyan);

                string signature = SoftwareUpdateInternal.GetDirectoryHash(pathTmpUpdateFolder);

                //ProConsole.WriteLine($"signature ({manifest}) manifest signature ({manifest.Signature}) ..", ConsoleColor.Cyan);
                //Thread.Sleep(10000);
                //if (false)   
                if (signature != manifest.Signature)
                {
                    string pathFailedManifest = Path.Combine(Path.GetDirectoryName(PathSoftwareUpdateBundle), $"machine_{manifest.Folder}_manifest_FAILED.json");
                    SoftwareUpdateInternal.CreateManifest(pathTmpUpdateFolder, pathFailedManifest);

                    return false;
                }

                string pathFolderToUpdateFolder = Path.Combine(PathMachineRootFolder, manifest.Folder);

#if EXECUTE_BACKUP
                //Backup
                string pathArchiveBackup = Path.Combine(PathMachineBackupFolder, $"{manifest.Folder}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}");
                ProConsole.WriteLine($"Backuping folder {manifest.Folder}..", ConsoleColor.Cyan);       
                Compression.CreateEncryptedArchiveFromFolder(PathMachineRootFolder, PathMachineUtilityFolder, pathToUpdateFolder, pathArchiveBackup, Password, CompressionLevel.Archive, SoftwareUpdateInternal.ProgressConsoleOutputHandler);
#endif

                //Delete
                ProConsole.WriteLine($"Cleaning folder {manifest.Folder}..", ConsoleColor.Cyan);

                var preCleanFiles = FileSystem.GetFiles(pathFolderToUpdateFolder, "*.*", SearchOption.AllDirectories);

                FileSystem.DeleteDirectory(pathFolderToUpdateFolder);
                Directory.CreateDirectory(pathFolderToUpdateFolder);

                var postCleanFiles = FileSystem.GetFiles(pathFolderToUpdateFolder, "*.*", SearchOption.AllDirectories);

                //Check directory
                if (!Directory.Exists(pathFolderToUpdateFolder))
                {
                    ProConsole.WriteLine($"Creating folder {pathFolderToUpdateFolder}..", ConsoleColor.Cyan);
                    Directory.CreateDirectory(pathFolderToUpdateFolder);
                }

                ProConsole.WriteLine($"Copying files from {pathTmpUpdateFolder} to {pathFolderToUpdateFolder}..", ConsoleColor.Cyan);

                //Copy files
                var cmdOut = ProcessHelper.ExecuteShellCommand($"xcopy /e /c /f /h /y {pathTmpUpdateFolder} {pathFolderToUpdateFolder}");
                foreach (var file in cmdOut.Item1)
                {
                    ProConsole.WriteLine(file, ConsoleColor.DarkGray);
                }

                var postCopyFiles = FileSystem.GetFiles(pathFolderToUpdateFolder, "*.*", SearchOption.AllDirectories);

                Console.WriteLine($"File statistics: {preCleanFiles.Count()}->{postCleanFiles.Count()}->{postCopyFiles.Count()}", ConsoleColor.Yellow);
            }

            //Delete
            ProConsole.WriteLine($"Cleaning folder..", ConsoleColor.Cyan);
            FileSystem.DeleteDirectory(PathMachineUpdateFolder);
            Directory.CreateDirectory(PathMachineUpdateFolder);

            return true;
        }
    }
}
