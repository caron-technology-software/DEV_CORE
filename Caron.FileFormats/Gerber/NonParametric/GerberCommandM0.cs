//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandM0 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.M0;

        public GerberCommandM0(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"End of file ({base.ToString()})";
        }
    }
}
