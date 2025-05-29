using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

using ProRob;
using ProRob.Conversion;

using ProRob.Extensions.String;
using ProRob.Extensions.ByteArray;

namespace ProRob.Security
{
    public static class Hashing
    {
        private const long MaxSizeToComputeHashWithManagedClasses = 5_000_000L; //[bytes]

        public static string ComputeFixedLengthHashWithAlphabeticCharacters(string hash)
        {
            var tmpHash = ProRob.Security.Hashing.ComputeFixedLengthHash(hash, 16);

            ulong num = System.Convert.ToUInt64(tmpHash, 16);

            return NumericHelper.DecimalToArbitrarySystem(num, 36);
        }

        public static string ComputeFixedLengthHash(string hash, int length)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                var tmpHash = Hashing.ComputeSHA512(hash.ToBytes().Append((byte)i).ToArray());
                sb.Append(tmpHash.Substring(0, 1));
            }

            return sb.ToString();
        }

        #region Folder
        public static string GetFolderSHA1Hash(string path)
        {
            var files = FileSystem.GetFiles(path, "*.*", SearchOption.AllDirectories);

            string hash = String.Empty;

            foreach (var file in files)
            {
                hash = ProRob.Security.Hashing.ComputeSHA1($"{hash}{file.Name}{ProRob.Security.Hashing.ComputeSHA1(file)}".ToBytes());
            }

            return hash;
        }

        public static string GetFolderSHA256Hash(string path)
        {
            var files = FileSystem.GetFiles(path, "*.*", SearchOption.AllDirectories);

            string hash = String.Empty;

            foreach (var file in files)
            {
                hash = ProRob.Security.Hashing.ComputeSHA256($"{hash}{file.Name}{ProRob.Security.Hashing.ComputeSHA256(file)}".ToBytes());
            }

            return hash;
        }

        public static string GetFolderSHA512Hash(string path)
        {
            var files = FileSystem.GetFiles(path, "*.*", SearchOption.AllDirectories);

            string hash = String.Empty;

            foreach (var file in files)
            {
                hash = ProRob.Security.Hashing.ComputeSHA512($"{hash}{file.Name}{ProRob.Security.Hashing.ComputeSHA512(file)}".ToBytes());
            }

            return hash;
        }
        #endregion

        #region Buffer
        public static string ComputeSHA1(byte[] buffer)
        {
            var hasher = new SHA1Managed();
            return (hasher.ComputeHash(buffer)).ToHexString();
        }

        public static string ComputeSHA256(byte[] buffer)
        {
            var hasher = new SHA256Managed();
            return hasher.ComputeHash(buffer).ToHexString();

        }

        public static string ComputeSHA512(byte[] buffer)
        {
            var hasher = new SHA512Managed();
            return hasher.ComputeHash(buffer).ToHexString();
        }
        #endregion

        #region File

        public static string ComputeSHA1(FileInfo fileInfo)
        {
            if (!File.Exists(fileInfo.FullName))
            {
                throw new FileNotFoundException();
            }

            if (fileInfo.Length < MaxSizeToComputeHashWithManagedClasses)
            {
                return Hashing.ComputeSHA1(System.IO.File.ReadAllBytes(fileInfo.FullName));
            }

            var output = ProcessHelper.Execute("CertUtil", $"-hashfile \"{fileInfo.FullName}\" SHA1", false);

            return output.Item1[1];
        }

        public static string ComputeSHA256(FileInfo fileInfo)
        {
            if (!File.Exists(fileInfo.FullName))
            {
                throw new FileNotFoundException();
            }

            if (fileInfo.Length < MaxSizeToComputeHashWithManagedClasses)
            {
                return Hashing.ComputeSHA256(System.IO.File.ReadAllBytes(fileInfo.FullName));
            }

            var output = ProcessHelper.Execute("CertUtil", $"-hashfile \"{fileInfo.FullName}\" SHA256", false);

            return output.Item1[1];
        }

        public static string ComputeSHA512(FileInfo fileInfo)
        {
            if (!File.Exists(fileInfo.FullName))
            {
                throw new FileNotFoundException();
            }

            if (fileInfo.Length < MaxSizeToComputeHashWithManagedClasses)
            {
                return Hashing.ComputeSHA512(System.IO.File.ReadAllBytes(fileInfo.FullName));
            }

            var output = ProcessHelper.Execute("CertUtil", $"-hashfile \"{fileInfo.FullName}\" SHA512", false);

            return output.Item1[1];
        }

        #endregion
    }
}