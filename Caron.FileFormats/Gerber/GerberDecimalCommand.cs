//MMIx18
using System;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Caron.FileFormats.Gerber
{

    public abstract class GerberDecimalCommand : GerberNumericCommand<decimal>
    {

        protected GerberDecimalCommand(string token) : base(token, false)
        {
            if (string.IsNullOrWhiteSpace(token) || token.Length < 2)
                throw new ArgumentException($"'{Kind}' token cannot be null or whitespace, and must have a length greater than 1", nameof(token));

            var valueStr = new StringBuilder();
            var right = token.Skip(1).ToList();
            if (right.Count > 0)
            {
                do
                {
                    var c = right[0];
                    if ((c >= '0' && c <= '9') || c == '-' || c == '.')
                    {
                        valueStr.Append(c);
                        right.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                } while (right.Count > 0);
            }

            Value = decimal.Parse(valueStr.ToString(), CultureInfo.InvariantCulture);

            if (right.Count > 0)
            {
                TrailingContent = new string(right.ToArray());
            }
        }
    }

}
