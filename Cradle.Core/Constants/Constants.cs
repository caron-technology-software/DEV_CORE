using System;

using ProRob;

namespace Caron.Cradle
{
    public static partial class Constants
    {
        public static readonly MachineModel MachineModel = MachineModel.Cradle;
        public static readonly string MachineShortName = Enum.GetName(typeof(MachineModel), Constants.MachineModel);
        public static readonly string MachineFullName = "CARON Cradle York";

        public static class Path
        {
            public static readonly string RootFolder = Caron.Constants.Path.RootFolder;
            public static readonly string MachineFolder = System.IO.Path.Combine(RootFolder, $"machine_{MachineShortName.ToLower()}");
            public static readonly string AssetsFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.Assets);
            public static readonly string DatabaseFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.DatabaseFolder);
            public static readonly string BinFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.BinFolder);
            public static readonly string LowLevelControlBinFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.LowLevelControlBinFolder);
            public static readonly string SettingsIOFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.SettingsIOFolder);
            public static readonly string LogsFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.LogsFolder);
            public static readonly string SettingsFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.SettingsFolder);
            public static readonly string SettingsDefaultFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.SettingsDefaultFolder);
            public static readonly string BinUtilityFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.BinUtilityFolder);
            public static readonly string BinServerFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.BinServerFolder);
            public static readonly string UpdateFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.UpdateFolder);
            public static readonly string LocalizationsFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.LocalizationsFolder);
            public static readonly string WwwFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.WwwFolder);
            public static readonly string BackupsFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.BackupsFolder);
            public static readonly string TmpFolder = System.IO.Path.Combine(MachineFolder, Machine.Constants.FolderName.TmpFolder);

            public static class Log
            {
                public static readonly string UILogFile = System.IO.Path.Combine(LogsFolder, $"log_ui.db");
                public static readonly string ControlLogFile = System.IO.Path.Combine(LogsFolder, $"log_control.db");
                public static readonly string LowLevelControlLogFile = System.IO.Path.Combine(LogsFolder, $"log_low_level_control.db");
                public static readonly string UILogFilename = $"log_ui.db";
                public static readonly string ControlLogFilename = $"log_control.db";
                public static readonly string LowLevelControlLogFilename = $"log_low_level_control.db";
            }

            public static class LowLevelControl
            {
                public static readonly string LowLevelControlFilename = "CradleControl.exe";
                public static readonly string LowLevelControlFile = System.IO.Path.Combine(LowLevelControlBinFolder, LowLevelControlFilename);

                public static readonly string DigitalInputsMapFilename = "cradle_di_map.txt";
                public static readonly string DigitalInputsTypeMapFilename = "cradle_di_type_map.txt";
                //public static readonly string LowLewHighLevMapFilename = "cradle_di_LowLewHighLev_map.txt";
                public static readonly string DigitalInputsMapFile = System.IO.Path.Combine(SettingsIOFolder, DigitalInputsMapFilename);
                public static readonly string DigitalInputsTypeMapFile = System.IO.Path.Combine(SettingsIOFolder, DigitalInputsTypeMapFilename);
                //public static readonly string LowLewHighLevMapFile = System.IO.Path.Combine(SettingsIOFolder, LowLewHighLevMapFilename);

                public static readonly string DigitalOutputsMapFilename = "cradle_do_map.txt";
                public static readonly string DigitalOutputsMapFile = System.IO.Path.Combine(SettingsIOFolder, DigitalOutputsMapFilename);
                //public static readonly string DoLowLewHighLevMapFilename = "cradle_do_LowLewHighLev_map.txt";
                //public static readonly string DoLowLewHighLevMapFile = System.IO.Path.Combine(SettingsIOFolder, DoLowLewHighLevMapFilename);

                public static readonly string AnalogInputsMapFilename = "cradle_ai_map.txt";
                public static readonly string AnalogInputsMapFile = System.IO.Path.Combine(SettingsIOFolder, AnalogInputsMapFilename);
                //public static readonly string AnLowLewHighLevMapFilename = "cradle_analog_LowLewHighLev_map.txt";
                //public static readonly string AnLowLewHighLevMapFile = System.IO.Path.Combine(SettingsIOFolder, AnLowLewHighLevMapFilename);

                public static readonly string BlackListIOFilename = "blacklist_io.json";
                public static readonly string BlackListIOFile = System.IO.Path.Combine(SettingsIOFolder, BlackListIOFilename);

            }

            public static class Database
            {
                public static readonly string DatabaseWorkingsFile = System.IO.Path.Combine(DatabaseFolder, "db_workings.db");
            }

            public static class Data
            {
                public static readonly string LocalizationsFile = System.IO.Path.Combine(LocalizationsFolder, $"{MachineShortName.ToLower()}_localizations.json");
            }

            public static class Settings
            {
                public static readonly string MachineConfigurationFile = System.IO.Path.Combine(SettingsFolder, $"{MachineShortName.ToLower()}_machine_configuration.db");
                public static readonly string SettingsFile = System.IO.Path.Combine(SettingsFolder, $"{MachineShortName.ToLower()}_settings.db");
                public static readonly string LowLevelMotionSettingsFile = System.IO.Path.Combine(SettingsFolder, $"{MachineShortName.ToLower()}_low_level_motion_settings.db");
                public static readonly string WorkingsSettingsFile = System.IO.Path.Combine(SettingsFolder, $"{MachineShortName.ToLower()}_workings_settings.db");
                public static readonly string UISettingsFile = System.IO.Path.Combine(SettingsFolder, $"{MachineShortName.ToLower()}_ui_settings.db");
                public static readonly string WorkingsStatisticsFile = System.IO.Path.Combine(SettingsFolder, $"{MachineShortName.ToLower()}_workings_statistics.db");
                public static readonly string DefaultWorkingsSettingsFile = System.IO.Path.Combine(SettingsFolder, $"{MachineShortName.ToLower()}_workings_settings_DEFAULT.db");
                public static readonly string WorkingContextFile = System.IO.Path.Combine(SettingsFolder, $"{MachineShortName.ToLower()}_working_context.db");
                public static readonly string Localization = System.IO.Path.Combine(SettingsFolder, $"{MachineShortName.ToLower()}_localization_settings.db");
                public static readonly string MachineEndurance = System.IO.Path.Combine(SettingsFolder, $"{MachineShortName.ToLower()}_machine_endurance.db");
                public static readonly string DefaultSettingsFile = System.IO.Path.Combine(SettingsDefaultFolder, "cradle_settings.json");
            }
        }

        public static class Errors
        {
            public static readonly TimeSpan TimeSpanFromLastDataPacketReceived = TimeSpan.FromMilliseconds(3500);
        }

        public static class Hardware
        {
            public static class IO
            {
                public const int NumberOfDigitalInputs = 20;
                public const int NumberOfDigitalOutputs = 20;
                public const int NumberOfAnalogInputs = 4;
                //GPIx21
                //change//public const int NumberOfVirtualInputs = 10;
                public const int NumberOfVirtualInputs = 9;
                //GPFx21
                public const int NumberOfMachineInputs = 2;
            }
        }
    }
}