using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Machine;
using Machine.Utility;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.Control.Database
{
    public class DatabaseSettings
    {
        public static void Update(LocalizationSettings settings)
        {
            Console.WriteLine("Updating localization settings..");
            MachineData.Save<LocalizationSettings>(Constants.Path.Settings.Localization, settings);
        }
        public static void Update(UISettings settings)
        {
            Console.WriteLine("Updating UI settings..");
            MachineData.Save<UISettings>(Constants.Path.Settings.UISettingsFile, settings);
        }

        public static void Update(MachineConfiguration configuration)
        {
            Console.WriteLine("Updating machine configuration settings..");
            MachineData.Save<MachineConfiguration>(Constants.Path.Settings.MachineConfigurationFile, configuration);
        }

        public static void Update(WorkingContext context)
        {
            Console.WriteLine("Updating working context..");
            MachineData.Save<WorkingContext>(Constants.Path.Settings.WorkingContextFile, context);
        }

        public static void Update(Working statistics)
        {
            Console.WriteLine("Updating workings statistics..");
            MachineData.Save<Working>(Constants.Path.Settings.WorkingsStatisticsFile, statistics);
        }

        public static void Update(HighLevelSettings settings)
        {
            Console.WriteLine("Updating root settings..");
            MachineData.Save<HighLevelSettings>(Constants.Path.Settings.SettingsFile, settings);
        }

        public static void Update(LowLevelMotionSettings settings)
        {
            Console.WriteLine("Updating low level motion settings..");
            MachineData.Save<LowLevelMotionSettings>(Constants.Path.Settings.LowLevelMotionSettingsFile, settings);
        }

        public static void Update(MachineEndurance machineEndurance)
        {
            Console.WriteLine("Updating machine endurance..");
            MachineData.Save<MachineEndurance>(Constants.Path.Settings.MachineEndurance, machineEndurance);
        }

        public static void Update(WorkingsSettings settings)
        {
            Console.WriteLine("Updating workings settings..");
            MachineData.Save<WorkingsSettings>(Constants.Path.Settings.WorkingsSettingsFile, settings);
        }

        public static void Close()
        {
            Thread.Sleep(50);

            MachineData.WaitAllOperations();

            Thread.Sleep(50);
        }
    }
}
