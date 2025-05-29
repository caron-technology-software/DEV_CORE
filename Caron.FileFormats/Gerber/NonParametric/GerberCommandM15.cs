//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandM15 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.M15;

        public GerberCommandM15(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Knife up ({base.ToString()})";
        }
    }
}
