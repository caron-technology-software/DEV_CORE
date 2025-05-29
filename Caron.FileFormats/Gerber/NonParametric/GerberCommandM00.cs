//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandM00 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.M00;

        public GerberCommandM00(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Program stop ({base.ToString()})";
        }
    }
}
