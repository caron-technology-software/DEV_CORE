using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//MMIx50
namespace Caron.FileFormats.Gerber.OverlapFinder
{
    public class GeometryOverlap
    {
        public string Id { get; set; }

        public decimal CutPoint { get; set; }

        public decimal SpreadPoint { get; set; }       
    }
}
