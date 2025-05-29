using System;
using System.Threading;
using System.Windows.Forms;

namespace Machine
{
    public class ApplicationManager
    {
        private const int MillisecondsTimeout = 10;
        private const int MaxIter = 30;

        public static ProcessStatus CheckIfProcessIsAlreadyRunning(string processName, bool showMessageBox = true)
        {
            int nChecks = 0;

            while (System.Diagnostics.Process.GetProcessesByName(processName).Length > 1)
            {
                nChecks++;

                if (nChecks > MaxIter)
                {
                    if (showMessageBox)
                    {
                        var threads = System.Diagnostics.Process.GetProcessesByName(processName);
                        foreach (var t in threads)
                        {
                            Console.WriteLine($"t: {t.Id}");
                        }

                        MessageBox.Show(new Form { TopMost = true }, $"{processName} is already running", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    return ProcessStatus.Running;
                }

                Thread.Sleep(MillisecondsTimeout);
            }

            return ProcessStatus.NotInExecution;
        }

        private volatile ApplicationStatus status;
        public ApplicationStatus Status { get => status; private set => status = value; }

        public bool IsRestarting { get => Status == ApplicationStatus.Restarting; }
        public bool IsRunning { get => Status == ApplicationStatus.Running; }
        public bool IsStopped { get => Status == ApplicationStatus.Stopped; }
        public bool IsShutdowing { get => Status == ApplicationStatus.Shutdowing; }
        public bool IsRebooting { get => Status == ApplicationStatus.Rebooting; }

        public ApplicationManager()
        {
            Status = ApplicationStatus.Initializing;
        }

        public void Start()
        {
            Status = ApplicationStatus.Running;
        }

        public void Stop()
        {
            Status = ApplicationStatus.Stopped;
        }

        public void Shutdown()
        {
            Status = ApplicationStatus.Shutdowing;
        }

        public void Reboot()
        {
            Status = ApplicationStatus.Rebooting;
        }

        public void Restart()
        {
            Status = ApplicationStatus.Restarting;
        }
    }
}