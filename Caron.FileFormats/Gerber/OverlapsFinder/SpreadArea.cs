using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//MMIx50
namespace Caron.FileFormats.Gerber
{
    public class SpreadArea
    {
        public virtual ICollection<string> GeometriesIds { get; set; }

        public List<int> GeometryIds { get; set; } = new List<int>();//test

        public List<(double MinX, double MaxX, double MinY, double MaxY)> Geometries = new List<(double MinX, double MaxX, double MinY, double MaxY)>();
        
        public decimal Left { get; set; }

        public decimal Right { get; set; }
    }
}
