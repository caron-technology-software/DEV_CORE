using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProRob;
using ProRob.Extensions.String;
using ProRob.Extensions.Hashing;
using ProRob.Extensions.Json;

namespace Machine.SoftwareUpdate
{
    internal class SoftwareUpdateInternal
    {
        public static Action<string> ProgressConsoleOutputHandler = (x) =>
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

        public static List<Manifest> GetManifestFromFolder(string path)
        {
            List<Manifest> manifests = new List<Manifest>();

            foreach (var item in Directory.GetFiles(path).Where(x => x.Contains("manifest")))
            {
                manifests.Add(ProRob.Json.Deserialize<Manifest>(File.ReadAllText(item)));
            }

            return manifests;
        }

        public static List<FileHash> GetDirectoryFileHashes(string path)
        {
            if (!Directory.Exists(path))
            {
                return null;
            }

            var hashes = new ConcurrentBag<FileHash>();

            var files = FileSystem.GetFiles(path, "*.*", SearchOption.AllDirectories);

            Parallel.ForEach(files, file =>
            {
                if (file.DirectoryName.StartsWith(path))
                {
                    hashes.Add(new FileHash()
                    {
                        Path = file.FullName.Substring(path.Length + 1),
                        //GPIx79    tolti spazi da firma del file.      //GPIxEMERG01 20/02/2024 eliminato controllo di integrità che va in errore per spazi ogni due caratteri! (chiesto da BREDA)
                        //Hash = ProRob.Security.Hashing.ComputeSHA512(file)
                        Hash = String.Concat(ProRob.Security.Hashing.ComputeSHA512(file).Where(c => !Char.IsWhiteSpace(c)))
                        //GPFx79                                        //GPFxEMERG01 20/02/2024 eliminato controllo di integrità che va in errore per spazi ogni due caratteri! (chiesto da BREDA)
                    });
                }
            });

            return hashes.ToList();
        }

        public static string GetDirectoryHash(string path)
        {
            return GetDirectoryHash(GetDirectoryFileHashes(path));
        }

        public static string GetDirectoryHash(List<FileHash> fileHashes)
        {
            string hash = "";

            if (fileHashes is null || fileHashes.Count() == 0)
            {
                return hash;
            }

            fileHashes.Sort();
            fileHashes.ForEach(x => hash = $"{hash}{x.Path}{x.Hash}".GetSHA512Hash());

            return hash;
        }

        public static void CreateManifest(string pathFolder, string pathManifest)
        {
            var fileHashes = GetDirectoryFileHashes(pathFolder).ToList();

            Directory.CreateDirectory(Path.GetDirectoryName(pathManifest));

            var manifest = new Manifest()
            {
                Folder = FileSystem.GetDirectoryNameFromPath(pathFolder),
                Signature = GetDirectoryHash(fileHashes),
                FileHashes = fileHashes
            };

            manifest.ToJsonIndented().SaveToFile(pathManifest);
        }
    }
}
