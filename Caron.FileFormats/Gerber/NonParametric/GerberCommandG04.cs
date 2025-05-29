//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandG04 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.G04;

        public GerberCommandG04(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Origin point ({base.ToString()})";
        }
    }
}
