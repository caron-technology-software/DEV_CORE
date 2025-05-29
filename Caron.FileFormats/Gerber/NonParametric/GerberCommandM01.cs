//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandM01 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.M01;

        public GerberCommandM01(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Optional stop ({base.ToString()})";
        }
    }
}
