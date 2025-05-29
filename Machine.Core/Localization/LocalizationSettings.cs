using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using ProRob.Documents;
using ProRob.Extensions;

namespace Machine
{

    public class LocalizationSettings
    {
        public MachineLanguage MachineLanguage { get; set; }

        public LocalizationSettings()
        {
            //--
        }
    }
}