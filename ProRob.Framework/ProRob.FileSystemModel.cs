//#define VERBOSE_MODE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using ProRob.Extensions.Collections;

namespace ProRob
{
    public class FileSystemModel
    {
        #region Data Structures
        public struct FileModel
        {
            public string directoryPath;
            public string filename;

            public FileModel(string fullPath)
            {
                this.directoryPath = FileSystem.GetDirectoryPathFromFileFullPath(fullPath);
                this.filename = FileSystem.GetFilenameFromFullPath(fullPath);
            }

            public FileModel(string directoryPath, string filename)
            {
                this.directoryPath = directoryPath;
                this.filename = filename;
            }

            public FileModel(ProFileSystemWatcher.FileSystemChange fileSystemChange)
            {
                if (fileSystemChange.isDirectory)
                {
                    throw new ArgumentException("FileSystemChange must rappresents a file");
                }

                this.directoryPath = FileSystem.GetDirectoryPathFromFileFullPath(fileSystemChange.fullPath);
                this.filename = fileSystemChange.name;
            }

            public override string ToString()
            {
                return $"Directory Path: {directoryPath} - Filename: {filename}";
            }
        }
        #endregion

        public long NumberOfFiles { get => fileSystem.SelectMany(x => x.Value).Count(); }
        public long NumberOfDirectories { get => fileSystem.Count(); }
        public int NumberOfCollectionErrors { get; private set; } = 0;

        private ConcurrentDictionary<string, HashSet<string>> fileSystem;

        public FileSystemModel(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                throw new System.IO.DirectoryNotFoundException();
            }

            fileSystem = new ConcurrentDictionary<string, HashSet<string>>();

            // FileSystemModel
            var directories = FileSystem.GetDirectories(path);

            Parallel.For(0, directories.Length, i =>
            {
                var key = new HashSet<string>(FileSystem.GetFiles(directories[i], "*.*", SearchOption.TopDirectoryOnly).Select(x => x.Name).ToList());

                if (!fileSystem.TryAdd(directories[i], key))
                {
                    NumberOfCollectionErrors++;
                }
            });

        }

        public bool IsDirectory(string fullPath, bool checkPhysicalFilesystem)
        {
            bool ret = checkPhysicalFilesystem ? FileSystem.IsDirectory(fullPath) : fileSystem.ContainsKey(fullPath);

#if VERBOSE_MODE
            //Console.WriteLine($"IsDirectory -> fullPath: {fullPath} => {ret}");
#endif
            return ret;
        }

        public void AddFile(FileModel fileModel)
        {
            if (!FileSystem.IsDirectory(fileModel.directoryPath))
            {
                if (!fileSystem.TryAdd(fileModel.directoryPath, new HashSet<string> { fileModel.filename }))
                {
                    NumberOfCollectionErrors++;
                }
            }
            else
            {
                // Verifica presenza Key
                if (!fileSystem.ContainsKey(fileModel.directoryPath))
                {
                    if (!fileSystem.TryAdd(fileModel.directoryPath, new HashSet<string> { fileModel.filename }))
                    {
                        NumberOfCollectionErrors++;
                    }

                    return;
                }

                // Verifica presenza file
                if (fileSystem[fileModel.directoryPath].Contains(fileModel.filename))
                {
                    return;
                }

                // Aggiunta file alla collezione
                fileSystem[fileModel.directoryPath].Add(fileModel.filename);
            }
        }

        public void DeleteFile(FileModel fileModel)
        {
            if (fileSystem.ContainsKey(fileModel.directoryPath))
            {
                if (fileSystem[fileModel.directoryPath].Contains(fileModel.filename))
                {
                    fileSystem[fileModel.directoryPath].Remove(fileModel.filename);
                }
            }
        }

        public void DeleteDirectory(string directoryPath)
        {
            if (!fileSystem.ContainsKey(directoryPath))
            {
                return;
            }

            if (!fileSystem.TryRemove(directoryPath, out _))
            {
                NumberOfCollectionErrors++;
            }

            var subDirectories = GetSubDirectories(directoryPath);

            subDirectories.ForEach(x =>
                {
                    if (!fileSystem.TryRemove(x, out _))
                    {
                        NumberOfCollectionErrors++;
                    }
                });
        }

        public string[] GetFullPathFilesFromDirectory(string directoryPath)
        {
            if (!fileSystem.ContainsKey(directoryPath))
            {
                return new string[] { };
            }

            var files = fileSystem[directoryPath].ToArray();

            return files.Select(x => directoryPath + @"\" + x).ToArray();
        }

        public string[] GetFullPathFilesFromSubDirectories(string directoryPath)
        {
            var files = new List<string>();
            files.AddRange(GetFullPathFilesFromDirectory(directoryPath));

            var subDirectories = fileSystem.Where(x => x.Key.Contains(directoryPath) && x.Key != directoryPath).Select(x => x.Key).ToList();

            subDirectories.ForEach(x => { files.AddRange(GetFullPathFilesFromDirectory(x)); });

            return files.ToArray();
        }

        public string[] GetSubDirectories(string directoryPath)
        {
            return fileSystem.Where(x => x.Key.Contains(directoryPath) && x.Key != directoryPath).Select(x => x.Key).ToArray();
        }

        public string[] GetDirectories()
        {
            return fileSystem.Select(x => x.Key).ToArray();
        }

        public string[] GetFiles()
        {
            var files = new List<string>();

            foreach (var dir in fileSystem)
            {
                foreach (var file in dir.Value)
                {
                    files.Add($"{dir.Key}\\{file}");
                }
            }

            return files.ToArray();
        }

        public void PrintFileSystem()
        {
            ProConsole.WriteLine("[FILESYSTEM MODEL]", ConsoleColor.Red);

            foreach (var key in fileSystem.Keys.ToList())
            {
                ProConsole.WriteLine(key, ConsoleColor.Green);
                foreach (var file in fileSystem[key])
                {
                    Console.WriteLine($"\t{file}");
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"[FileSystemModel]");
            sb.AppendLine($"\tNumberOfFiles: {NumberOfFiles}");
            sb.AppendLine($"\tNumberOfDirectories: {NumberOfDirectories}");
            sb.AppendLine($"\tNumberOfCollectionErrors: {NumberOfCollectionErrors}");

            //foreach (var key in fileSystem.Keys.ToList())
            //{
            //    sb.AppendLine(key);
            //    foreach (var file in fileSystem[key])
            //    {
            //        sb.AppendLine($"\t{file}");
            //    }
            //}

            return sb.ToString();
        }
    }
}