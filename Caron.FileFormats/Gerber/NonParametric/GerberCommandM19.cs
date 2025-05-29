//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandM19 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.M19;

        public GerberCommandM19(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Ignore overcut ({base.ToString()})";
        }
    }
}
