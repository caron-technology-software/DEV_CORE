using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.SoftwareUpdate
{
    //  7za.exe
    // -mx0 = Don't compress at all - just copy the contents to archive.
    // -mx1 = Consumes least time, but compression is low.
    // -mx3 = Better than -mx1.
    // -mx5 = This is default (compression is normal).
    // -mx7 = Maximum compression.
    // -mx9 = Ultra compression.

    internal enum CompressionLevel
    {
        Archive = 0,
        VeryLow = 1,
        Low = 3,
        Normal = 5,
        Maximum = 7,
        Ultra = 9
    }
}
