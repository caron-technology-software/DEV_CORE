using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.FileFormats.Denninson
{
    public static partial class DenninsonConverter
    {
        internal class MarkerInfo
        {
            public int MarkerLength { get; set; }
            public string MarkerId { get; set; }
            public string MarkerFilename { get; set; }

            public MarkerInfo()
            {
                // --
            }

            public override string ToString()
            {
                return $"MarkerLength:{MarkerLength} MarkerId: {MarkerId} MarkerFilename: {MarkerFilename}";
            }
        }

        internal class SectionRange
        {
            public int SectionFrom { get; set; }
            public int SectionTo { get; set; }

            public SectionRange()
            {
                // --
            }

            public override string ToString()
            {
                return $"SectionFrom:{SectionFrom} SectionTo: {SectionTo}";
            }
        }
    }
}