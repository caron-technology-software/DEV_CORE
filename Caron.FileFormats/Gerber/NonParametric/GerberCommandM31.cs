//MMIx18
namespace Caron.FileFormats.Gerber.NonParametric
{
    public class GerberCommandM31 : GerberSimpleCommand
    {
        public override string Kind => GerberCommandKind.M31;
        public string? Text { get; set; }
        public GerberCommandM31(string token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"Labeler data fellows ({base.ToString()})";
        }
    }
}
