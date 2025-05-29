#undef VERBOSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProRob;
using ProRob.Extensions.String;

namespace Caron.FileFormats.Denninson
{
    public static partial class DenninsonConverter
    {
        public static void ConvertFromFile(string input, string output)
        {
            var src = File.ReadAllLines(input);

            File.WriteAllText(output, DenninsonConverter.Convert(src));
        }

        public static DenninsonVersion GetVersion(string[] src)
        {
            if (IsGerberVersion(src))
            {
                return DenninsonVersion.Gerber;
            }
            else if (IsLectraVersion(src))
            {
                return DenninsonVersion.Lectra;
            }
            else
            {
                return DenninsonVersion.Caron;
            }
        }

        public static string Convert(string[] src)
        {
            if (IsGerberVersion(src))
            {
                //MMIx13
                FilesFormat.IsLectraVersion = false;
                //MMIx13
                return ConvertFromGerberVersion(src);
            }
            else if (IsLectraVersion(src))
            {
                //MMIx13
                FilesFormat.IsLectraVersion = true;
                //MMIx13

                var gerberDenninson = ConvertFromLectraVersionToGerberVersion(src);
                //MMIx46
                if(gerberDenninson.Trim() == "")
                {
                    return "";
                }
                //MMFx46
                ////// file DENNISON convertito al tipo DENNISON GERBER
                //string result1 = string.Join("\r\n", src);
                //System.IO.File.WriteAllLines(@"C:\WORKINGS\DENISON_GERBER_DA_448209A+_001.001", gerberDenninson.SplitNewline());

                return ConvertFromGerberVersion(gerberDenninson.SplitNewline());
            }
            else if (IsMatVersion(src))
            {
                //MMIx13
                FilesFormat.IsLectraVersion = false;
                //MMIx13
                var gerberDenninson = ConvertFromMatVersionToGerberVersion(src);

                if (gerberDenninson.Trim()=="")
                {
                    return "";
                }

                return ConvertFromGerberVersion(gerberDenninson.SplitNewline());
                //return "";
            }
            else
            {
                //MMIx13
                FilesFormat.IsLectraVersion = false;
                //MMIx13
                return string.Join(Environment.NewLine, src);
            }
        }
    }
}
