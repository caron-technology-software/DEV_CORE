//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandM14 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.M14;

        public GerberCommandM14(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Knife down ({base.ToString()})";
        }
    }
}
