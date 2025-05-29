using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Apex.Serialization;

namespace ProRob.Extensions.Object
{
    public static class ObjectExtensions
    {
        private static readonly object locker;

        private static readonly IBinary serializer;
        private static readonly IBinary deserializer;

        static ObjectExtensions()
        {
            locker = new object();
            serializer = Binary.Create(new Apex.Serialization.Settings());
            deserializer = Binary.Create(new Apex.Serialization.Settings());
        }

        public static T Clone<T>(this T obj)
        {
            lock (locker)
            {
                var stream = new System.IO.MemoryStream();

                serializer.Write(obj, stream);

                return deserializer.Read<T>(new System.IO.MemoryStream(stream.ToArray()));
            }
        }

        public static byte[] GetBytesFromSerialization<T>(this T obj)
        {
            lock (locker)
            {
                var stream = new System.IO.MemoryStream();

                serializer.Write(obj, stream);

                return stream.ToArray();
            }
        }
    }
}
