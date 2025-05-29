//MMIx18
namespace Caron.FileFormats.Gerber
{
    ///////////Per poter utilizzare INumber bisogna avere una versione di .NET Framework uguale o superiore alla 7.0

    public abstract class GerberNumericCommand<T> : GerberSimpleCommand
    //where T : INumber<T>
    where T : struct
    {
        public T Value { get; set; }

        protected GerberNumericCommand(string token, bool computeTrailingCommands) : base(token, false)
        {
        }

        public override string ToString()
        {
            return $"{Kind} = {Value}";
        }
    }

}
