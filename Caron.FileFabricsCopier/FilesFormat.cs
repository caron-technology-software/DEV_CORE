using System;
using System.Linq;
using System.Text;
using System.IO;

using ProRob.Extensions.String;

namespace Caron.FileFormats
{
    public static class FilesFormat
    {
        public static bool IsDenninsonFiles(FileInfo file)
        {
            if (file.Length > 100_000)
            {
                return false;
            }

            const string HeaderDenninson = "000 --------------------------------------------------------------";
            const int NumberOfLines = 2;

            try
            {
                string[] fileContent = File.ReadLines(file.FullName).Take(NumberOfLines).ToArray();

                if (fileContent.Length < NumberOfLines)
                {
                    return false;
                }

                return (fileContent[1].SequenceEqual(HeaderDenninson));
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDenninsonFiles(string path)
        {
            return IsDenninsonFiles(new FileInfo(path));
        }

        public static bool IsGerberFile(FileInfo file)
        {
            if (file.Length > 1_024_000)
            {
                return false;
            }

            string[] HeaderGerber = { "H1*", "H001*" };

            var bytes = File.ReadAllBytes(file.FullName);

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
                var header = File.ReadAllText(file.FullName).Take(HeaderGerber[0].Length);

                if (header.SequenceEqual(HeaderGerber[0]))
                {
                    //Console.WriteLine($"[IsGerberFile] ret: {true} path:{fullPath}");

                    return true;
                }
            }

            return false;
        }

        public static bool IsGerberFile(string path)
        {
            return IsGerberFile(new FileInfo(path));
        }

        public static bool IsCutTicketFile(FileInfo file)
        {
            var src = File.ReadAllLines(file.FullName);

            return src[1] == "<CutTicket xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">";
        }

        public static bool IsCutTicketFile(string path)
        {
            return IsCutTicketFile(new FileInfo(path));
        }
    }
}