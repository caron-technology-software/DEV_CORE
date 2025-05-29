using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caron.FileFormats.Denninson
{
    public partial class Denninson
    {
        public static int GetSpreadLength(string[] src)
        {
            try
            {
                if (DenninsonConverter.IsGerberVersion(src))
                {
                    src = DenninsonConverter.Convert(src).Split(Environment.NewLine.ToCharArray());
                }

                //201 001 009 09728 10954 151_B70_A_3356_ 151_B70_A_3356_SD1U20_P_T36.gbr  
                var lastLine = string.Empty;

                src = DenninsonConverter.Convert(src).Split(Environment.NewLine.ToCharArray());

                string pattern = @"201 001 (\d+) (\d+) (\d+) .*";

                for (int i = 0; i < src.Length; i++)
                {
                    var matches = Regex.Matches(src[i], pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
                        lastLine = src[i];
                    }
                }

                if (string.IsNullOrEmpty(lastLine))
                {
                    return -1;
                }

                var subString = new string(lastLine.ToCharArray().Skip(Constants.SectionEndStartIndex).Take(Constants.SectionEndLength).ToArray());

                return int.Parse(subString);
            }
            catch
            {
                return -1;
            }
        }

        public static int GetSpreadLength(string denninsonPath)
        {
            var src = File.ReadAllLines(denninsonPath);

            return GetSpreadLength(src);
        }

        public static int GetNumberOfMarkers(string[] src)
        {
            if (DenninsonConverter.IsGerberVersion(src))
            {
                src = DenninsonConverter.Convert(src).Split(Environment.NewLine.ToCharArray());
            }

            int counter = 0;

            string pattern = @"^201.*";

            for (int idxLine = 0; idxLine < src.Length; idxLine++)
            {
                var matches = Regex.Matches(src[idxLine], pattern, RegexOptions.None);

                if (matches.Count > 0)
                {
                    counter++;
                }
            }

            return counter;
        }

        public static List<string> GetFilesGerber(string[] src)
        {
            if (DenninsonConverter.IsGerberVersion(src))
            {
                src = DenninsonConverter.Convert(src).Split(Environment.NewLine.ToCharArray());
            }

            var files = new List<string>();

            string pattern = @"^201.*";

            for (int idxLine = 0; idxLine < src.Length; idxLine++)
            {
                var matches = Regex.Matches(src[idxLine], pattern, RegexOptions.None);

                if (matches.Count > 0)
                {
                    files.Add(src[idxLine].Substring(40).Trim());
                }
            }

            return files.Distinct().ToList();
        }

        public static List<string> GetColors(string[] src)
        {
            if (DenninsonConverter.IsGerberVersion(src))
            {
                src = DenninsonConverter.Convert(src).Split(Environment.NewLine.ToCharArray());
            }

            var colors = new List<string>();

            string pattern = @"^002.*";

            for (int idxLine = 0; idxLine < src.Length; idxLine++)
            {
                var matches = Regex.Matches(src[idxLine], pattern, RegexOptions.None);

                if (matches.Count > 0)
                {
                    colors.Add(src[idxLine].Substring(33, 26).Trim());
                }
            }

            return colors;
        }

        public static SpreadMode GetSpreadMode(string[] src)
        {
            src = DenninsonConverter.Convert(src).Split(Environment.NewLine.ToCharArray());

            string pattern = @"^002.*";

            bool exitCond = false;
            int idxLine = 0;

            while (exitCond == false)
            {
                var matches = Regex.Matches(src[idxLine], pattern, RegexOptions.None);

                if (matches.Count > 0)
                {
                    exitCond = true;
                    continue;
                }

                if (idxLine >= src.Count())
                {
                    exitCond = true;
                }

                idxLine++;
            }

            string spreadModeString = src[idxLine].Substring(Constants.SpreadingMethodStartIndex, 2);

            switch (spreadModeString)
            {
                case "":
                case " ":
                case "  ":
                case "EW":
                    return SpreadMode.Open;

                case "EP":
                    return SpreadMode.MatchedUp;

                case "SL":
                    return SpreadMode.Tubular;

                case "ZZ":
                case "ZA":
                case "ZE":
                    return SpreadMode.ZigZag;
            }

            return SpreadMode.Open;
        }

        public static int GetFrontAllowance(string[] src)
        {
            src = DenninsonConverter.Convert(src).Split(Environment.NewLine.ToCharArray());

            string pattern = @"^007.*";

            bool exitCond = false;
            int idxLine = 0;

            while (exitCond == false)
            {
                var matches = Regex.Matches(src[idxLine], pattern, RegexOptions.None);

                if (matches.Count > 0)
                {
                    exitCond = true;
                    continue;
                }

                if (idxLine >= src.Count())
                {
                    return -1;
                }

                idxLine++;
            }

            string stringFrontAllowance = src[idxLine].Substring(Constants.FrontAllowanceStartIndex, 4);

            return int.Parse(stringFrontAllowance);
        }

        public static int GetRearAllowance(string[] src)
        {
            src = DenninsonConverter.Convert(src).Split(Environment.NewLine.ToCharArray());

            string pattern = @"^007.*";

            bool exitCond = false;
            int idxLine = 0;

            while (exitCond == false)
            {
                var matches = Regex.Matches(src[idxLine], pattern, RegexOptions.None);

                if (matches.Count > 0)
                {
                    exitCond = true;
                    continue;
                }

                if (idxLine >= src.Count())
                {
                    return -1;
                }

                idxLine++;
            }

            string stringRearAllowance = src[idxLine].Substring(Constants.RearAllowanceStartIndex, 4);

            return int.Parse(stringRearAllowance);
        }

        public static int GetParameterSet(string[] src)
        {
            src = DenninsonConverter.Convert(src).Split(Environment.NewLine.ToCharArray());

            string pattern = @"^002.*";

            bool exitCond = false;
            int idxLine = 0;

            while (exitCond == false)
            {
                var matches = Regex.Matches(src[idxLine], pattern, RegexOptions.None);

                if (matches.Count > 0)
                {
                    exitCond = true;
                    continue;
                }

                if (idxLine >= src.Count())
                {
                    return -1;
                }

                idxLine++;
            }

            string charParameterSet = src[idxLine].Substring(Constants.ParameterSetStartIndex, 1);

            return int.Parse(charParameterSet);
        }

        public static bool TryGetInfos(string src, out Info info)
        {
            var lines = src.Split(Environment.NewLine.ToCharArray());

            //GPIx272 sistemato caricamento spreadMethod da pulsante caricamento DENNINSON
            int lineCount = -1;
            for (int i = 0; i < lines.Length; i++)
            {
                if (!lines[i].Trim().Equals("")) { lineCount++; }
            }
            string[] linesFix = new string[lineCount + 1];
            int c01 = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (!lines[i].Trim().Equals("")) { linesFix[c01] = lines[i]; c01++; }
            }
            return TryGetInfos(linesFix, out info);
            //GPFx272

            //return TryGetInfos(lines, out info);
        }

        public static bool TryGetInfos(string[] src, out Info info)
        {
            info = null;

            try
            {
                src = DenninsonConverter.Convert(src).Split(Environment.NewLine.ToCharArray());

                info = new Info()
                {
                    ParameterSet = GetParameterSet(src),
                    SpreadMode = GetSpreadMode(src),
                    NumberOfMarkers = GetNumberOfMarkers(src),
                    SpreadLength = GetSpreadLength(src),
                    Colors = GetColors(src),
                    GerberFiles = GetFilesGerber(src),
                    FrontAllowance = GetFrontAllowance(src),
                    RearAllowance = GetRearAllowance(src),
                };

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[Denninson.Info] Exception: {ex.Message}");
                info = null;

                return false;
            }
        }
    }
}
