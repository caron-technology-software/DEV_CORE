using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob
{
    public class ApplicationInfo
    {
        private static ApplicationInfo instance;
        private static readonly Object locker = new Object();

        private static string applicationName = string.Empty;
        private static string processName = string.Empty;
        private static string coreLibs = string.Empty;
        private static DateTime startDateTime;
        private static DateTime startDateTimeUtc;

        private static ApplicationVersion applicationVersion;

        public static string ApplicationName => applicationName;
        public static string ProcessName => processName;
        public static string CoreLibs => coreLibs;

        public static DateTime StartDateTime => startDateTime;

        public static DateTime BuildDate => BuildInfo.BuildDate;
        public static string FullCommitHash => BuildInfo.CommitHash.ToUpper();
        public static string CommitHash = BuildInfo.ShortCommitHash.ToUpper();

        public static int NumberOfThreads => System.Diagnostics.Process.GetProcessesByName(ProcessName).First().Threads.Count;
        public static TimeSpan TotalProcessorTime => System.Diagnostics.Process.GetProcessesByName(ProcessName).First().TotalProcessorTime;
        public static double AverageCpuUtilization => System.Diagnostics.Process.GetProcessesByName(ProcessName).First().TotalProcessorTime.TotalMilliseconds / (double)TotalMillisecondsApplicationUptime * 100.0;

        public static int TotalMillisecondsApplicationUptime => (int)(DateTime.UtcNow - startDateTimeUtc).TotalMilliseconds;
        public static ApplicationVersion ApplicationVersion => applicationVersion;

        static ApplicationInfo()
        {
            var applicationInfo = new ApplicationInfo();

            processName = System.AppDomain.CurrentDomain.FriendlyName;
            processName = (string)processName.Substring(0, processName.Length - 4);

            startDateTime = DateTime.Now;
            startDateTimeUtc = DateTime.UtcNow;

            coreLibs = typeof(String).Assembly.GetName().Version.ToString();
        }

        private ApplicationInfo()
        {
            //--
        }

        public static void SetApplicationName(string name)
        {
            applicationName = name;
        }

        public static void SetApplicationVersion(ApplicationVersion version)
        {
            applicationVersion = version;
        }

        public static ApplicationInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ApplicationInfo();
                        }
                    }
                }

                return instance;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Application: {ApplicationName}");
            sb.AppendLine($"Version: {ApplicationVersion}");
            sb.AppendLine($"Build date: {BuildInfo.BuildDate}");
            sb.AppendLine($"Commit: {BuildInfo.CommitHash}");
            sb.AppendLine($"Process: {ProcessName}");
            sb.AppendLine($"Core libs: {CoreLibs}");
            //sb.AppendLine($"Curent user: {Environment.UserName}");

            return sb.ToString();
        }
    }
}
