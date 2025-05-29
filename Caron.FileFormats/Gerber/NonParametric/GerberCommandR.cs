//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandR : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.R;

        public GerberCommandR(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Drill ({base.ToString()})";
        }
    }
}
