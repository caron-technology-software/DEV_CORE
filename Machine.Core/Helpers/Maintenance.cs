using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

namespace Machine
{
    public class Maintenance
    {
        public static void BackupFolder(string sourceFolder, string destinationFolder)
        {
            sourceFolder = Path.Combine(sourceFolder);
            destinationFolder = Path.Combine(destinationFolder);

            string directoryName = new string(sourceFolder.Skip(Path.GetDirectoryName(sourceFolder).Length + 1).ToArray());
            string destinationFile = Path.Combine(destinationFolder, $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_{directoryName}.zip");

            ZipFile.CreateFromDirectory(sourceFolder, destinationFile, CompressionLevel.Optimal, false);
        }

        public static void RemoveOldFiles(string path, int numberOfDays)
        {
            path = Path.Combine(path);

            var files = ProRob.FileSystem.GetFiles(path).Where(x => x.CreationTime < (DateTime.Now - TimeSpan.FromDays(numberOfDays)));

            foreach (var file in files)
            {
                File.Delete(file.FullName);
            }
        }
    }
}

