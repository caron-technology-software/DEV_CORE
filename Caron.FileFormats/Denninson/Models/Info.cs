using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.FileFormats.Denninson
{
    public class Info
    {
        public SpreadMode SpreadMode { get; set; }
        public int ParameterSet { get; set; }
        public int SpreadLength { get; set; }
        public int NumberOfMarkers { get; set; }
        public int FrontAllowance { get; set; }
        public int RearAllowance { get; set; }
        public List<string> GerberFiles { get; set; } = new List<string>();
        public int NumberOfGerberFiles { get => GerberFiles.Count; }
        public List<string> Colors { get; set; } = new List<string>();
        public int NumberOfColors { get => Colors.Count; }
        public Info()
        {
            SpreadMode = SpreadMode.Undefined;
            ParameterSet = -1;
            SpreadLength = -1;
        }

        public override string ToString()
        {
            return $"SpreadMode: {SpreadMode} ParameterSet: {ParameterSet} SpreadLength: {SpreadLength} NumberOfColors: {NumberOfColors} FrontAllowance: {FrontAllowance} RearAllowance: {RearAllowance}";
        }
    }
}
