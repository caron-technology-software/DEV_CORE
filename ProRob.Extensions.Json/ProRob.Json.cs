using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using ProRob.Extensions.Object;

namespace ProRob
{
    public partial class Json
    {
        public static bool EnumConversion = true;

        private static readonly JsonSerializerSettings serializationSettings;
        private static readonly JsonSerializerSettings deserializationSettings;
        private static readonly DefaultContractResolver contractResolver;

        static Json()
        {
            contractResolver = new DefaultContractResolver
            {
                //NamingStrategy = new SnakeCaseNamingStrategy()
            };

            serializationSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
            };

            if (EnumConversion)
            {
                serializationSettings.Converters.Add(new StringEnumConverter());
            }

            deserializationSettings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.DateTime,
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,

                /*ContractResolver = new DefaultContractResolver
                {
                    //NamingStrategy = new CamelCaseNamingStrategy
                    NamingStrategy = new SnakeCaseNamingStrategy
                    {
                        ProcessDictionaryKeys = true,
                        OverrideSpecifiedNames = true
                    }
                }
                */
            };
        }

        public static string Sanitize(string json)
        {
            json = json.Trim('"');

            //[OPTIMIZE]
            json = json.Replace("\\\"", "\"");
            json = json.Replace("\\\"", "\"");
            json = json.Replace(@"""\\\""", "\"\"");

            return json;
        }

        public static string AddBrackets(string json)
        {
            if (!(json[0].Equals('{') && json[json.Length - 1].Equals('}')))
            {
                json = "{" + json + "}";
            }

            return json;
        }

        public static T Deserialize<T>(string json)
        {
            if (String.IsNullOrEmpty(json))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(json, deserializationSettings);
        }

        public static string Serialize(in object value, bool indented = true)
        {
            try
            {
                object objToSerialize;
                try
                {
                    objToSerialize = value.Clone();
                }
                catch
                {
                    objToSerialize = value;
                }

                var jsonFormatting = indented ? Formatting.Indented : Formatting.None;

                return JsonConvert.SerializeObject(objToSerialize, jsonFormatting, serializationSettings);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetArraysElementsWithoutVariableDeclaration(string json)
        {
            var startIndex = json.IndexOf('[');
            int stopIndex = json.IndexOf(']');

            return json.Substring(startIndex, stopIndex - startIndex + 1);
        }
    }
}
