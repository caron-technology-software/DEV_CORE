using System;
using System.IO;
using System.Text;

namespace ProRob.Extensions.ByteArray
{
    public static class BytesArrayExtensions
    {
        public static string ToVectorString(this byte[] value)
        {
            var sb = new StringBuilder();

            sb.Append("{");

            foreach (var v in value)
            {
                sb.Append($"{v},");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");

            return sb.ToString();
        }

        public static string ToHexFormattedString(this byte[] value)
        {
            return BitConverter.ToString(value);
        }

        public static string ToHexString(this byte[] value)
        {
            return BitConverter.ToString(value).Replace("-", "");
        }
    }
}