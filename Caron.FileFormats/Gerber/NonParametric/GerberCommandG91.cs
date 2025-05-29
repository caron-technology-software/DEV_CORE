//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandG91 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.G91;

        public GerberCommandG91(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Gerber ({base.ToString()})";
        }
    }
}
