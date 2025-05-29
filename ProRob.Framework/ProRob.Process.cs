using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using ProRob.Extensions.Collections;

namespace ProRob
{
    //TODO refactoring
    public static class ProcessHelper
    {
        private static string RemoveProcessExtension(string process)
        {
            if (process.Substring(process.Length - 4) == ".exe")
            {
                process = process.Substring(0, process.Length - 4);
            }

            return process;
        }

        public static bool IsProcessRunning(string process)
        {
            int count = Process.GetProcessesByName(RemoveProcessExtension(process)).Count();

            return count > 0;
        }


        public static void CloseKillProcess(string process, TimeSpan maxIntervalToCloseProcess)
        {
            if (IsProcessRunning(process) == false)
            {
                Console.WriteLine($"Process {process} : NOT RUNNING");
                return;
            }

            CloseProcess(process);

            DateTime start = DateTime.UtcNow;

            while (DateTime.UtcNow < start + maxIntervalToCloseProcess)
            {
                if (IsProcessRunning(process) == false)
                {
                    Console.WriteLine($"CloseProcess({process}) : YES");
                    return;
                }

                Thread.Sleep(100);
            }

            Console.WriteLine($"CloseProcess({process}) : NO");

            KillProcess(process);

            if (IsProcessRunning(process))
            {
                Console.WriteLine($"KillProcess({process}) : NO");
            }
            else
            {
                Console.WriteLine($"KillProcess({process}) : YES");
            }
        }

        public static void CloseProcess(string process)
        {
            process = RemoveProcessExtension(process);

            var processesToClose = new List<Process>();
            processesToClose.AddRange(Process.GetProcessesByName(process));

            int id = Process.GetCurrentProcess().Id;

            foreach (var p in processesToClose)
            {
                if (p.Id != id)
                {
                    p.CloseMainWindow();
                }
            }
        }

        public static void KillProcess(string process)
        {
            process = RemoveProcessExtension(process);

            var processesToKill = new List<Process>();
            processesToKill.AddRange(Process.GetProcessesByName(process));

            int id = Process.GetCurrentProcess().Id;

            foreach (var p in processesToKill)
            {
                if (p.Id != id)
                {
                    p.Kill();
                }
            }
        }

        public static (string[], int) ExecuteShellCommand(string command, bool consoleOutput = false, Action<string> standardOutputRedirect = null)
        {
            var cmdOutput = new List<string>();

            object locker = new object();

            string fileName = string.Empty;
            string workingDirectory = string.Empty;
            string arguments = string.Empty;

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    fileName = @"C:\Windows\System32\cmd.exe";
                    arguments = $"/c {command}";
                    workingDirectory = @"C:\Windows\System32";
                    break;

                case PlatformID.Unix:
                    fileName = "/bin/bash";
                    arguments = $"-c \"{command.Replace("\"", "\\\"")}\"";
                    workingDirectory = @"/bin/";
                    break;

                default:
                    throw new NotImplementedException();
            }

            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    WorkingDirectory = workingDirectory,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };

            process.OutputDataReceived += StandardOutputErrorDataReceived;
            process.ErrorDataReceived += StandardOutputErrorDataReceived;

            if (!(standardOutputRedirect is null))
            {
                process.OutputDataReceived += (o, e) => standardOutputRedirect(e.Data);
                process.ErrorDataReceived += (o, e) => standardOutputRedirect(e.Data);
            }


            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            int exitCode = process.ExitCode;
            process.Close();
            process.Dispose();

            return (cmdOutput.ToArray(), exitCode);

            void StandardOutputErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
            {
                if (e.Data is null)
                {
                    return;
                }

                lock (locker)
                {
                    cmdOutput.Add(e.Data);
                }

                if (consoleOutput)
                {
                    Console.WriteLine($"{e.Data}");
                }
            }
        }

        public static (string[], int) Execute(string processName, string arguments = null, bool verbose = true, string username = "")
        {
            object locker = new object();

            var cmdOutput = new List<string>();

            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = processName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UserName = username
                }
            };

            process.OutputDataReceived += StandardOutputErrorDataReceived;
            process.ErrorDataReceived += StandardOutputErrorDataReceived;

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            int exitCode = process.ExitCode;
            process.Close();
            process.Dispose();

            return (cmdOutput.ToArray(), exitCode);

            void StandardOutputErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
            {
                if (e.Data is null)
                {
                    return;
                }

                lock (locker)
                {
                    cmdOutput.Add(e.Data);
                }

                if (verbose)
                {
                    Console.WriteLine($"{e.Data}");
                }
            }
        }

        public static void ExecuteWithNoRedirect(string filename, string arguments = null, bool waitForExit = false)
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filename,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            if (waitForExit)
            {
                process.WaitForExit();
                process.Close();
                process.Dispose();
            }
        }
    }
}
