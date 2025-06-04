using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

using ProRob.Extensions.String;

namespace ProRob.Extensions.Json
{
    public static class JsonExtensions
    {
        private static JsonSerializerSettings deserializationSettings;

        static JsonExtensions()
        {
            deserializationSettings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.DateTime,
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToJson<T>(this T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToJsonIndented<T>(this T obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return json;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FromJson<T>(this T obj, string json)
        {
            return JsonConvert.DeserializeObject<T>(json, deserializationSettings);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FromJsonFile<T>(this T obj, string path)
        {
            var json = File.ReadAllLines(path).Join();

            return JsonConvert.DeserializeObject<T>(json, deserializationSettings);
        }
    }
}
