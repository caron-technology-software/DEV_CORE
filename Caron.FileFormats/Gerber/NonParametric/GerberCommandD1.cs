//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandD1 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.D1;

        public GerberCommandD1(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Pen down ({base.ToString()})";
        }
    }
}
