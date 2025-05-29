//MMIx16
using Caron.FileFormats.Gerber.NonParametric;

namespace Caron.FileFormats.Gerber.Parametric
{
    public class GerberCommandX : GerberCoordinateCommand
    {
        public override string Kind => GerberCommandKind.X;

        public GerberCommandX(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Before X ({base.ToString()})";
        }
    }
}
