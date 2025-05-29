using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine
{
    public static class LocalizationExtensions
    {
        public static string Translate(this string @string)
        {
            return Localization.GetTranslation(@string);
        }

        public static string Translate(this object @object)
        {
            return Localization.GetTranslation(@object.ToString());
        }

        public static string Translate(this bool @bool)
        {
            return Localization.GetTranslation(@bool ? "True" : "False");
        }
    }
}
