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
        public static bool IsLectraVersion(string[] src)
        {
            //------------------------------------------------
            // Find not default commands
            //------------------------------------------------
            //GPIx260
            //string pattern = @"000 |        Total General Allowance (Half on each side)";
            string pattern = @"004";

            string noPattern = @"997";
            string noPattern1 = @".mat";

            for (int i = 0; i < src.Length; i++)
            {
                if ((src[i].StartsWith(noPattern)) || (src[i].Contains(noPattern1)))//MMIx46
                {
                    return false;
                }
            }

            for (int i = 0; i < src.Length; i++)
            {
                string str01 = src[i];
                if (str01.Length >= 3)
                {
                    //if (src[i].Contains(pattern))
                    if (str01.Substring(0, 3) == pattern)
                    {
                        return true;
                    }
                }
            }
            //GPFx260

            return false;
        }

        private static string ConvertFromLectraVersionToGerberVersion(string[] src, string gerberExtension = ".gbr")
        {
            //GPIx269
            //------------------------------------------------
            // Overlap fix position. 
            //------------------------------------------------
            if(true)
            {
                int indexLineOverlapStart = 0;
                int indexLineOverlapStop = 0;
                string pattern = @"^014";
                for (int i = 0; i < src.Length; i++)
                {
                    var line = src[i];

                    var matches = Regex.Matches(line, pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
                        indexLineOverlapStart = i;
                        break;
                    }
                }
                for (int i = src.Length - 1; i > -1; i--)
                {
                    var line = src[i];

                    var matches = Regex.Matches(line, pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
                        indexLineOverlapStop = i;
                        break;
                    }
                }

                try
                {
                    int overlapArrayLength = indexLineOverlapStop - (indexLineOverlapStart - 5) + 1;

                    string[] overlapArray = new string[overlapArrayLength];

                    for (int i = 0; i < overlapArray.Length; i++)
                    {
                        overlapArray[i] = src[indexLineOverlapStart - 5 + i];
                    }

                    for (int i = 0; i < overlapArray.Length - 1; i++)       //elaborazione righe 014.
                    {
                        if (i > 4)
                        {
                            int cutPointStart = 8;
                            int spreadPointStart = 13;
                            int pointLength = 6;
                            string cutPoint = overlapArray[i + 1].Substring(cutPointStart, pointLength).Trim();
                            string spreadPoint = overlapArray[i].Substring(spreadPointStart, pointLength).Trim();
                            overlapArray[i] = overlapArray[i].ReplaceChars(spreadPoint, cutPointStart).ReplaceChars(cutPoint, spreadPointStart + 1);
                        }
                    }

                    src = src.RemoveLines(Enumerable.Range(indexLineOverlapStart - 5, overlapArrayLength));

                    for (int i = 0; i < overlapArray.Length - 1; i++)  //tolta la riga in più alla fine dell'elaborazione.
                    {
                        src = src.Append(overlapArray[i]).ToArray();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    return "";
                }            
            }
            //GPFx269

            const int StartIndexTotalGeneralAllowance = 13;
            const int StartIndexMarkerName = 18;
            const int StartIndexSectionStart = 33;
            const int StartIndexOrderId = 4;
            const int StartIndexSpreadNumber = 13;
            const int OrderIdLength = 8;

            int allowance = 0;
            int offset = 0;
            string orderId = "";

            //------------------------------------------------
            // Header
            //------------------------------------------------
            //Sections and Markers
            {
                var header = Constants.Gerber.Headers.Section201.SplitNewline();
                for (int i = 0; i < header.Length; i++)
                {
                    try
                    {
                        src[i + 1] = header[i];
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                        return "";
                    }
                }
            }

            //------------------------------------------------
            // Conversion 001
            //------------------------------------------------
            {
                string pattern = @"^001";

                const int MinimunLineLength = 50;

                for (int i = 0; i < src.Length; i++)
                {
                    var line = src[i];

                    var matches = Regex.Matches(line, pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
                        line = line.ReplaceChars("201", 0);

                        if (line.Length < MinimunLineLength)
                        {
                            line = line.PadRight(MinimunLineLength);
                        }

                        string markerName = line.Substring(StartIndexMarkerName, StartIndexSectionStart - StartIndexMarkerName).Trim();
#if VERBOSE
                        Console.WriteLine($"markerName: {markerName}");
#endif
                        if (markerName == "DUMMY")
                        {
                            line = line.ReplaceChars("*A*      ", StartIndexMarkerName);
                            line = line.ReplaceChars("     ", StartIndexSectionStart);
                        }
                        else
                        {
                            line = line.ReplaceChars($" {markerName}{gerberExtension}", StartIndexSectionStart);
                        }

                        src[i] = line;
                    }
                }
            }

            //------------------------------------------------
            // Header002
            //------------------------------------------------
            //Body
            {
                int indexLineStart002Section = 0;
                string pattern = @"^002";
                for (int i = 0; i < src.Length; i++)
                {
                    var line = src[i];

                    var matches = Regex.Matches(line, pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
                        indexLineStart002Section = i;
                        break;
                    }
                }

                orderId = src[indexLineStart002Section].Substring(StartIndexOrderId, StartIndexSpreadNumber - StartIndexOrderId).Trim();
#if VERBOSE
                Console.WriteLine($"OrderId: {orderId}");
#endif
                var header = Constants.Gerber.Headers.Section002.SplitNewline();
                for (int i = 0; i < header.Length; i++)
                {
                    src[i + indexLineStart002Section - header.Length] = header[i];
                }
            }

            //------------------------------------------------
            // Allowance 
            //------------------------------------------------
            //[1]
            {
                int indexLineAllowance = 0;
                string pattern = @"^004";
                for (int i = 0; i < src.Length; i++)
                {
                    var line = src[i];

                    var matches = Regex.Matches(line, pattern, RegexOptions.None);

                    if (matches.Count > 0)
                    {
                        indexLineAllowance = i;
                        break;
                    }
                }

                allowance = int.Parse(src[indexLineAllowance].Substring(StartIndexTotalGeneralAllowance, 4)) / 2;
                offset = allowance;

                src = src.RemoveLines(Enumerable.Range(indexLineAllowance - 3, 5));
            }

            //[2]   General Allowance
            {
                src = src.Append($"000").ToArray();

                var header = Constants.Gerber.Headers.Section007.SplitNewline();

                for (int i = 0; i < header.Length; i++)
                {
                    src = src.Append(header[i]).ToArray();
                }

                src = src.Append($"007 {orderId.PadRight(OrderIdLength)} {offset:0000} {offset:0000}").ToArray();
            }

            //3     Splice allowans
            {
                src = src.Append($"000").ToArray();

                var header = Constants.Gerber.Headers.Section118.SplitNewline();

                for (int i = 0; i < header.Length; i++)
                {
                    src = src.Append(header[i]).ToArray();
                }

                offset = 0; //MMIx54
                src = src.Append($"118 {orderId.PadRight(OrderIdLength)} {offset:0000} {offset:0000}").ToArray();
            }

            //------------------------------------------------
            // Date and Time 
            //------------------------------------------------
            //[1]   Date and Time of order
            {
                src = src.Append($"000").ToArray();

                var header = Constants.Gerber.Headers.Section008.SplitNewline();

                for (int i = 0; i < header.Length; i++)
                {
                    src = src.Append(header[i]).ToArray();
                }

                src = src.Append($"008 {orderId.PadRight(OrderIdLength)} 30121899 000000 30121899 000000").ToArray();
            }

            //[2]   Date and Time of transmition
            {
                src = src.Append($"000").ToArray();

                var header = Constants.Gerber.Headers.Section009.SplitNewline();

                for (int i = 0; i < header.Length; i++)
                {
                    src = src.Append(header[i]).ToArray();
                }

                src = src.Append($"009 {orderId.PadRight(OrderIdLength)} 30121899 000000 30121899 000000").ToArray();
            }

            return string.Join(Environment.NewLine, src);
        }
    }
}
