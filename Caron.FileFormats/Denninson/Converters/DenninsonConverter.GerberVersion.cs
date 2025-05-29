#undef VERBOSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProRob;
using ProRob.Extensions.String;

namespace Caron.FileFormats.Denninson
{
    public static partial class DenninsonConverter
    {
        public static bool IsGerberVersion(string[] src)
        {
            //------------------------------------------------
            // Find not default commands
            //------------------------------------------------
            string pattern = @"^008 *";

            for (int i = 0; i < src.Length; i++)
            {
                var matches = Regex.Matches(src[i], pattern, RegexOptions.None);

                if (matches.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static string ConvertFromGerberVersion(string[] src)
        {
            //------------------------------------------------
            // Range #1
            //------------------------------------------------
#if VERBOSE
            ProConsole.WriteLine("Range #1", ConsoleColor.Yellow);
#endif
            var range = new List<int>();
            {
                string pattern = @"^201 *";

                for (int i = 0; i < src.Length; i++)
                {
                    var matches = Regex.Matches(src[i], pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
#if VERBOSE
                        Console.WriteLine($"[line {i:00}]\n\t{src[i]}");
#endif
                        range.Add(i);
                    }
                }
            }

            //------------------------------------------------
            // Offset
            //------------------------------------------------
            //201 001 002 00010 *A* 
#if VERBOSE
            ProConsole.WriteLine("Offset", ConsoleColor.Yellow);
#endif
            var offsets = new List<int>();
            var linesOffsets = new List<int>();
            {
                string pattern = @"^201 (\d+) (\d+) (\d+) \*A\*";

                for (int i = 0; i < src.Length; i++)
                {
                    var matches = Regex.Matches(src[i], pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
                        int lineOffset = int.Parse(matches[0].Groups[3].ToString());

#if VERBOSE
                        Console.WriteLine($"[line {i:00}]\n\t{src[i]}");
                        Console.WriteLine($"\t Offset: {lineOffset}");
#endif

                        linesOffsets.Add(i);
                        offsets.Add(lineOffset);
                    }
                }
            }
            var helperSections = linesOffsets.Select(x => x - linesOffsets[0] + 1);

            //------------------------------------------------
            // Sections
            //------------------------------------------------
            //002 A4       001 001 15856 001 005
            //ProConsole.WriteLine("Sections", ConsoleColor.Yellow);
            var records002Section = new List<SectionRange>();
            var linesSection002 = new List<string>();
            {
                const int SectionFromStartIndex = 27;
                const int SectionToStartIndex = 31;
                const int NumberOfPlyStartIndex = 35;

                string pattern = @"^002.*";

                for (int i = 0; i < src.Length; i++)
                {
                    var matches = Regex.Matches(src[i], pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
#if VERBOSE
                        Console.WriteLine($"[line {i:00}]\n\t{src[i]}");
#endif
                        linesSection002.Add(src[i]);

                        var record = new SectionRange()
                        {
                            SectionFrom = int.Parse(src[i].Substring(SectionFromStartIndex, SectionToStartIndex - SectionFromStartIndex - 1)),
                            SectionTo = int.Parse(src[i].Substring(SectionToStartIndex, NumberOfPlyStartIndex - SectionToStartIndex - 1)),
                        };

                        records002Section.Add(record);

#if VERBOSE
                        Console.WriteLine(record);
#endif
                    }
                }
            }

            //------------------------------------------------
            // Markers info
            //------------------------------------------------
            //000 V   V   V     V               V
            //201 001 001 03926 J4850CF         J4850CF.GBR
#if VERBOSE
            ProConsole.WriteLine("Markers info", ConsoleColor.Yellow);
#endif
            var records201Section = new List<MarkerInfo>();
            {
                const int MarkerLengthStartIndex = 12;
                const int MarkerIdStartIndex = 18;
                const int MarkerFilenameStartIndex = 34;

                for (int i = range.First(); i <= range.Last(); i++)
                {
                    if (linesOffsets.Contains(i))
                    {
                        continue;
                    }

                    var record = new MarkerInfo()
                    {
                        MarkerLength = int.Parse(src[i].Substring(MarkerLengthStartIndex, MarkerIdStartIndex - MarkerLengthStartIndex - 1)),
                        MarkerId = src[i].Substring(MarkerIdStartIndex, MarkerFilenameStartIndex - MarkerIdStartIndex - 1).Trim(),
                        MarkerFilename = src[i].Substring(MarkerFilenameStartIndex).Trim()
                    };

                    records201Section.Add(record);

#if VERBOSE
                    Console.WriteLine($"{record}");
#endif
                }
            }

            //------------------------------------------------
            // Overlaps
            //------------------------------------------------
            //014 001 01260 01114 
#if VERBOSE
            ProConsole.WriteLine("Overlaps", ConsoleColor.Yellow);
#endif
            var overlapsLines = new List<string>();
            {
                string pattern = @"^014.*";

                for (int i = 0; i < src.Length; i++)
                {
                    var matches = Regex.Matches(src[i], pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
#if VERBOSE
                        Console.WriteLine($"[line {i:00}]\n\t{src[i]}");
#endif
                        overlapsLines.Add(src[i]);
                    }
                }
            }

            //------------------------------------------------
            // General allowance
            //------------------------------------------------
            //007 A4       0025 0025
#if VERBOSE
            ProConsole.WriteLine("General allowance", ConsoleColor.Yellow);
#endif
            var generalAllowancesLines = new List<string>();
            {
                string pattern = @"^007.*";

                for (int i = 0; i < src.Length; i++)
                {
                    var matches = Regex.Matches(src[i], pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
#if VERBOSE
                        Console.WriteLine($"[line {i:00}]\n\t{src[i]}");
#endif
                        generalAllowancesLines.Add(src[i]);
                    }
                }
            }

            //------------------------------------------------
            // Splice allowance
            //------------------------------------------------
            //118 A4       0025 0025
#if VERBOSE
            ProConsole.WriteLine("Splice allowance", ConsoleColor.Yellow);
#endif
            var spliceAllovanceLines = new List<string>();
            {
                string pattern = @"^118.*";

                for (int i = 0; i < src.Length; i++)
                {
                    var matches = Regex.Matches(src[i], pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
#if VERBOSE
                        Console.WriteLine($"[line {i:00}]\n\t{src[i]}");
#endif
                        spliceAllovanceLines.Add(src[i]);
                    }
                }
            }

            //------------------------------------------------
            // Result
            //------------------------------------------------
            int offset = 0;
            if (offsets != null && offsets.Count > 0)
            {
                offset = offsets.Max();
            }

            var sb = new StringBuilder();

            sb.AppendLine(src[0]);

            sb.Append(Constants.Caron.Headers.Section201);
            sb.Append(ConvertSection201(records201Section, offset));

            int missingColor = 0;
            sb.Append(Constants.Caron.Headers.Section002);
            for (int i = 0; i < linesSection002.Count; i++)
            {
                string line = linesSection002[i];
                line = line.Substring(0, 21) + line.Substring(27);

                int sectionFrom = records002Section[i].SectionFrom;
                int deltaFrom = helperSections.Where(x => x < sectionFrom).Count();
                sectionFrom -= deltaFrom;

                int sectionTo = records002Section[i].SectionTo;
                int deltaTo = helperSections.Where(x => x < sectionTo).Count();
                sectionTo -= deltaTo;

                line = line.ReplaceChars(sectionFrom.ToString("000"), 21);
                line = line.ReplaceChars(sectionTo.ToString("000"), 25);

                if (string.IsNullOrWhiteSpace(line.Substring(33, 1)))
                {
                    missingColor++;
                    line = line.ReplaceChars($"C{missingColor:00}", 33);
                }

                if (string.IsNullOrWhiteSpace(line.Substring(91, 1)))
                {
                    line = line.ReplaceChars($"EW", 91);
                }

                //Replace spreader
                line = line.ReplaceChars($"001", 13);

                sb.AppendLine(line);
            }

            sb.Append(Constants.Caron.Headers.Section007);
            generalAllowancesLines.ForEach(x => sb.AppendLine(x));

            sb.Append(Constants.Caron.Headers.Section014);
            overlapsLines.ForEach(x => sb.AppendLine(x));

            sb.Append(Constants.Caron.Headers.Section118);
            spliceAllovanceLines.ForEach(x => sb.AppendLine(x));

            return sb.ToString();
        }

        //000 --------------------------------------------------------------
        //000 Spread Nr.
        //000 v Section Nr.
        //000 v v   Section begin 
        //000 v v   v Section end
        //000 v v   v v     Marker ID 
        //000 v v   v v     v Marker Filename
        //000 V V   V V     V V
        //
        //    0   0   1     1     2               4
        //    4   8   2     8     4               0
        //201 001 001 00000 01526 150-CM          150-CM.CUT
        private static string ConvertSection201(IEnumerable<MarkerInfo> data, int offset)
        {
            int counter = 0;
            var sb = new StringBuilder();

            for (int i = 0; i < data.Count(); i++)
            {
                var element = data.ElementAt(i);

                string line = new string(' ', 90);

                line = line.ReplaceChars("201 001", 0);
                line = line.ReplaceChars($"{(i + 1):000}", 8);
                line = line.ReplaceChars($"{counter:00000}", 12);
                line = line.ReplaceChars($"{counter + element.MarkerLength:00000}", 18);
                line = line.ReplaceChars(element.MarkerId, 24);
                line = line.ReplaceChars(element.MarkerFilename, 40);

                counter += (element.MarkerLength + offset);

                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}
