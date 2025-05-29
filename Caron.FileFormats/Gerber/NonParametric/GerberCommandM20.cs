//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandM20 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.M20;
        public string? Text { get; set; }
        public GerberCommandM20(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Message stop ({base.ToString()})";
        }
    }
}
