using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ProRob
{
    public static class Health
    {
        public static bool[] CheckHardDisksStatus()
        {
            var retCmd = ProcessHelper.Execute(@"C:\Windows\System32\wbem\wmic", "diskdrive get Status").Item1;

            //Eliminazione righe nulle
            retCmd = retCmd.Where(x => !(String.IsNullOrEmpty(x))).ToArray();

            int numberOfDisks = retCmd.Length - 1;

            bool[] ret = new bool[numberOfDisks];

            for (int i = 0; i < numberOfDisks; i++)
            {
                ret[i] = retCmd[i + 1].Replace(" ", "").SequenceEqual("OK");
            }

            return ret;
        }

    }
}
