using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.HighLevel
{
    public class DailyWorkingsStatistics
    {
        public DateTime Day { get; set; }
        public double TotalMaterialsSpread { get; set; }
        public double TotalTimeCounter { get; set; }
        public double TotalCradleInSyncTimeCounter { get; set; }
        public double TotalTimeLoadUnloadCounter { get; set; }
        public int DistinctMaterials { get; set; }
        public int DistinctMaterialsCode { get; set; }
        public int NumberOfWorkings { get; set; }

        public DailyWorkingsStatistics()
        {
            //--
        }
    }

    public class MaterialWorkingsStatistics
    {
        public string Material { get; set; }
        public double TotalMaterialsSpread { get; set; }
        public double TotalTimeCounter { get; set; }
        public double TotalCradleInSyncTimeCounter { get; set; }
        public double TotalTimeLoadUnloadCounter { get; set; }

        public int DistinctMaterials { get; set; }
        public int DistinctMaterialsCode { get; set; }
        public int NumberOfWorkings { get; set; }

        public MaterialWorkingsStatistics()
        {
            //--
        }
    }

    public class MaterialCodeWorkingsStatistics
    {
        public string MaterialCode { get; set; }
        public double TotalMaterialsSpread { get; set; }
        public double TotalTimeCounter { get; set; }
        public double TotalCradleInSyncTimeCounter { get; set; }
        public double TotalTimeLoadUnloadCounter { get; set; }

        public int DistinctMaterials { get; set; }
        public int DistinctMaterialsCode { get; set; }
        public int NumberOfWorkings { get; set; }

        public MaterialCodeWorkingsStatistics()
        {
            //--
        }
    }
}
