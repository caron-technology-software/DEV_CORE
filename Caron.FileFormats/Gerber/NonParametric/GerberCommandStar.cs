//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandStar : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.Star;

        public GerberCommandStar(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"End of block ({base.ToString()})";
        }
    }
}
