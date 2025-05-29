//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandM43 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.M43;

        public GerberCommandM43(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Drill ({base.ToString()})";
        }
    }
}
