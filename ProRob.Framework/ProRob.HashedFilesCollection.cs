using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Collections.Concurrent;

using ProRob.Security;
using ProRob.Extensions.String;
using ProRob.Extensions.Collections;

namespace ProRob
{
    #region Data Structures

    [Serializable]
    public struct FileInfoHash
    {
        public FileInfo FileInfo;
        public string Hash;
        public int? IdFileType;

        public FileInfoHash(FileInfo fileInfo, string hash, int? idFileType = null)
        {
            this.FileInfo = fileInfo;
            this.Hash = hash;
            this.IdFileType = idFileType;
        }

        public override string ToString()
        {
            return $"{FileInfo.FullName}\n\t-> {Hash}";
        }
    }

    [Serializable]
    public struct FileHash
    {
        public string FullPath;
        public string Hash;
        public int? IdFileType;

        public FileHash(string fullPath, string hash, int? idFileType = null)
        {
            this.FullPath = fullPath;
            this.Hash = hash;
            this.IdFileType = idFileType;
        }

        public override string ToString()
        {
            return $"{FullPath} -> {Hash}";
        }
    }

    public struct FileFilter
    {
        public string TypeName;
        public string[] Extensions;
        public long MaxFileSize;
        public Func<string, bool> ContentChecker;

        public FileFilter(string typeName, string[] extensions, long maxFileSize, Func<string, bool> contentChecker = null)
        {
            this.TypeName = typeName;
            this.Extensions = extensions;
            this.MaxFileSize = maxFileSize;
            this.ContentChecker = contentChecker;
        }

        public FileFilter(string typeName, string[] extensions, ByteSize maxFileSize, Func<string, bool> contentChecker = null)
            : this(typeName, extensions, (long)maxFileSize.Bytes, contentChecker)
        {

        }
    }

    #endregion

    public class HashedFilesCollection
    {
        private ConcurrentDictionary<string, FileInfoHash> filesCollection;

        private readonly Func<FileInfo, string> hashFunction;

        private FileFilter[] fileFilters;

        public string[] ExtensionsFilter { get; private set; }
        public long NumberOfCollectedFiles { get => filesCollection.Count(); }
        public long NumberOfEventsHandled { get; private set; } = 0;
        public int NumberOfErrors { get; private set; } = 0;
        public int NumberOfCollectionErrors { get; private set; } = 0;

        public HashedFilesCollection(Func<FileInfo, string> hashFunction, FileFilter[] fileFilters = null)
        {
            //Console.WriteLine("[HashedFilesCollection]");

            this.filesCollection = new ConcurrentDictionary<string, FileInfoHash>();

            this.hashFunction = hashFunction;

            if (fileFilters != null)
            {
                this.fileFilters = fileFilters;

                ExtensionsFilter = fileFilters.SelectMany(x => x.Extensions).Distinct().ToArray();
                ExtensionsFilter.ForEach(x => { x = x.ToLower(); });

                // Print extensions
                //Console.Write("\tExtension Filter: ");
                //ExtensionsFilter.ForEach(x => { Console.Write($"{x} "); });
                //Console.WriteLine("");
            }
        }

        public Dictionary<string, FileInfoHash> GetFilesDictionary()
        {
            return filesCollection.ToDictionary(x => x.Key, x => x.Value);
        }

        public IEnumerable<string> GetFiles()
        {
            return filesCollection.Values.Select(x => x.FileInfo.FullName);
        }

        public void RemoveFiles(string[] fullPath)
        {
            fullPath.ForEach(x => { RemoveFile(x); });
        }

        public void RemoveFile(string fullPath)
        {
            NumberOfEventsHandled++;

            if (filesCollection.ContainsKey(fullPath))
            {
                if (!filesCollection.TryRemove(fullPath, out _))
                {
                    NumberOfCollectionErrors++;
                }
            }
        }

        public void AddFilesFromPath(string path)
        {
            var files = FileSystem.GetFiles(path).Select(x => x.FullName).ToArray();
            //files.ForEach(x => { AddFile(x.FullName); });
            Parallel.For(0, files.Count(), i => { AddFile(files[i]); });
        }

        public void AddFiles(string[] fullPath)
        {
            //fullPath.ForEach(x => { AddFile(x); });
            Parallel.For(0, fullPath.Length, i => { AddFile(fullPath[i]); });
        }

        public void AddFile(string fullPath)
        {
            NumberOfEventsHandled++;

            string extension = "";
            int idxFilterFile = -1;

            try
            {
                // Verifica presenza file
                if (!File.Exists(fullPath))
                {
                    return;
                }

                var fileInfo = new FileInfo(fullPath);

                long size = fileInfo.Length;

                // Se non sono presenti filtri
                if (fileFilters != null)
                {
                    if (fileInfo.Extension.Length > 0)
                    {
                        extension = fileInfo.Extension.ToLower();
                    }
                    else
                    {
                        return;
                    }

                    // Verifico se estensione presente
                    if (!ExtensionsFilter.Any(x => x.SequenceEqual(extension)))
                    {
                        return;
                    }

                    var fileFiltersToTest = fileFilters.Where(x => x.Extensions.Contains(extension)).ToArray();

                    for (int i = 0; i < fileFilters.Count(); i++)
                    {
                        // Verifica dimensione file
                        if (size > fileFilters[i].MaxFileSize)
                        {
                            continue;
                        }

                        // Verifica contenuto file
                        if ((fileFilters[i].ContentChecker == null) || (fileFilters[i].ContentChecker(fullPath)))
                        {
                            idxFilterFile = i;
                            break;
                        }
                    }
                }

                if (idxFilterFile >= 0)
                {
                    //Console.WriteLine($"Adding file {fileInfo.Name} [{fileFilters[idxFilterFile].typeName}] ..");

                    RemoveFile(fullPath);

                    if (!filesCollection.TryAdd(fullPath, new FileInfoHash(fileInfo, hashFunction(fileInfo), idxFilterFile)))
                    {
                        NumberOfCollectionErrors++;
                    }
                }
            }
            catch
            {
                NumberOfErrors++;
            }
        }

        public void AddFiles(object source, ProFileSystemWatcherEventArgs e)
        {
            //Console.WriteLine($"ADD=>{e}");

            Task.Run(() => { AddFiles(e.Files); });
        }

        public void RemoveFiles(object source, ProFileSystemWatcherEventArgs e)
        {
            //Console.WriteLine($"REMOVE=>{e}");

            Task.Run(() => { RemoveFiles(e.Files); });
        }

        public static string CalculateHashOfDirectoryFromFileName(string path, Func<byte[], string> hashFunction)
        {
            var sb = new StringBuilder();
            var files = FileSystem.GetFiles(path).OrderBy(x => x.FullName);

            files.ForEach(x => { sb.Append(hashFunction(x.FullName.ToBytes())); });

            return Hashing.ComputeSHA512(sb.ToString().ToBytes());
        }

        public static string CalculateHashOfDirectoryFromFileContent(string path, Func<string, string> hashFunction)
        {
            var files = FileSystem.GetFiles(path).OrderBy(x => x.FullName).Select(x => x.FullName).ToArray();
            var hashes = new string[files.Length];

            Parallel.For(0, hashes.Length, i => { hashes[i] = hashFunction(files[i]); });

            string.Join("", hashes);

            return Hashing.ComputeSHA512(string.Join("", hashes).ToBytes());
        }

        public string CalculateHashOfCollection()
        {
            var sb = new StringBuilder();

            filesCollection.OrderBy(x => x.Key).ForEach(x => { sb.Append(x.Value.Hash); });

            return Hashing.ComputeSHA512(sb.ToString().ToBytes());
        }

        public List<FileHash> GetFileHashFromCollection()
        {
            return filesCollection.Select(x => new FileHash(x.Key, x.Value.Hash)).ToList<FileHash>();
        }

        public List<FileInfoHash> GetFileInfoHashFromCollection()
        {
            return filesCollection.Select(x => x.Value).ToList();
        }

        public static List<FileHash> GetFileHashFromDirectory(string path, Func<string, string> hashFunction)
        {
            var files = FileSystem.GetFiles(path).ToList();
            var fileHashes = new List<FileHash>(files.Count);

            Parallel.For(0, files.Count, i =>
            {
                var hash = hashFunction(files[i].FullName);
                fileHashes.Add(new FileHash(files[i].FullName, hash));
            });

            return fileHashes.OrderBy(x => x.FullPath).ToList();
        }

        public static void PrintDifferenceFromFileInfoHashCollection(List<FileHash> list1, List<FileHash> list2)
        {
            var filesDifference = list1.Select(x => x.FullPath).Except(list2.Select(x => x.FullPath)).ToList();

            int hashDifference = 0;
            foreach (var file in list1)
            {
                if (file.Hash != list2.Find(x => x.FullPath == file.FullPath).Hash)
                {
                    hashDifference++;
                }
            }

            Console.WriteLine($"FILE DIFFERENCES: {filesDifference.Count}");
            Console.WriteLine($"HASH DIFFERENCES: {hashDifference}");

        }

        public void PrintFiles()
        {
            ProConsole.WriteLine("[HASHED FILES COLLECTION]", ConsoleColor.Red);

            foreach (var key in filesCollection.Keys.ToList())
            {
                Console.WriteLine(filesCollection[key]);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("[HashedFilesCollection]");
            sb.AppendLine($"\tNumberOfEventsHandled: {NumberOfEventsHandled}");
            sb.AppendLine($"\tNumberOfCollectedFiles: {NumberOfCollectedFiles}");
            sb.AppendLine($"\tNumberOfErrors: {NumberOfErrors}");

            if (fileFilters != null)
            {
                for (int i = 0; i < fileFilters.Count(); i++)
                {
                    sb.AppendLine($"\tType: {fileFilters[i].TypeName} -> {filesCollection.Where(x => x.Value.IdFileType == i).Count()} files");
                }
            }

            return sb.ToString();
        }
    }
}
