//MMIx16
using Caron.FileFormats.Gerber.NonParametric;

namespace Caron.FileFormats.Gerber.Parametric
{
    public class GerberCommandN : GerberInt32Command
    {
        public override string Kind => GerberCommandKind.N;

        public GerberCommandN(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Sequence number ({base.ToString()})";
        }
    }
}
