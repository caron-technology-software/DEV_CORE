//MMIx16
using Caron.FileFormats.Gerber.NonParametric;

namespace Caron.FileFormats.Gerber.Parametric
{
    public class GerberCommandZ : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.Z;
        public GerberCommandZ(string token) : base(token) { }

        public override string ToString()
        {
            return $"Byte size ({base.ToString()})";
        }
    }
}
