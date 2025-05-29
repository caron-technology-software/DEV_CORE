//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandSlash : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.Slash;

        public GerberCommandSlash(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Block delete ({base.ToString()})";
        }
    }
}
