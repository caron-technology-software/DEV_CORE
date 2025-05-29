#undef VERBOSE_MODE

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

using ProRob.Extensions.Collections;

namespace ProRob
{

    #region Extension Methods
    internal static class ConcurrentBagExtensions
    {
        public static void Clear<T>(this ConcurrentBag<T> concurrentBag)
        {
            while (concurrentBag.TryTake(out _))
            {
                // do nothing
            }
        }
    }
    #endregion

    public class ProFileSystemWatcherEventArgs : EventArgs
    {
        public string[] Files { get; set; }

        public ProFileSystemWatcherEventArgs(string[] files)
        {
            Files = files;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("[ProFileSystemWatcherEventArgs]");
            foreach (var f in Files)
            {
                sb.AppendLine($"\t{f}");
            }

            return sb.ToString();
        }
    }

    public class ProFileSystemWatcher : IDisposable
    {
        #region Data Structures

        public struct ChangeEvent : IComparable<ChangeEvent>
        {
            public DateTime timestamp;
            public WatcherChangeTypes changeType;

            public ChangeEvent(DateTime timestamp, WatcherChangeTypes changeType)
            {
                this.timestamp = timestamp;
                this.changeType = changeType;
            }

            public int CompareTo(ChangeEvent x)
            {
                return this.timestamp.CompareTo(x.timestamp);
            }

            public override string ToString()
            {
                return $"{timestamp.ToString("HH:mm:ss.ffff")} - {changeType}";
            }
        }

        public struct FileSystemChange
        {
            public DateTime timestamp;
            public WatcherChangeTypes changeType;
            public string fullPath;
            public string name;
            public bool isDirectory;
            public string oldFullPath;
            public string oldName;

            public FileSystemChange(DateTime timestamp, WatcherChangeTypes changeType, string fullPath, bool isDirectory, string name)
            {
                this.timestamp = timestamp;
                this.changeType = changeType;
                this.fullPath = fullPath;
                this.isDirectory = isDirectory;
                this.name = name;
                this.oldFullPath = "";
                this.oldName = "";
            }

            public FileSystemChange(DateTime timestamp, WatcherChangeTypes changeType, string fullPath, bool isDirectory, string name, string oldFullPath, string oldName)
                : this(timestamp, changeType, fullPath, isDirectory, name)
            {

                this.oldFullPath = oldFullPath;
                this.oldName = oldName;
            }

            public override string ToString()
            {
                string type = isDirectory ? "FOLDER" : "FILE";
                return $"{timestamp.ToString("HH:mm:ss.ffff")}: {type} -> {changeType}: {fullPath}";
            }
        }

        #endregion

        private FileSystemWatcher watcher;
        private FileSystemModel fileSystemModel;
        private ConcurrentBag<FileSystemChange> eventsToProcess;

        private System.Timers.Timer timer;

        public long NumberOfMonitoredFiles { get => fileSystemModel.NumberOfFiles; }
        public long NumberOfMonitoredDirectories { get => fileSystemModel.NumberOfDirectories; }
        public string PathToMonitor { get; private set; }
        public long NumberOfEventsHandled { get; private set; } = 0;
        public int NumberOfErrors { get; private set; } = 0;

        // Events
        public event EventHandler<ProFileSystemWatcherEventArgs> FileNewOrAdded;
        public event EventHandler<ProFileSystemWatcherEventArgs> FileDeleted;

        public ProFileSystemWatcher(string path, TimeSpan timerTimespan)
        {
            if (!System.IO.Directory.Exists(path))
            {
                throw new System.IO.DirectoryNotFoundException();
            }

            // Objects initialization
            watcher = new FileSystemWatcher();
            fileSystemModel = new FileSystemModel(path);
            eventsToProcess = new ConcurrentBag<FileSystemChange>();

            timer = new System.Timers.Timer
            {
                Interval = timerTimespan.TotalMilliseconds,
                AutoReset = false
            };
            timer.Elapsed += OnFileSystemWatcherTimedEvent;

            PathToMonitor = path;

            // Settings
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.DirectoryName | NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Path = path;
            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;
            watcher.InternalBufferSize = watcher.InternalBufferSize * 10;

            // Events handler
            watcher.Created += new FileSystemEventHandler(OnFileSystemChangeOccured);
            watcher.Deleted += new FileSystemEventHandler(OnFileSystemChangeOccured);
            watcher.Changed += new FileSystemEventHandler(OnFileSystemChangeOccured);
            watcher.Renamed += new RenamedEventHandler(OnFileSystemChangeOccured);
            watcher.Error += OnWatcherError;

            // Begin watching
            watcher.EnableRaisingEvents = true;
        }

        private void OnWatcherError(object sender, ErrorEventArgs e)
        {
            NumberOfErrors++;
            Console.WriteLine($"[ProFileSystemWatcher] -> ERROR");
        }

        public string[] GetDirectories()
        {
            return fileSystemModel.GetDirectories();
        }

        public string[] GetFiles()
        {
            return fileSystemModel.GetFiles();
        }

        private bool IsDirectory(string fullPath, WatcherChangeTypes watcherChangeTypes)
        {
            return fileSystemModel.IsDirectory(fullPath, CheckDirectoryFromPhysicalFileSystem(watcherChangeTypes));
        }

        private bool CheckDirectoryFromPhysicalFileSystem(WatcherChangeTypes watcherChangeTypes)
        {
            if ((watcherChangeTypes == WatcherChangeTypes.Changed) ||
               (watcherChangeTypes == WatcherChangeTypes.Created) ||
               (watcherChangeTypes == WatcherChangeTypes.Renamed))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnFileSystemWatcherTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            // Suddivisione eventi
            var deletedDirectoriesEvents = eventsToProcess.Where(x => x.isDirectory == true && x.changeType == WatcherChangeTypes.Deleted).ToList();
            var renamedDirectoriesEvents = eventsToProcess.Where(x => x.isDirectory == true && x.changeType == WatcherChangeTypes.Renamed).ToList();

            var deletedFilesEvents = eventsToProcess.Where(x => x.isDirectory == false && x.changeType == WatcherChangeTypes.Deleted).ToList();
            var createdFilesEvents = eventsToProcess.Where(x => x.isDirectory == false && x.changeType == WatcherChangeTypes.Created).ToList();
            var changedFilesEvents = eventsToProcess.Where(x => x.isDirectory == false && x.changeType == WatcherChangeTypes.Changed).ToList();
            var renamedFilesEvents = eventsToProcess.Where(x => x.isDirectory == false && x.changeType == WatcherChangeTypes.Renamed).ToList();

            // Elaborazione eventi relativi alle cartelle eliminate
            foreach (var directory in deletedDirectoriesEvents)
            {
                var files = fileSystemModel.GetFullPathFilesFromSubDirectories(directory.fullPath);

                foreach (var file in files)
                {
                    deletedFilesEvents.Add(new FileSystemChange(directory.timestamp, WatcherChangeTypes.Deleted, file, false, FileSystem.GetFilenameFromFullPath(file)));
                }
            }

            // Elaborazione eventi relativi alle cartelle rinominate
            foreach (var directory in renamedDirectoriesEvents)
            {
                var files = fileSystemModel.GetFullPathFilesFromSubDirectories(directory.oldFullPath);

                foreach (var file in files)
                {
                    var filename = FileSystem.GetFilenameFromFullPath(file);

                    deletedFilesEvents.Add(new FileSystemChange(directory.timestamp, WatcherChangeTypes.Deleted, Path.Combine(directory.oldFullPath, filename), false, filename));
                    createdFilesEvents.Add(new FileSystemChange(directory.timestamp, WatcherChangeTypes.Created, Path.Combine(directory.fullPath, filename), false, filename));
                }
            }

            // Elaborazione eventi relativi ai files rinominati
            foreach (var file in renamedFilesEvents)
            {
                var filename = FileSystem.GetFilenameFromFullPath(file.oldFullPath);

                deletedFilesEvents.Add(new FileSystemChange(file.timestamp, WatcherChangeTypes.Deleted, file.oldFullPath, false, filename));
                createdFilesEvents.Add(new FileSystemChange(file.timestamp, WatcherChangeTypes.Created, file.fullPath, false, filename));
            }

            // Raggruppamento di tutti gli eventi
            var filesEvents = new List<FileSystemChange>();
            filesEvents.AddRange(deletedFilesEvents);
            filesEvents.AddRange(createdFilesEvents);
            filesEvents.AddRange(changedFilesEvents);

            // Raggruppamento eventi per file
            var fileChangeEvents = new Dictionary<string, List<ChangeEvent>>();
            foreach (var f in filesEvents)
            {
                if (!fileChangeEvents.ContainsKey(f.fullPath))
                {
                    fileChangeEvents.Add(f.fullPath, new List<ChangeEvent>());
                }

                fileChangeEvents[f.fullPath].Add(new ChangeEvent(f.timestamp, f.changeType));
            }

            // Ordinamento eventi di ogni file per timestamp
            fileChangeEvents.Keys.ForEach(key => { fileChangeEvents[key].Sort(); });

#if VERBOSE_MODE
            ProConsole.WriteLine($"FILE CHANGE EVENTS (n={fileChangeEvents.Count()}):", ConsoleColor.Red);
            foreach (var f in fileChangeEvents)
            {
                ProConsole.WriteLine($"{f.Key}", ConsoleColor.Red);
                f.Value.ForEach(x => { ProConsole.WriteLine($"\t{x}"); });
            }
#endif
            var newOrChangedFiles = new List<string>();
            var deletedFiles = new List<string>();

            // Elaborazione eventi per singolo file
            foreach (var fullPath in fileChangeEvents.Keys.ToArray())
            {
                var fileChanges = fileChangeEvents[fullPath].ToList();
                var lastFileChange = fileChanges.Last();

                if ((lastFileChange.changeType == WatcherChangeTypes.Changed) ||
                    (lastFileChange.changeType == WatcherChangeTypes.Created))
                {
                    // Verifica se il file è attualmente in uso
                    if (!FileSystem.IsFileInUse(fullPath))
                    {
                        newOrChangedFiles.Add(fullPath);
                    }
#if VERBOSE_MODE
                    else
                    {
                        Console.WriteLine($"File {fullPath} is in use");
                    }
#endif
                }
                else if (lastFileChange.changeType == WatcherChangeTypes.Deleted)
                {
                    deletedFiles.Add(fullPath);
                }
            }

            // FileSystem replication
            deletedFiles.ForEach(x => { fileSystemModel.DeleteFile(new FileSystemModel.FileModel(x)); });
            newOrChangedFiles.ForEach(x => { fileSystemModel.AddFile(new FileSystemModel.FileModel(x)); });

#if VERBOSE_MODE
            ProConsole.WriteLine($"NEW OR CHANGED FILES:", ConsoleColor.Red);
            newOrChangedFiles.ForEach(x => { Console.WriteLine($"\t{x}"); });
            ProConsole.WriteLine($"DELETED FILES:", ConsoleColor.Red);
            deletedFiles.ForEach(x => { Console.WriteLine($"\t{x}"); });
#endif

            // Send events
            FileNewOrAdded?.Invoke(this, new ProFileSystemWatcherEventArgs(newOrChangedFiles.ToArray()));

            eventsToProcess.Clear();

            // Send Events
            SendEvents(newOrChangedFiles.ToArray(), deletedFiles.ToArray());
        }

        private void SendEvents(string[] newOrChangedFiles, string[] deletedFiles)
        {
            if ((FileNewOrAdded != null) && (newOrChangedFiles.Count() > 0))
            {
                FileNewOrAdded(this, new ProFileSystemWatcherEventArgs(newOrChangedFiles));
            }

            if ((FileDeleted != null) && (deletedFiles.Count() > 0))
            {
                FileDeleted(this, new ProFileSystemWatcherEventArgs(deletedFiles));
            }
        }

        private void OnFileSystemChangeOccured(object source, FileSystemEventArgs e)
        {
            DateTime timestamp = DateTime.Now;

            NumberOfEventsHandled++;

#if VERBOSE_MODE 
            var folderFileType = IsDirectory(e.FullPath, e.ChangeType) ? "FOLDER" : "FILE";
            Console.WriteLine($"[{e.ChangeType}]->{folderFileType}\n\tFullPath: {e.FullPath}\n\tName: {e.Name}");
            if (e.ChangeType == WatcherChangeTypes.Renamed)
            {
                Console.WriteLine($"\tOldFullPath: {((RenamedEventArgs)e).OldFullPath} -> OldName: {((RenamedEventArgs)e).OldName}");
            }
#endif
            // Creazione eventi da processare
            if (e.ChangeType == WatcherChangeTypes.Renamed)
            {
                eventsToProcess.Add(
                    new FileSystemChange(timestamp, e.ChangeType, e.FullPath, IsDirectory(e.FullPath, e.ChangeType), e.Name,
                            ((RenamedEventArgs)e).OldFullPath, ((RenamedEventArgs)e).OldName)
                            );
            }
            else
            {
                eventsToProcess.Add(
                    new FileSystemChange(timestamp, e.ChangeType, e.FullPath, IsDirectory(e.FullPath, e.ChangeType), e.Name));
            }

            // Reset timer
            timer.Stop();
            timer.Start();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("[ProFileSystemWatcher]");
            sb.AppendLine($"\tPathToMonitor: {PathToMonitor}");
            sb.AppendLine($"\tNumberOfMonitoredFiles: {NumberOfMonitoredFiles}");
            sb.AppendLine($"\tNumberOfMonitoredDirectories: {NumberOfMonitoredDirectories}");
            sb.AppendLine($"\tNumberOfEventsHandled: {NumberOfEventsHandled}");
            sb.AppendLine($"\tNumberOfErrors: {NumberOfErrors}");
            sb.Append(fileSystemModel);

            return sb.ToString();
        }

        public void PrintFileSystem()
        {
            fileSystemModel.PrintFileSystem();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }

                if (watcher != null)
                {
                    watcher.Dispose();
                    watcher = null;
                }
            }
        }
    }

}