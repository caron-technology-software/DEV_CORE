//MMIx18
using System.Collections.Generic;
using System.ComponentModel;

namespace Caron.FileFormats.Gerber
{
    public class GerberFile
    {
        public GerberMeasurementSystem UnitOfMeasureSystem { get; set; }
        public List<GerberBlock> Blocks { get; set; } = new List<GerberBlock>();

        public override string ToString()
        {
            return $"{Blocks?.Count ?? 0}, {UnitOfMeasureSystem}";
        }
    }
    public enum GerberMeasurementSystem
    {
        /// <summary>
        /// Format 5.1 = 1/10 Millimeter defined with: N1*G71*
        /// </summary>
        [Description("Format 5.1 = 1/10 Millimeter defined with: N1*G71*")]
        Metric_1_10_Millimeters = 0,

        /// <summary>
        /// Format 3.3 = 1/1000 Inch defined with: N1*G70*
        /// </summary>
        [Description("Format 3.3 = 1/1000 Inch defined with: N1*G70*")]
        Imperial_1_1000_Inch = 1,

        /// <summary>
        /// Format 4.2 = 1/100 Inch (Standard Format)
        /// </summary>
        [Description("Format 4.2 = 1/100 Inch (Standard Format)")]
        Imperial_1_100_Inch = 2
    }
}
