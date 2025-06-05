using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using Apex.Serialization;

namespace ProRob.Extensions.Object
{
    public static class ObjectExtensions
    {
        private static readonly object locker = new object ();

        private static readonly IBinary serializer;
        private static readonly IBinary deserializer;

        static ObjectExtensions()
        {
            var settings = new Apex.Serialization.Settings();

            //settings.MarkSerializable(typeof(Caron.Cradle.Control.ControlStatus));
            //settings.MarkSerializable(typeof(Caron.Cradle.Control.LowLevel.ControlStatus));
            //settings.MarkSerializable(typeof(Caron.Cradle.Control.HighLevel.ControlStatus));
            //settings.AllowFunctionSerialization = true;

            serializer = Binary.Create(settings);
            deserializer = Binary.Create(settings);
        }

        public static T Clone<T>(this T obj)
        {
            lock (locker)
            {
                var json = JsonSerializer.Serialize(obj);
                return JsonSerializer.Deserialize<T>(json)!;
            }
        }

        public static T JClone<T>(this T obj)
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

                JsonSerializer.Serialize(stream, obj);

                return stream.ToArray();
            }
        }
    }
}
