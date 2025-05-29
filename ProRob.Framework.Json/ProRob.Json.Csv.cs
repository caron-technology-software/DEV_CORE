using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Dynamic;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using CsvHelper;
using CsvHelper.Configuration;

using ProRob.Extensions.Object;

namespace ProRob
{
    public partial class Json
    {
        public static string JsonToCsv(string jsonContent, string delimiter)
        {
            var expando = JsonConvert.DeserializeObject<ExpandoObject[]>(jsonContent);

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
            configuration.Delimiter = delimiter;

            using (var writer = new StringWriter())
            {
                using (var csv = new CsvWriter(writer, configuration))
                {
                    csv.WriteRecords(expando as IEnumerable<dynamic>);
                }

                return writer.ToString();
            }
        }

        public static string ObjectToCSV<T>(T obj, string delimiter)
        {
            return JsonToCsv(Serialize(new List<T>() { obj }), delimiter);
        }

        public static string ObjectToCSV<T>(IEnumerable<T> obj, string delimiter)
        {
            return JsonToCsv(Serialize(obj), delimiter);
        }
    }
}
