using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Machine
{
    public class LocalizationData
    {
        public string[] Languages { get; set; }
        public List<Dictionary<string, string>> Dictionary { get; set; }
    }
}