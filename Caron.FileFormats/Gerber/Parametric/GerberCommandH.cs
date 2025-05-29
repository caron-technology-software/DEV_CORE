//MMIx16
using Caron.FileFormats.Gerber.NonParametric;

namespace Caron.FileFormats.Gerber.Parametric
{
    public class GerberCommandH : GerberInt32Command
    {
        public override string Kind => GerberCommandKind.H;

        public GerberCommandH(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"File identifier ({base.ToString()})";
        }
    }
}
