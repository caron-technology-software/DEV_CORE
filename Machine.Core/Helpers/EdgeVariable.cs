using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Machine.Common
{
    public class EdgeVariable
    {
        public bool Initialized { get; private set; } = false;
        public bool Value { get; private set; } = false;
        public bool PrecedentValue { get; private set; } = false;
        public bool Triggered { get; private set; } = false;
        public bool FallingEdge { get; private set; } = false;
        public bool RisingEdge { get; private set; } = false;

        public int NumberOfFallingEdges { get; private set; } = 0;
        public int NumberOfRisingEdges { get; private set; } = 0;

        public EdgeVariable()
        {
            //--
        }

        public void Update(bool value)
        {
            if (!Initialized)
            {
                Initialized = true;
                Value = value;
            }

            PrecedentValue = Value;
            Value = value;

            Triggered = Value != PrecedentValue;
            FallingEdge = !Value && PrecedentValue;
            RisingEdge = Value && !PrecedentValue;

            if (FallingEdge)
            {
                NumberOfFallingEdges++;
            }

            if (RisingEdge)
            {
                NumberOfRisingEdges++;
            }
        }

        public override string ToString()
        {
            return $"[EdgeVariable] Value: {Value} RisingEdge:{RisingEdge} FallingEdge:{FallingEdge}";
        }
    }
}
