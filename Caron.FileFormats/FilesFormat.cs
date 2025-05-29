using System;
using System.Linq;
using System.Text;
using System.IO;

using ProRob;
using ProRob.Extensions.String;

namespace Caron.FileFormats
{
    public class FilesFormat
    {
        private static readonly string[] StartHeadersDenninson = { "999", "000" };
        public static bool IsLectraVersion { get; set; } = false;

        public static bool IsDenninsonFile(string path)
        {
            if (new FileInfo(path).Length > ByteSize.FromKiloBytes(100).Bytes)
            {
                return false;
            }

            try
            {
                string[] fileContent = File.ReadLines(path).Take(StartHeadersDenninson.Length).ToArray();

                if (fileContent.Length < StartHeadersDenninson.Length)
                {
                    return false;
                }

               if ((fileContent[1].IndexOf("001") == 0) && (fileContent[0].Contains(StartHeadersDenninson[0])))
                {
                    //file mat:
                    return true;
                }
                else
                {
                    return (fileContent[0].Contains(StartHeadersDenninson[0]) && fileContent[1].Contains(StartHeadersDenninson[1]));
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsGerberFile(string path)
        {
            string[] HeaderGerber = { "H1*", "H001*" };

            if (new FileInfo(path).Length > ByteSize.FromMegaBytes(1).Bytes)
            {
                return false;
            }

            var bytes = File.ReadAllBytes(path);

            bool ret = false;

            for (int i = 0; i < HeaderGerber.Count(); i++)
            {
                try
                {
                    byte[] fileContent = bytes.Take(HeaderGerber[i].Count()).ToArray();

                    ret = fileContent.SequenceEqual(HeaderGerber[i].ToBytes());

                    //Console.WriteLine($"[IsGerberFile] ret: {ret} path:{fullPath}");

                    if (ret)
                    {
                        return ret;
                    }
                }
                catch
                {
                    Console.WriteLine("[IsGerberFile] Exception");
                    // Nothing to do
                }
            }

            if (ret == false)
            {
                var header = File.ReadAllText(path).Take(HeaderGerber[0].Length);

                if (header.SequenceEqual(HeaderGerber[0]))
                {
                    //Console.WriteLine($"[IsGerberFile] ret: {true} path:{fullPath}");

                    return true;
                }
            }

            return false;
        }

        public static bool IsCutTicketFile(string path)
        {
            if (new FileInfo(path).Length > ByteSize.FromMegaBytes(1).Bytes)
            {
                return false;
            }

            var src = File.ReadAllLines(path);

            return (src[1] == "<CutTicket xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">")
                || (src[1] == "<CutTicket>");
        }
    }
}