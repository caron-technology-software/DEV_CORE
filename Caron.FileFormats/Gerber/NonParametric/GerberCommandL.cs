//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandL : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.L;

        public GerberCommandL(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Slow down ({base.ToString()})";
        }
    }
}
