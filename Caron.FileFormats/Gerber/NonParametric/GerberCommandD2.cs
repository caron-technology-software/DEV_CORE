//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandD2 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.D2;

        public GerberCommandD2(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Pen up ({base.ToString()})";
        }
    }
}
