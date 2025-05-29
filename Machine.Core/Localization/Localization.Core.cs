using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProRob.Documents;
using ProRob.Extensions.String;
using ProRob.Extensions.Json;

namespace Machine
{
    public sealed partial class Localization
    {
        public static void SaveDictionary(List<Dictionary<string, string>> dictionary, string path)
        {
            var dc = new LocalizationData()
            {
                Dictionary = dictionary,
                Languages = Enum.GetNames(typeof(MachineLanguage))
            };

            File.WriteAllText(path, dc.ToJsonIndented());
        }

        public static List<Dictionary<string, string>> CreateDictionary(string path)
        {
            var dictionary = new List<Dictionary<string, string>>();

            var matrix = SpreadSheet.GetMatrixFromExcelFile(path);

            var strings = matrix.GetColumn(0);
            strings.RemoveAt(0);
            int nStrings = strings.Count;

            var languages = matrix.GetRow(0);
            for (int idxLanguage = 0; idxLanguage < languages.Count; idxLanguage++)
            {
                var translations = matrix.GetColumn(idxLanguage + 1);
                translations.RemoveAt(0);

                var value = new Dictionary<string, string>();

                for (int idxString = 0; idxString < nStrings; idxString++)
                {
                    string langKey = strings[idxString];
                    if (string.IsNullOrEmpty(langKey))
                    {
                        continue;
                    }

                    string langValue = string.Empty;
                    if (string.IsNullOrEmpty(translations[idxString]))
                    {
                        langValue = strings[idxString].ToSentenceCase();
                    }
                    else
                    {
                        langValue = translations[idxString];
                    }

                    try
                    {
                        value.Add(langKey, langValue);
                    }
                    catch
                    {
                        Console.WriteLine("[Localization] Error on CreateDictionary()");
                    }
                }

                dictionary.Add(value);
            }

            return dictionary;
        }
    }
}
