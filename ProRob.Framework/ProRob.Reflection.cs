using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob
{
    public static class ReflectionHelper
    {
        private static void PrintDictionary(Dictionary<string, string> dict)
        {
            foreach (var key in dict.Keys)
            {
                Console.WriteLine("name:{0} - value: {1}", key, dict[key]);
            }
        }

        public static Dictionary<string, string> GetDictionaryFromObjectProperties(object obj)
        {
            var infos = obj.GetType().GetProperties();

            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (var info in infos)
            {
                dict.Add(info.Name, info.GetValue(obj, null).ToString());
            }

            PrintDictionary(dict);

            return dict;
        }

        public static Dictionary<string, string> GetDictionaryFromObjectFields(object obj)
        {
            var infos = obj.GetType().GetFields();

            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (var info in infos)
            {
                dict.Add(info.Name, info.GetValue(obj).ToString());
            }

            PrintDictionary(dict);

            return dict;
        }
    }
}
