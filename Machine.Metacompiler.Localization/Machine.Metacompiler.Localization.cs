#undef ADD_BUILD_DATE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob;
using ProRob.Extensions.String;
using ProRob.Documents;

namespace Machine.Metacompilers
{
    class Program
    {
        public static string GenerateProperties(string path)
        {
            var sb = new StringBuilder();

            var items = GetStringsToTranslateFromExcelDocument(path).ToList();
            items.RemoveAll(x => string.IsNullOrEmpty(x));
            items.Sort();

            for (int i = 0; i < items.Count(); i++)
            {
                if (!string.IsNullOrEmpty(items[i]))
                {
                    sb.AppendLine($"		public static string {items[i]} => Machine.Localization.GetTranslation();");
                }
            }

            return sb.ToString();
        }

        public static string[] GetStringsToTranslateFromExcelDocument(string path)
        {
            var matrix = ProRob.Documents.SpreadSheet.GetMatrixFromExcelFile(path);

            var stringToLocalize = matrix.GetColumn(0);
            stringToLocalize.RemoveAt(0);

            return stringToLocalize.ToArray();
        }

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                string pathXls = args[0];
                string namespaceName = args[1];
                string className = args[2];
                string pathLocalizationStringClass = args[3];
                string pathLocalizationData = args[4];

                bool debugMode = args.Length == 6 && args[5].ToUpper().Equals("DEBUG");

                //---------------------
                // Class
                //---------------------
                {
                    var sb = new StringBuilder();

                    sb.AppendLine($"// File: {Path.GetFileName(pathXls)}");
                    sb.AppendLine($"// Hash (SHA512): {ProRob.Security.Hashing.ComputeSHA512(new FileInfo(pathXls))}");
#if ADD_BUILD_DATE

                    sb.AppendLine($"// Build date: {DateTime.Now.ToString()}");
#endif
                    sb.AppendLine();

                    sb.AppendLine($"namespace {namespaceName}");
                    sb.AppendLine("{");
                    sb.AppendLine($"    public sealed partial class {className}");
                    sb.AppendLine("    {" + Environment.NewLine);

                    sb.AppendLine(GenerateProperties(pathXls));

                    sb.AppendLine("    }");
                    sb.AppendLine("}");

                    File.WriteAllText(pathLocalizationStringClass, sb.ToString());
                }

                //---------------------
                // Dictionary
                //---------------------
                {
                    var localizationData = new LocalizationData()
                    {
                        Languages = Enum.GetNames(typeof(MachineLanguage)),
                        Dictionary = Localization.CreateDictionary(pathXls)
                    };

                    Json.Serialize(localizationData).SaveToFile(pathLocalizationData);
                }

                sw.Stop();

                if (debugMode)
                {
                    MessageBox.Show($"Elapsed time: {sw.ElapsedMilliseconds} ms");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Message:{e.Message}\n\nSource:{e.Source}", "Metacompiler exception", MessageBoxButtons.OK);
            }
        }
    }
}
