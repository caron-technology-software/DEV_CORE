using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Compression;
using System.IO;
using System.Runtime.CompilerServices;

namespace ProRob.Compression
{
    public class Zlip
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Deflate(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            using (var output = new MemoryStream())
            {
                using (
                    var compressor = new Ionic.Zlib.DeflateStream(
                    output, Ionic.Zlib.CompressionMode.Compress,
                    Ionic.Zlib.CompressionLevel.BestSpeed))
                {
                    compressor.Write(data, 0, data.Length);
                }

                return output.ToArray();
            }
        }
    }

    public class Zip
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Compress(byte[] data, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            using var compressedStream = new MemoryStream();
            using var zipStream = new GZipStream(compressedStream, compressionLevel);

            zipStream.Write(data, 0, data.Length);
            zipStream.Close();

            return compressedStream.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Decompress(byte[] data)
        {
            using var compressedStream = new MemoryStream(data);
            using var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress);
            using var resultStream = new MemoryStream();

            zipStream.CopyTo(resultStream);

            return resultStream.ToArray();
        }
    }
}
