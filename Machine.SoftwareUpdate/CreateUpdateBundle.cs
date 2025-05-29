using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using ProRob;

namespace Machine.SoftwareUpdate
{
    public class CreateUpdateBundle
    {
        public string PathMachineRootFolder { get; private set; }
        public List<string> InputFoldersName { get; private set; }
        public string PathSoftwareUpdateBundle { get; private set; }
        private string Password { get; set; }
        private string BinUtilityFolder { get; set; }

        public string InputFolderName { get; private set; }
        public string PathMachineUtilityFolder { get; private set; }
        public string PathBuilderFolder { get; private set; }

        public CreateUpdateBundle(
            string pathMachineRootFolder,
            string binUtilityFolder,
            List<string> inputFoldersName,
            string pathSoftwareUpdateBundle,
            string password)
        {
            PathMachineRootFolder = Path.Combine(pathMachineRootFolder);
            InputFoldersName = inputFoldersName;
            PathSoftwareUpdateBundle = Path.Combine(pathSoftwareUpdateBundle);
            Password = password;

            BinUtilityFolder = binUtilityFolder;
            PathMachineUtilityFolder = Path.Combine(PathMachineRootFolder, BinUtilityFolder);
            PathBuilderFolder = Path.GetDirectoryName(PathSoftwareUpdateBundle);
        }

        private readonly static Action<string> ProgressConsoleOutputHandler = (x) =>
        {
            if (string.IsNullOrWhiteSpace(x))
            {
                return;
            }

            if (x.Contains("%"))
            {
                Console.WriteLine($"{x.Substring(0, 3 + 1)}");
            }
        };

        public bool CreateSoftwareUpdateBundle()
        {
            //Cleaning
            ProConsole.WriteLine($"Cleaning folder..");
            FileSystem.DeleteDirectory(PathBuilderFolder);
            Directory.CreateDirectory(PathBuilderFolder);

            ProConsole.WriteLine($"[*] ARCHIVING STAGE 1/2]", ConsoleColor.Yellow);

            //First stage
            for (int i = 0; i < InputFoldersName.Count; i++)
            {
                var pathInputFolder = Path.Combine(PathMachineRootFolder, InputFoldersName[i]);
                var folderName = FileSystem.GetDirectoryNameFromPath(pathInputFolder);

                ////MMIx26
                //if (InputFolderName[i].Equals("Vision"))
                //{
                //    pathInputFolder = Path.Combine(@"C:\CARON\Vision");
                //    folderName = FileSystem.GetDirectoryNameFromPath(pathInputFolder);
                //}
                //else if (InputFolderName[i].Equals("server"))
                //{
                //    pathInputFolder = @"C:\CARON\server";
                //    folderName = FileSystem.GetDirectoryNameFromPath(pathInputFolder);
                //}
                ////MMFx26

                var pathManifest = Path.Combine(PathBuilderFolder, $"machine_{folderName}_manifest.json");
                var pathEncryptedArchive = Path.Combine(PathBuilderFolder, $"machine_{folderName}_data.encrypted_archive");

                ProConsole.WriteLine($"Creating manifest ({folderName})..", ConsoleColor.Cyan);
                SoftwareUpdateInternal.CreateManifest(pathInputFolder, pathManifest);

                ProConsole.WriteLine($"Encrypting folder ({folderName})..", ConsoleColor.Cyan);
                Compression.CreateEncryptedArchiveFromFolder(PathMachineRootFolder, PathMachineUtilityFolder, pathInputFolder, pathEncryptedArchive, Password, CompressionLevel.Low, ProgressConsoleOutputHandler);
            }

            //Final stage
            ProConsole.WriteLine($"[*] ARCHIVING STAGE 2/2]", ConsoleColor.Yellow);
            Compression.CreateEncryptedArchiveFromFolder(PathMachineRootFolder, PathMachineUtilityFolder, PathBuilderFolder, PathSoftwareUpdateBundle, Password, CompressionLevel.Archive, ProgressConsoleOutputHandler);

            ProConsole.WriteLine($"Moving files..", ConsoleColor.Cyan); ;
            string archivesFolder = Path.Combine(PathBuilderFolder, "_archives");
            Directory.CreateDirectory(archivesFolder);

            var files = new List<string>();
            files.AddRange(Directory.GetFiles(PathBuilderFolder, "*.encrypted_archive"));
            files.AddRange(Directory.GetFiles(PathBuilderFolder, "*.json"));
            files.ForEach(x => File.Move(x, Path.Combine(archivesFolder, Path.GetFileName(x))));

            //------------------------------------------------------------------------
            // Check Integrity
            //------------------------------------------------------------------------
            ProRob.TicToc.Tic();
            ProConsole.WriteLine("[CheckIntegrity]", ConsoleColor.Yellow);
            var checkerUpdate = new SoftwareUpdate.CheckSoftwareUpdateBundle(PathMachineRootFolder, BinUtilityFolder);
            bool ret = checkerUpdate.CheckIntegrity(PathSoftwareUpdateBundle, Password);
            ProRob.TicToc.Toc();

            //------------------------------------------------------------------------
            return ret;
        }
    }
}
