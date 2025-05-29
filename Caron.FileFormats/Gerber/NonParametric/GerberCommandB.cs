//MMIx18

namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandB : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.B;

        public GerberCommandB(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Knife down ({base.ToString()})";
        }
    }
}