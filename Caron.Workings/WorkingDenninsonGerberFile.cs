using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Caron.Workings
{
    public struct WorkingDenninsonGerberFile
    {
        public string name;
        public FileInfo fileInfo;
        public string hash;
        public string barcode;
        public List<FileInfo> filesInfoGerber;

        public WorkingDenninsonGerberFile(FileInfo fileInfo, string hash, string barcode, List<FileInfo> filesInfoGerber)
        {
            this.name = fileInfo.Name;
            this.fileInfo = fileInfo;
            this.hash = hash;
            this.barcode = barcode;
            this.filesInfoGerber = filesInfoGerber;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"File Denninson: {fileInfo.Name}\nHash: {hash}\nBarcode: {barcode}\n\nFiles Gerber:");
            foreach (var f in filesInfoGerber)
            {
                sb.AppendLine($"-> {f.Name}");
            }
            return sb.ToString();
        }
    }
}