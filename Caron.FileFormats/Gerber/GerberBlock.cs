//MMIx18
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.FileFormats.Gerber
{
    public static class CsvUtils
    {
        public static StringBuilder ToCsv<T>(this T[] vals, string separator = ",", bool appendSeparatorOnLasElement = false, bool excludeNullValues = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (vals == null)
            {
                return stringBuilder;
            }

            if (separator == null)
            {
                throw new ArgumentNullException("separator", "Cannot generate a CSV without a valid separator");
            }

            List<T> list = new List<T>();
            if (excludeNullValues)
            {
                list.AddRange(vals.Where((T v) => v != null));
            }
            else
            {
                list.AddRange(vals);
            }

            for (int i = 0; i < list.Count; i++)
            {
                stringBuilder.Append(list[i]);
                if (i < list.Count - 1 || appendSeparatorOnLasElement)
                {
                    stringBuilder.Append(separator);
                }
            }

            return stringBuilder;
        }
    }
    public class GerberBlock
    {
        [Required]
        [MinLength(1)]
        public List<GerberCommand> Commands { get; } = new List<GerberCommand>();

        public GerberCommand? Head => Commands.FirstOrDefault();

        public override string ToString()
        {
            if (Commands.Count == 0)
                return base.ToString()!;

            if (Commands.Count == 1 && Head != null)
            {
                return Head?.ToString() ?? base.ToString()!;
            }

            return Commands.Select(c => c!.ToString()).ToArray().ToCsv(" → ").ToString() ?? base.ToString()!;
        }
    }
}
