//MMIx16
using Caron.FileFormats.Gerber.NonParametric;

namespace Caron.FileFormats.Gerber.Parametric
{
    public class GerberCommandF : GerberSimpleCommand { public override string Kind => GerberCommandKind.F; public GerberCommandF(string token) : base(token) { } }
}
