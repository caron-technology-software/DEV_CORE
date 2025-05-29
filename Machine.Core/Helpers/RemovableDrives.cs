using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine
{
    public class RemovableDrives
    {
        public static IEnumerable<DriveInfo> GetRemovablesDrives()
        {
            var removableDrivers = DriveInfo.GetDrives()
                .Where(x => x.DriveType == DriveType.Removable &&
                x.Name != "C:\\" &&
                x.Name != "B:\\" &&
                x.Name != "X:\\" &&
                x.Name != "Y:\\" &&
                x.Name != "Z:\\");

            return removableDrivers;
        }

        public static string GetLetterDrive()
        {
            return GetRemovablesDrives().First().RootDirectory.Name;
        }

        public static bool IsPresentRemovableDevice()
        {
            return GetRemovablesDrives().Count() > 0;
        }
    }
}
