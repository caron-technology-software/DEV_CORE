using System;
using System.Linq;
using System.Text;
using System.IO;

using ProRob.Extensions;
using ProRob.Security;
using ProRob.Conversion;

namespace ProRob
{
    public static class Barcode
    {
        public static string GetBarcodeFromFile(FileInfo fileInfo)
        {
            return GetBarcodeFromHash(Hashing.ComputeSHA512(fileInfo));
        }

        public static string GetBarcodeFromHash(string hash)
        {
            return ProRob.Security.Hashing.ComputeFixedLengthHashWithAlphabeticCharacters(hash);
        }
    }
}