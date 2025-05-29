//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandA : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.A;

        public GerberCommandA(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Knife up ({base.ToString()})";
        }
    }
}
