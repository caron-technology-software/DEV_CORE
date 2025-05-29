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
        public static bool IsMatVersion(string[] src)
        {
            //------------------------------------------------
            // Find not default commands
            //------------------------------------------------
            string pattern = @"997";
            string pattern1 = @".mat";

            for (int i = 0; i < src.Length; i++)
            {
                if ((src[i].StartsWith(pattern)) || (src[i].Contains(pattern1)))//MMIx46
                {
                    return true;
                }
            }

            return false;
        }

        private static string ConvertFromMatVersionToGerberVersion(string[] src, string gerberExtension = ".gbr")
        {
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
            {
                string result = string.Join("\r\n", src);
                int found = 0;
                found.ToString();
                found = result.IndexOf("\r\n");
                if (!(found>0))
                {
                    return "";
                }
                int found2 = 0;
                found2.ToString();
                found2 = result.IndexOf("\r\n001");
                if (!(found2 > 0))
                {
                    return "";
                }
                string leftS = result.Substring(0, found+2) + Constants.Gerber.Headers.Section201 + result.Substring(found2+2);
                var src_modify = leftS.SplitNewline();
                src = src_modify;

                var header = Constants.Gerber.Headers.Section201.SplitNewline();
                //for (int i = 0; i < header.Length; i++)
                //{
                //    src[i + 1] = header[i];
                //}
            }

            //------------------------------------------------
            // Conversion 001
            //------------------------------------------------
            {
                string result = string.Join("\r\n", src);
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
            {
                string result = string.Join("\r\n", src);
                int found = 0;
                found.ToString();
                found = result.IndexOf("\r\n002");
                if (!(found > 0))
                {
                    return "";
                }
                int found2 = 0;
                found2.ToString();
                found2 = result.IndexOf("\r\n004");
                if (!(found2 > 0))
                {
                    return "";
                }
                int found4 = 0;
                found4.ToString();
                found4 = result.IndexOf("\r\n997");
                if (!(found4 > 0))
                {
                    return "";
                }

                string s004 = result.Substring(found2 + 2);
                int found3 = 0;
                found3.ToString();
                found3 = s004.IndexOf("\r\n");
                if (!(found3 > 0))
                {
                    return "";
                }
                s004 = s004.Substring(0, found3 + 2);

                string s997 = result.Substring(found4 + 2);
                int found5 = 0;
                found5.ToString();
                found5 = s997.IndexOf("\r\n");
                //GPIx32
                if (!(found5 > 0))
                {
                    s997 = s997.Substring(0, s997.Length);
                    //return "";
                }
                else
                {
                    s997 = s997.Substring(0, found5 + 2);
                }
                //GPFx32

                string Section004 =
                    @"000 Order name
                    000 |        Total General Allowance (Half on each side)
                    000 V        V
                    ";
                string leftS = result.Substring(0, found + 2) + "000\r\n" + Section004 + s004 + "000\r\n" + Constants.Gerber.Headers.Section002 
                    + result.Substring(found + 2).Replace(s004, "").Replace(s997, "");
                var src_modify = leftS.SplitNewline();
                src = src_modify;

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
                //for (int i = 0; i < header.Length; i++)
                //{
                //    src[i + indexLineStart002Section - header.Length] = header[i];
                //}
            }

            //------------------------------------------------
            // Allowance 
            //------------------------------------------------
            //[1]
            {
                string result = string.Join("\r\n", src);
                int found = 0;
                found.ToString();
                found = result.IndexOf("\r\n014");
                //GPIx32
                string Section014;
                string[] src_modify;
                if (!(found > 0))
                {
                    //return "";
                    string leftS = result.Substring(0, result.Length);
                    src_modify = leftS.SplitNewline();
                }
                else
                {
                    Section014 =
@"000
000 Overlap zone number
000 |   Cut point
000 |   |     Spread point
000 V   V     V
";
                    string leftS = result.Substring(0, found + 2) + Section014 + result.Substring(found + 2);
                    src_modify = leftS.SplitNewline();
                }
                //GPFx32

                src = src_modify;

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

            //[2]
            {
                src = src.Append($"000").ToArray();

                var header = Constants.Gerber.Headers.Section007.SplitNewline();

                for (int i = 0; i < header.Length; i++)
                {
                    src = src.Append(header[i]).ToArray();
                }

                src = src.Append($"007 {orderId.PadRight(OrderIdLength)} {offset:0000} {offset:0000}").ToArray();
            }

            //3
            {
                src = src.Append($"000").ToArray();

                var header = Constants.Gerber.Headers.Section118.SplitNewline();

                for (int i = 0; i < header.Length; i++)
                {
                    src = src.Append(header[i]).ToArray();
                }

                src = src.Append($"118 {orderId.PadRight(OrderIdLength)} {offset:0000} {offset:0000}").ToArray();
            }

            //------------------------------------------------
            // Allowance 
            //------------------------------------------------
            //[1]
            {
                src = src.Append($"000").ToArray();

                var header = Constants.Gerber.Headers.Section008.SplitNewline();

                for (int i = 0; i < header.Length; i++)
                {
                    src = src.Append(header[i]).ToArray();
                }

                src = src.Append($"008 {orderId.PadRight(OrderIdLength)} 30121899 000000 30121899 000000").ToArray();
            }

            //[2]
            {
                src = src.Append($"000").ToArray();

                var header = Constants.Gerber.Headers.Section009.SplitNewline();

                for (int i = 0; i < header.Length; i++)
                {
                    src = src.Append(header[i]).ToArray();
                }

                src = src.Append($"009 {orderId.PadRight(OrderIdLength)} 30121899 000000 30121899 000000").ToArray();
            }

            //string result1 = string.Join("\r\n", src);

            //GPIx269
            //------------------------------------------------
            // Overlap fix position. 
            //------------------------------------------------
            if (false)   //disabilitato nel mat per ora, attendo conferma BREDA per modifica!!!    (invertire i numeri non togliere primo e ultimo come per lectra!!!!)
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

                int overlapArrayLength = indexLineOverlapStop - (indexLineOverlapStart - 5) + 1;

                string[] overlapArray = new string[overlapArrayLength];

                for (int i = 0; i < overlapArray.Length; i++)
                {
                    overlapArray[i] = src[indexLineOverlapStart - 5 + i];
                }

                for (int i = 0; i < overlapArray.Length; i++)		//elaborazione righe 014.
                {
                    if (i > 4)
                    {
                        int cutPointStart = 8;
                        int spreadPointStart = 13;
                        int pointLength = 6;
                        string cutPoint = overlapArray[i].Substring(cutPointStart, pointLength).Trim();
                        string spreadPoint = overlapArray[i].Substring(spreadPointStart, pointLength).Trim();
                        overlapArray[i] = overlapArray[i].ReplaceChars(spreadPoint, cutPointStart).ReplaceChars(cutPoint, spreadPointStart + 1);
                    }
                }

                src = src.RemoveLines(Enumerable.Range(indexLineOverlapStart - 5, overlapArrayLength));

                for (int i = 0; i < overlapArray.Length; i++)  //tolta la riga in più alla fine dell'elaborazione.
                {
                    src = src.Append(overlapArray[i]).ToArray();
                }
            }
            //GPFx269

            return string.Join(Environment.NewLine, src);
        }

    }
}
