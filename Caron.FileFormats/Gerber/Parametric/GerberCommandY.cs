//MMIx16
using Caron.FileFormats.Gerber.NonParametric;

namespace Caron.FileFormats.Gerber.Parametric
{
    public class GerberCommandY : GerberCoordinateCommand
    {
        public override string Kind => GerberCommandKind.Y;

        public GerberCommandY(string token) : base(token)
        {

        }

        public override string ToString()
        {
            return $"Before Y ({base.ToString()})";
        }
    }
}
