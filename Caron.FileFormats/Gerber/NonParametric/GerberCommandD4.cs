//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandD4 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.D4;

        public GerberCommandD4(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Light source ({base.ToString()})";
        }
    }
}
