//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandM70 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.M70;

        public GerberCommandM70(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Origin ({base.ToString()})";
        }
    }
}
