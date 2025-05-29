using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

//[TODO], [REFACTORING]
//DirectoryInfo di = new DirectoryInfo(path);
//DirectoryInfo.EnumerateDirectories Metodo

namespace ProRob
{
    public static class FileSystem
    {
        public static string ReplacePath(string path, string fromDirectory, string toDirectory)
        {
            return toDirectory + new string(path.Skip(fromDirectory.Length).ToArray());
        }

        public static List<string> GetFilesFromCommandPrompt(string path, string tmpPath)
        {
            var guid = Guid.NewGuid();

            string tmpFile = Path.Combine(tmpPath, $"{guid}.tmp");

            ProcessHelper.ExecuteShellCommand($"dir {path} /b /s /a-d > {tmpFile}");

            var files = File.ReadAllLines(tmpFile);

            try
            {
                File.Delete(tmpFile);
            }
            catch (Exception ex)
            {
                // --
            }

            return files.ToList();
        }


        public static bool IsFileInUse(string path)
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                throw new NotImplementedException();
            }

            //https://stackoverflow.com/questions/876473/is-there-a-way-to-check-if-a-file-is-in-use

            path = Path.Combine(path);

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    var ret = fs.CanWrite;
                }
                return false;
            }
            catch
            {
                return true;
            }
        }

        public static IEnumerable<string> FilterFiles(string path, params string[] extensions)
        {
            return extensions.SelectMany(x => Directory.EnumerateFiles(path, x, SearchOption.AllDirectories));
        }

        public static bool DeleteDirectory(string path)
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                throw new NotImplementedException();
            }

            path = Path.Combine(path);

            if (Directory.Exists(path))
            {
                ProcessHelper.ExecuteShellCommand($"rmdir {path} /S /Q");
            }

            return true;
        }

        public static bool IsDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string GetFilenameFromFullPath(string path)
        {
            path = Path.Combine(path);

            return path.Substring(Path.Combine(path).LastIndexOf(@"\") + 1);
        }

        public static string GetDirectoryPathFromFileFullPath(string fullPath)
        {
            fullPath = Path.Combine(fullPath);

            return fullPath.Substring(0, Path.Combine(fullPath).LastIndexOf(@"\"));
        }

        public static IEnumerable<string> ProcessCollectionToRemoveSubDirectories(IEnumerable<string> directoriesToProcess)
        {
            var directories = directoriesToProcess.ToList();
            var idxToDelete = new List<int>();

            do
            {
                idxToDelete.Clear();

                if (directories.Count() > 1)
                {
                    for (int i = 0; i < directories.Count - 1; i++)
                    {
                        for (int j = i + 1; j < directories.Count; j++)
                        {
                            if (IsSubDirectory(directories[i], directories[j]))
                            {
                                idxToDelete.Add(j);
                            }
                            else if (IsSubDirectory(directories[j], directories[i]))
                            {
                                idxToDelete.Add(i);
                            }
                        }
                    }
                }

                idxToDelete.OrderByDescending(x => x).Distinct().ToList().ForEach(index => { directories.RemoveAt(index); });

            } while (idxToDelete.Count > 0);

            return directories;
        }

        public static bool IsSubDirectory(string directory, string subDirectory)
        {
            if (directory == subDirectory)
            {
                return false;
            }

            return subDirectory.Contains(directory);
        }

        // This method assumes that the application has discovery permissions for all folders under the specified path.
        public static IEnumerable<System.IO.FileInfo> GetDirectoriesWithoutSecurityAccessChecks(string path)
        {
            return GetDirectoriesWithoutSecurityAccessChecks(path, SearchOption.AllDirectories);
        }

        public static IEnumerable<System.IO.FileInfo> GetDirectoriesWithoutSecurityAccessChecks(string path, SearchOption searchOption)
        {
            if (!System.IO.Directory.Exists(path))
            {
                throw new System.IO.DirectoryNotFoundException();
            }

            var directories = new List<System.IO.FileInfo>
            {
                new System.IO.FileInfo(path)
            };

            foreach (var item in System.IO.Directory.GetDirectories(path, "*.*", searchOption))
            {
                try
                {
                    directories.Add(new System.IO.FileInfo(item));
                }
                catch
                {
                    // Nothing to do
                }
            }

            return directories;
        }

        public static string GetDirectoryNameFromPath(string path)
        {
            return path.Substring(Path.GetDirectoryName(path).Length + 1);
        }

        public static string[] GetDirectories(string path)
        {
            path = Path.Combine(path);

            if (!Directory.Exists(path))
            {
                return null;
            }

            var dirsBag = new ConcurrentBag<string>();

            InternalGetDirectories(path, dirsBag);

            var directories = dirsBag.ToArray();
            Array.Sort(directories);

            return directories;

            void InternalGetDirectories(string directoryPath, ConcurrentBag<string> bag)
            {
                try
                {
                    bag.Add(directoryPath);

                    var dirs = Directory.GetDirectories(directoryPath, "*.*", SearchOption.TopDirectoryOnly);

                    if (dirs.Length > 0)
                    {
                        Parallel.For(0, dirs.Length, i =>
                        {
                            InternalGetDirectories(dirs[i], bag);
                        });
                    }

                    return;
                }
                catch
                {
                    //--
                }
            }
        }

        public static IEnumerable<System.IO.FileInfo> GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            List<System.IO.FileInfo> files = new List<System.IO.FileInfo>();

            if (!System.IO.Directory.Exists(path))
            {
                return files;
            }

            string[] directories;
            if (searchOption == SearchOption.TopDirectoryOnly)
            {
                directories = new string[] { path };
            }
            else
            {
                directories = GetDirectories(path);
            }

            try
            {
                foreach (var dir in directories)
                {
                    var filenames = System.IO.Directory.GetFiles(dir, searchPattern, SearchOption.TopDirectoryOnly);
                    foreach (var file in filenames)
                    {
                        files.Add(new System.IO.FileInfo(file));
                    }
                }
            }
            catch
            {
                //--
            }

            return files;
        }

        public static void RemoveOldFiles(string directoryPath, TimeSpan timespan)
        {
            try
            {
                string[] files = Directory.GetFiles(directoryPath);

                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);

                    if (fi.LastAccessTime < (DateTime.Now - timespan))
                    {
                        fi.Delete();
                    }
                }
            }
            catch
            {
                //--
            }
        }

        public static IEnumerable<System.IO.FileInfo> GetFiles(string path)
        {
            return GetFiles(path, "*.*", SearchOption.AllDirectories);
        }

        public static List<FileInfo> GetFilesFromDifferentDirectoriesOrFiles(string[] paths)
        {
            var files = new List<FileInfo>();

            foreach (var path in paths)
            {
                if (IsDirectory(path))
                {
                    files.AddRange(GetFiles(path));
                }
                else
                {
                    files.Add(new FileInfo(path));
                }
            }

            return files;
        }

        public static string GetFilename(FileInfo file)
        {
            return file.Name.Substring(0, file.Name.Length - file.Extension.Length);
        }

        //public static bool CheckPath(string path)
        //{
        //    string command = $"cd {path}";

        //    (_, int ret) = ProcessHelper.ExecuteShellCommand(command);

        //    return ret == 0;
        //}

        public static bool CheckPathWithWritePermission(string path)
        {
            string pathFile = Path.Combine(path, $"{Guid.NewGuid()}.tmp");
            string content = string.Empty;

            try
            {
                File.WriteAllText(pathFile, content);

                File.Delete(pathFile);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // This implementation defines a very simple comparison
        // between two FileInfo objects. It only compares the name
        // of the files being compared and their length in bytes.
        class FileCompare : IEqualityComparer<System.IO.FileInfo>
        {
            //[TO IMPROVE]
            static FileCompare()
            {
                // --
            }

            public bool Equals(System.IO.FileInfo f1, System.IO.FileInfo f2)
            {
                return (f1.Name == f2.Name &&
                        f1.Length == f2.Length);
            }

            // Return a hash that reflects the comparison criteria. According to the 
            // rules for IEqualityComparer<T>, if Equals is true, then the hash codes must
            // also be equal. Because equality as defined here is a simple value equality, not
            // reference identity, it is possible that two or more objects will produce the same
            // hash code.
            public int GetHashCode(System.IO.FileInfo fi)
            {
                string s = String.Format("{0}{1}", fi.Name, fi.Length);
                return s.GetHashCode();
            }
        }
    }
}
