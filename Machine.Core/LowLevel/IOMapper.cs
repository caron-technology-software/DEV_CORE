using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.Control.LowLevel
{
    public class IOMapper
    {
        public Dictionary<int, int> DigitalInputs { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> DigitalOutputs { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> AnalogInputs { get; set; } = new Dictionary<int, int>();
        public IOMapper()
        {
            // --
        }
    }
}
