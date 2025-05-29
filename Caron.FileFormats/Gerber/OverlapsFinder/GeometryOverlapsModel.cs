using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//MMIx50
namespace Caron.FileFormats.Gerber.OverlapFinder
{
    public class GeometryOverlapsModel
    {
        public ICollection<GeometryOverlap>? Overlaps { get; set; }

        public override string ToString()
        {
            var count = Overlaps?.Count;
            if (count > 0)
                return $"{count} overlaps";

            return "No overlaps";
        }
    }
}
