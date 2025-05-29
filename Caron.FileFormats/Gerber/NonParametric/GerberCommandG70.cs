//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandG70 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.G70;

        public GerberCommandG70(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Imperial units, 1/1000 Inch ({base.ToString()})";
        }
    }
}
