using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.SoftwareUpdate
{
    public class Manifest
    {
        public string Folder { get; set; }
        public string Signature { get; set; }
        public List<FileHash> FileHashes { get; set; }
    }
}
