using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using ProRob.Extensions.Object;
using ProRob.Extensions.ByteArray;

namespace ProRob.Extensions.Hashing
{
    public static class HashingExtensions
    {
        private static readonly SHA1Managed sha1Managed = new SHA1Managed();
        private static readonly SHA256Managed sha256Managed = new SHA256Managed();
        private static readonly SHA512Managed sha512Managed = new SHA512Managed();

        static HashingExtensions()
        {
            //--
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetSHA1Hash(this object obj)
        {
            //GPIx272
            string result = "ERROR";
            try
            {
                result = sha1Managed.ComputeHash(obj.GetBytesFromSerialization()).ToHexString();
            }
            catch
            {
                result = "ERROR";
            }
            return result;
            //GPFx272
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetSHA256Hash(this object obj)
        {
            return sha256Managed.ComputeHash(obj.GetBytesFromSerialization()).ToHexString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetSHA512Hash(this object obj)
        {
            return sha512Managed.ComputeHash(obj.GetBytesFromSerialization()).ToHexString();
        }
    }
}
