using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProRob
{
    namespace Conversion
    {
        public static class StringHelper
        {
            public static string Base64Encode(string message)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(message);
                return System.Convert.ToBase64String(plainTextBytes);
            }

            public static string Base64Decode(string message)
            {
                message = message.Trim('"');
                var base64EncodedBytes = System.Convert.FromBase64String(message);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            public static byte[] Base64ToBytes(string message)
            {
                return StringToBytes(Base64Encode(message));
            }

            public static byte[] StringToBytes(string message)
            {
                return System.Text.Encoding.UTF8.GetBytes(message);
            }

            public static string BytesToString(byte[] data)
            {
                return System.Text.Encoding.UTF8.GetString((data));
            }
        }

        public static class NumericHelper
        {
            const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const int BitsInLong = 64;

            public static string DecimalToArbitrarySystem(ulong decimalNumber, int radix)
            {
                ulong ulongRadix = (ulong)radix;
                if (radix < 2 || radix > Digits.Length)
                {
                    throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());
                }

                if (decimalNumber == 0)
                {
                    return "0";
                }

                int index = BitsInLong - 1;
                char[] charArray = new char[BitsInLong];

                while (decimalNumber != 0)
                {
                    int remainder = (int)(decimalNumber % ulongRadix);
                    charArray[index--] = Digits[remainder];
                    decimalNumber = decimalNumber / ulongRadix;
                }

                string result = new string(charArray, index + 1, BitsInLong - index - 1);

                return result;
            }

            public static ulong ArbitraryToDecimalSystem(string number, int radix)
            {
                ulong ulongRadix = (ulong)radix;
                if (radix < 2 || radix > Digits.Length)
                {
                    throw new ArgumentException("The radix must be >= 2 and <= " +
                        Digits.Length.ToString());
                }

                if (string.IsNullOrEmpty(number))
                {
                    return 0;
                }

                // Make sure the arbitrary numeral system number is in upper case
                number = number.ToUpperInvariant();

                ulong result = 0;
                ulong multiplier = 1;
                for (int i = number.Length - 1; i >= 0; i--)
                {
                    char c = number[i];

                    long digit = (long)Digits.IndexOf(c);
                    if (digit == -1)
                    {
                        throw new ArgumentException(
                            "Invalid character in the arbitrary numeral system number",
                            "number");
                    }

                    result += (ulong)digit * multiplier;
                    multiplier *= ulongRadix;
                }

                return result;
            }
        }
        public static class ObjectHelper
        {
            [Serializable]
            private class NonSerializableException : Exception
            {

            }

            //public static T DeepCopy<T>(T obj)
            //{
            //    if (typeof(T).IsSerializable)
            //    {
            //        BinaryFormatter bf = new BinaryFormatter();
            //        using (MemoryStream ms = new MemoryStream())
            //        {
            //            bf.Serialize(ms, obj);
            //            ms.Position = 0;
            //            T t = (T)bf.Deserialize(ms);

            //            return t;
            //        }
            //    }
            //    else
            //    {
            //        throw new NonSerializableException();
            //    }
            //}

            public static T Convert<T>(this string input)
            {
                try
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        // Cast ConvertFromString(string text) : object to (T)
                        return (T)converter.ConvertFromString(input);
                    }
                    return default(T);
                }
                catch (NotSupportedException)
                {
                    return default(T);
                }
            }
        }
    }
}

