using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.SoftwareUpdate.Arguments
{
    public class UpdateArgs
    {
        public string PathRootFolder { get; set; }
        public string MachineFolderName { get; set; }
        public string BinUtilityFolderName { get; set; }
        public string UpdateFolderName { get; set; }
        public string BackupFolderName { get; set; }
        public string UpdateBundleFile { get; set; }
        public string Password { get; set; }
    }
}
