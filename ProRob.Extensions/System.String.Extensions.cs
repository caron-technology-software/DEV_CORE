using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProRob.Extensions.String
{
    public static class StringExtensions
    {
        public static string ReplaceChars(this string @string, string text, in int startIndex)
        {
            if (startIndex + text.Length > @string.Length ||
                startIndex < 0)
            {
                return @string;
            }

            var stringCharArray = @string.ToCharArray();
            var textCharArray = text.ToCharArray();

            for (int i = 0; i < textCharArray.Length; i++)
            {
                stringCharArray[startIndex + i] = textCharArray[i];
            }

            return new string(stringCharArray);
        }

        public static string[] SplitNewline(this string obj)
        {
            return obj.Split(Environment.NewLine.ToCharArray()).Select(x => x.Trim()).Where(x => string.IsNullOrEmpty(x) == false).ToArray();
        }

        public static string[] RemoveLines(this string[] obj, IEnumerable<int> indexes)
        {
            indexes = indexes.OrderByDescending(x => x);
            
            var lines = obj.ToList();

            foreach(var index in indexes)
            {
                lines.RemoveAt(index);
            }

            return lines.ToArray();
        }

        public static string Join(this string[] obj, string separator = "")
        {
            return string.Join(separator, obj);
        }

        public static string ToUTF8String(this byte[] value)
        {
            return System.Text.Encoding.UTF8.GetString(value);
        }
        public static string ToBase64(this string text)
        {
            return Convert.ToBase64String(text.ToBytes());
        }

        public static byte[] ToBytes(this string value)
        {
            return System.Text.Encoding.UTF8.GetBytes(value);
        }

        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }

        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }

        public static void SaveToFile(this string str, string path)
        {
            File.WriteAllText(path, str);
        }
    }
}