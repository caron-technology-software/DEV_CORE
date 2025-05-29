using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ProRob
{
    public static class Xml
    {
        public static T Deserialize<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml));

            Console.WriteLine(System.Text.Encoding.UTF8.GetString(ms.ToArray()));

            T obj = (T)serializer.Deserialize(ms);

            ms.Close();

            return obj;

        }

        public static string Serialize<T>(T obj)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                XmlWriter writer = new XmlTextWriter(ms, Encoding.Unicode);

                serializer.Serialize(writer, obj);
                writer.Close();

                return System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }
            catch
            {
                return null;
            }
        }
    }
}
