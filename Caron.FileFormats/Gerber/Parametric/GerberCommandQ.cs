//MMIx16
using Caron.FileFormats.Gerber.NonParametric;

namespace Caron.FileFormats.Gerber.Parametric
{
    public class GerberCommandQ : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.Q;

        public GerberCommandQ(string token) : base(token)
        {
            if (token?.Length > 1)
                TrailingContent = token.Substring(1, token.Length - 1);
        }

        public override string ToString()
        {
            return $"Light is the tool ({base.ToString()})";
        }
    }
}
