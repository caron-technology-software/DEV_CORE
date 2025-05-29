//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandG71 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.G71;

        public GerberCommandG71(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Metric units, 1/10mm ({base.ToString()})";
        }
    }
}
