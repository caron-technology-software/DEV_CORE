using System;

namespace Machine
{
    public static class Constants
    {
        public static class Path
        {
            public const string TmpFolder = @"C:\CARON\tmp";
        }

        public static class FolderName
        {
            public const string DatabaseFolder = "db";
            public const string BinFolder = "bin";
            public const string BinServerFolder = "bin_server";
            public const string BinUtilityFolder = "bin_utility";
            public const string LowLevelControlBinFolder = "bin_low_level_control";
            public const string LogsFolder = "logs";
            public const string ScriptsFolder = "scripts";
            public const string ScriptsUserFolder = "scripts_user";
            public const string SettingsFolder = "settings";
            public const string SettingsIOFolder = "settings_io";
            public const string LocalizationsFolder = "localizations";
            public const string WwwFolder = "www";
            public const string BackupsFolder = "backups";
            public const string UpdateFolder = "update";
            public const string TmpFolder = "tmp";
            //GPIx237
            public const string UsersFolder = "users";
            //GPFx237
            public const string Assets = "assets";
            public const string SettingsPathFolder = "settings_path";
            public const string SettingsDefaultFolder = "settings_default";          
        }

        public static class Networking
        {
            public const string IPAddressLowLevelControl = "10.0.0.1";

#if TEST
            public static readonly string IPAddressHighLevelControl = ProRob.Networking.Network.GetLocalIPAddress();

#elif DEBUG
            public const string IPAddressHighLevelControl = "10.0.0.3";
#else
            public const string IPAddressHighLevelControl = "10.0.0.2";
#endif
            public const int WebApiPort = 9001;
            public const int WebApiProxyPort = 9010;
            public const int WebApiHttpsProxyPort = 9011;

            public const int LowLevelControlTcpPort = 5000;
            public const int LowLevelControlLoggerUdpPort = 5002;
            public const int LowLevelControlMachineManagerUdpPort = 5003;
            public const int LowLevelControlUdpPort = 10000;

            public static readonly string WebApiUri = $@"http://{IPAddressHighLevelControl}:{WebApiPort}/";

            public static readonly string WebApiProxyUri = $@"http://{IPAddressHighLevelControl}:{WebApiProxyPort}/";
            public static readonly string WebApiProxyUriHttps = $@"http://{IPAddressHighLevelControl}:{WebApiHttpsProxyPort}/";

        }
        public static class Maintenance
        {
            public const int NumberOfDaysToKeepLogs = 14;
        }

        public static class Timeouts
        {
            public static readonly TimeSpan HighLevelControlCommunication = TimeSpan.FromMilliseconds(1000);
            public static readonly TimeSpan LowLevelTimeoutCommunication = TimeSpan.FromMilliseconds(250);
        }

        public static class Intervals
        {
            public static readonly TimeSpan Heartbeat = TimeSpan.FromMilliseconds(500);
            public static readonly TimeSpan HighLevelControlStatusUpdate = TimeSpan.FromMilliseconds(3);
            public static readonly TimeSpan HighLevelControlCycle = TimeSpan.FromMilliseconds(1);

            public static readonly TimeSpan WaitDevice = TimeSpan.FromMilliseconds(100);
            public static readonly TimeSpan WaitTask = TimeSpan.FromMilliseconds(1000);

            public static readonly TimeSpan DebugSleep = TimeSpan.FromMilliseconds(100);

            public static readonly TimeSpan SlowWaitSleep = TimeSpan.FromMilliseconds(100);
            public static readonly TimeSpan LongWaitSleep = TimeSpan.FromMilliseconds(250);

            public static readonly TimeSpan FireSignalWindow = TimeSpan.FromMilliseconds(200);
            public static readonly TimeSpan WaitUIComponent = TimeSpan.FromMilliseconds(10);

            public static readonly TimeSpan TaskPause = TimeSpan.FromMilliseconds(100);
            public static readonly TimeSpan TaskDispose = TimeSpan.FromMilliseconds(100);

            public static readonly TimeSpan WaitAfterUploadFiles = TimeSpan.FromMilliseconds(1000);
            public static readonly TimeSpan WaitAfterDeleteFiles = TimeSpan.FromMilliseconds(1000);
            public static readonly TimeSpan WaitAfterLowLevelMachineManagerHelloCommand = TimeSpan.FromMilliseconds(2000);
            public static readonly TimeSpan WaitAfterStartLowLevelControlCommand = TimeSpan.FromMilliseconds(5000);
            public static readonly TimeSpan WaitAfterLowLevelControlShutdownCommand = TimeSpan.FromSeconds(15);

            public static readonly TimeSpan WaitAfterExitSignal = TimeSpan.FromMilliseconds(4000);
            public static readonly TimeSpan WaitSignal = TimeSpan.FromMilliseconds(100);
            public static readonly TimeSpan WaitStartTrajectoryMode = TimeSpan.FromMilliseconds(500);

            public static class Plots
            {
                public static readonly TimeSpan FastRefreshRate = TimeSpan.FromMilliseconds(100);
                public static readonly TimeSpan SlowRefreshRate = TimeSpan.FromMilliseconds(250);
            }

            public static class DataCollections
            {
                public static readonly TimeSpan RefreshRate = TimeSpan.FromMilliseconds(100);
                public static readonly TimeSpan SamplingWindow = TimeSpan.FromMinutes(5);
                public static readonly TimeSpan LastPeriodSamplingWindow = TimeSpan.FromSeconds(30);
            }
        }

        public static class Kinematics
        {
            public const int MinTrajectoryLength = 10; //[mm]
            public const int DeltaPosition = 3; //[mm]
            //
            public const float MaxVelocityToConsiderDeviceStationary = 15.0f; //[mm/s]
            public const float MinVelocityToConsiderDeviceInMovement = 12.50f; //[mm/s]
            //GPIx136
            public const int ItersAtZeroVelocityToConsiderStationary = 50; //50;
            public const int ItersAtZeroPositionToConsiderStationary = 60; //60;
            //GPFx136
        }
    }
}
