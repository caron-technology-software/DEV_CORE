using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProRob.Extensions.Collections;

namespace Machine.SoftwareUpdate
{
    public class FileHash : IComparer, IComparable
    {
        public string Path { get; set; }
        public string Hash { get; set; }

        public int CompareTo(object obj)
        {
            return Path.CompareTo((obj as FileHash).Path);
        }

        int IComparer.Compare(object x, object y)
        {
            return ((new CaseInsensitiveComparer()).Compare((x as FileHash).Path, (y as FileHash).Path));
        }

        public override string ToString()
        {
            return $"Path: {Path} Hash: {string.Concat(Hash.Take(8))}...{string.Concat(Hash.TakeLast(8))}";
        }
    }
}
