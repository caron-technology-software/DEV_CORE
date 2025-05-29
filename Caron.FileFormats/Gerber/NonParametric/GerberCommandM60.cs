//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandM60 : GerberSimpleCommand { public override string Kind => GerberCommandKind.M60; public GerberCommandM60(string token) : base(token) { } }
}
