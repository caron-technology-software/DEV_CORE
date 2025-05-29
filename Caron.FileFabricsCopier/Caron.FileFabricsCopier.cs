using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Caron.FileFormats;

using ProRob;

namespace Lion.Copier
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            const bool AlwaysCopyFiles = true;
            const bool DeleteDestinationFiles = false;
#if DEBUG
            string sourceFolder = @"T:\_TMP";
            string denninsonDestinationFolder = @"K:\D\";
            string gerberDestinationFolder = @"K:\G";
#else
            string sourceFolder = args[0];
            string denninsonDestinationFolder = args[1];
            string gerberDestinationFolder = args[2];
#endif
            ProConsole.WriteLine("CARON File Fabrics Copier (powered by PROROB)", ConsoleColor.Green);

            sourceFolder = new string(sourceFolder.TrimEnd('\\').Append('\\').ToArray());
            denninsonDestinationFolder = new string(denninsonDestinationFolder.TrimEnd('\\').Append('\\').ToArray());
            gerberDestinationFolder = new string(gerberDestinationFolder.TrimEnd('\\').Append('\\').ToArray());

            Console.WriteLine($"From: [{sourceFolder}] To (Denninson): [{denninsonDestinationFolder}] To (Gerber): [{gerberDestinationFolder}]");

            var sw = new Stopwatch();
            sw.Start();

            ProConsole.WriteLine($"Loading source files..", ConsoleColor.Yellow);

            var sourceFiles = FileSystem.FilterFiles(sourceFolder, "*.*").ToList();

            int filesCounter = 0;
            int denninsonFileCounter = 0;
            int gerberFilesCounter = 0;

            if (DeleteDestinationFiles)
            {
                ProConsole.WriteLine($"Deleting destination files..", ConsoleColor.Yellow);

                ProcessHelper.ExecuteShellCommand($"rmdir /S /Q {denninsonDestinationFolder}");
                ProcessHelper.ExecuteShellCommand($"rmdir /S /Q {gerberDestinationFolder}");
            }

            Directory.CreateDirectory(denninsonDestinationFolder);
            Directory.CreateDirectory(gerberDestinationFolder);

            ProConsole.WriteLine($"Loading destination files..", ConsoleColor.Yellow);

            var denninsonFilesInDestination = FileSystem.FilterFiles(denninsonDestinationFolder, "*.*").ToList();
            var gerberFilesInDestination = FileSystem.FilterFiles(gerberDestinationFolder, "*.*").ToList();

            var denninsonFilesInSource = new List<string>();
            var gerberFilesInSource = new List<string>();

            Console.WriteLine($"[Info] Initialization done ({sw.Elapsed.TotalSeconds:0.0} s)");

            ProConsole.WriteLine($"Processing files..", ConsoleColor.Yellow);

            foreach (var file in sourceFiles)
            {
                filesCounter++;

                if ((filesCounter % 100) == 0)
                {
                    Console.WriteLine($"[Info] Processing file {filesCounter}/{sourceFiles.Count} {(float)filesCounter / (float)sourceFiles.Count * 100.0f:0.0}%) ...");
                }

                if (FilesFormat.IsDenninsonFiles(file))
                {
                    denninsonFilesInSource.Add(file);

                    denninsonFileCounter++;
                }
                else if (FilesFormat.IsGerberFile(file))
                {
                    gerberFilesInSource.Add(file);

                    gerberFilesCounter++;
                }
            }

            ProConsole.WriteLine($"Copying Denninson files..", ConsoleColor.Yellow);

            foreach (var file in denninsonFilesInSource)
            {
                try
                {
                    string newPath = FileSystem.ReplacePath(file, sourceFolder, denninsonDestinationFolder);

                    if (AlwaysCopyFiles || File.Exists(newPath) == false)
                    {
                        ProConsole.WriteLine($"{file} ==> {newPath}", ConsoleColor.DarkGray);

                        Directory.CreateDirectory(Path.GetDirectoryName(newPath));

                        File.Copy(file, newPath, AlwaysCopyFiles);
                    }
                }
                catch
                {
                    ProConsole.WriteLine($"[Error] Problems occured during copying file {file}", ConsoleColor.Red);
                }
            }

            ProConsole.WriteLine($"Copying Gerber files..", ConsoleColor.Yellow);

            foreach (var file in gerberFilesInSource)
            {
                try
                {
                    string newPath = FileSystem.ReplacePath(file, sourceFolder, gerberDestinationFolder);

                    if (AlwaysCopyFiles || File.Exists(newPath) == false)
                    {
                        ProConsole.WriteLine($"{file} ==> {newPath}", ConsoleColor.DarkGray);

                        Directory.CreateDirectory(Path.GetDirectoryName(newPath));

                        File.Copy(file, newPath, AlwaysCopyFiles);
                    }
                }
                catch
                {
                    ProConsole.WriteLine($"[Error] Problems occured during copying file {file}", ConsoleColor.Red);
                }
            }

            ProConsole.WriteLine($"Removing Denninson files..", ConsoleColor.Yellow);

            var denninsonFilesToRemove = denninsonFilesInDestination
                                         .Except(denninsonFilesInSource.Select(x => FileSystem.ReplacePath(x, sourceFolder, denninsonDestinationFolder)))
                                         .ToList();

            foreach (var file in denninsonFilesToRemove)
            {
                try
                {
                    if (File.Exists(file))
                    {
                        ProConsole.WriteLine($"{file}", ConsoleColor.DarkGray);

                        File.Delete(file);
                    }
                }
                catch
                {
                    ProConsole.WriteLine($"[Error] Problems occured during deleting file {file}", ConsoleColor.Red);
                }
            }


            ProConsole.WriteLine($"Removing Gerbers files..", ConsoleColor.Yellow);

            var gerberFilesToRemove = gerberFilesInDestination
                                      .Except(gerberFilesInSource.Select(x => FileSystem.ReplacePath(x, sourceFolder, gerberDestinationFolder)))
                                      .ToList();

            foreach (var file in gerberFilesToRemove)
            {
                try
                {
                    if (File.Exists(file))
                    {
                        ProConsole.WriteLine($"{file}", ConsoleColor.DarkGray);

                        File.Delete(file);
                    }
                }
                catch
                {
                    ProConsole.WriteLine($"[Error] Problems occured during deleting file {file}", ConsoleColor.Red);
                }
            }

            sw.Stop();

            int filesDiscarted = filesCounter - (denninsonFileCounter + gerberFilesCounter);

            Console.WriteLine($"\nFiles Counter:{filesCounter} Denninson Counter:{denninsonFileCounter} Gerber Counter:{gerberFilesCounter} Files discarded: {filesDiscarted}");
            Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalMinutes:0.0} min");

#if DEBUG
            ProConsole.PressKeyToContinue();
#endif
        }
    }
}
