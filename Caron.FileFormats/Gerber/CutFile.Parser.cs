using Caron.FileFormats.Gerber.NonParametric;
using Caron.FileFormats.Gerber.Parametric;
using Caron.FileFormats.Gerber.Parametric.Coalescend;
using OpenCvSharp;
using ProRob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caron.FileFormats.Gerber
{
    public partial class GerberFileFormat
    {
        public partial class CutFile
        {
            private ParserStatus parserStatus;
            private readonly List<string> parsingErrors = new List<string>();
            private readonly List<string> commandsNotParsed = new List<string>();

            public GerberMeasurementSystem InternationalSystemOfUnits { get; set; } //MMIx18
            public List<string> ParsingErrors { get => parsingErrors; }
            public List<string> CommandsNotParsed { get => commandsNotParsed; }
            public void RescaleAllPoints(double scalingFactor)
            {
                int nKnifePoints = knifePieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points).Count();
                int nPenPoints = penPieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points).Count();

                if (nKnifePoints > 0)
                {
                    GerberHelper.RescalePiecesPoints(knifePieces, scalingFactor);
                }

                if (nPenPoints > 0)
                {
                    GerberHelper.RescalePiecesPoints(penPieces, scalingFactor);
                }
            }
            public (List<Piece>, List<Piece>) GetKnifeAndPenPoints()
            {
                return (knifePieces, penPieces);
            }

            //MMIx16
            private List<Piece> knifePieces;
            private List<Piece> penPieces;

            private static readonly IReadOnlyCollection<(string Command, Func<string, GerberSimpleCommand> Factory)> _nonParametricCommands =
            new List<(string Command, Func<string, GerberSimpleCommand> Factory)>
            {
                (GerberCommandKind.A, token => new GerberCommandA(token)),
                (GerberCommandKind.B, token => new GerberCommandB(token)),
                (GerberCommandKind.D1, token => new GerberCommandD1(token)),
                (GerberCommandKind.D2, token => new GerberCommandD2(token)),
                (GerberCommandKind.D4, token => new GerberCommandD4(token)),
                (GerberCommandKind.E, token => new GerberCommandE(token)),
                (GerberCommandKind.G04, token => new GerberCommandG04(token)),
                (GerberCommandKind.G70, token => new GerberCommandG70(token)),
                (GerberCommandKind.G71, token => new GerberCommandG71(token)),
                (GerberCommandKind.G91, token => new GerberCommandG91(token)),
                (GerberCommandKind.L, token => new GerberCommandL(token)),
                (GerberCommandKind.M0, token => new GerberCommandM0(token)),
                (GerberCommandKind.M00, token => new GerberCommandM00(token)),
                (GerberCommandKind.M01, token => new GerberCommandM01(token)),
                (GerberCommandKind.M14, token => new GerberCommandM14(token)),
                (GerberCommandKind.M15, token => new GerberCommandM15(token)),
                (GerberCommandKind.M17, token => new GerberCommandM17(token)),
                (GerberCommandKind.M18, token => new GerberCommandM18(token)),
                (GerberCommandKind.M19, token => new GerberCommandM19(token)),
                (GerberCommandKind.M20, token => new GerberCommandM20(token)),
                (GerberCommandKind.M25, token => new GerberCommandM25(token)),
                (GerberCommandKind.M26, token => new GerberCommandM26(token)),
                (GerberCommandKind.M30, token => new GerberCommandM30(token)),
                (GerberCommandKind.M31, token => new GerberCommandM31(token)),
                (GerberCommandKind.M40, token => new GerberCommandM40(token)),
                (GerberCommandKind.M41, token => new GerberCommandM41(token)),
                (GerberCommandKind.M42, token => new GerberCommandM42(token)),
                (GerberCommandKind.M43, token => new GerberCommandM43(token)),
                (GerberCommandKind.M44, token => new GerberCommandM44(token)),
                (GerberCommandKind.M46, token => new GerberCommandM46(token)),
                (GerberCommandKind.M47, token => new GerberCommandM47(token)),
                (GerberCommandKind.M48, token => new GerberCommandM48(token)),
                (GerberCommandKind.M51, token => new GerberCommandM51(token)),
                (GerberCommandKind.M60, token => new GerberCommandM60(token)),
                (GerberCommandKind.M61, token => new GerberCommandM61(token)),
                (GerberCommandKind.M62, token => new GerberCommandM62(token)),
                (GerberCommandKind.M63, token => new GerberCommandM63(token)),
                (GerberCommandKind.M64, token => new GerberCommandM64(token)),
                (GerberCommandKind.M65, token => new GerberCommandM65(token)),
                (GerberCommandKind.M66, token => new GerberCommandM66(token)),
                (GerberCommandKind.M67, token => new GerberCommandM67(token)),
                (GerberCommandKind.M68, token => new GerberCommandM68(token)),
                (GerberCommandKind.M69, token => new GerberCommandM69(token)),
                (GerberCommandKind.M70, token => new GerberCommandM70(token)),
                (GerberCommandKind.O, token => new GerberCommandO(token)),
                (GerberCommandKind.R, token => new GerberCommandR(token)),
                (GerberCommandKind.Slash, token => new GerberCommandSlash(token)),
                (GerberCommandKind.Star, token => new GerberCommandStar(token))
            };
            
            private static readonly IReadOnlyCollection<(string Command, Func<string, GerberSimpleCommand> Factory)> _parametricCommands =
            new List<(string Command, Func<string, GerberSimpleCommand> Factory)>
            {
                
                (GerberCommandKind.Q, token => new GerberCommandQ(token)),
                (GerberCommandKind.H, token => new GerberCommandH(token)),
                (GerberCommandKind.F, token => new GerberCommandF(token)),
                (GerberCommandKind.X, token => new GerberCommandX(token)),
                (GerberCommandKind.Y, token => new GerberCommandY(token)),
                (GerberCommandKind.N, token => new GerberCommandN(token)),
                (GerberCommandKind.Z, token => new GerberCommandZ(token))
                
            };

            private readonly static Regex OvercutsStitchingRegex_M19_D2_M15_M14 =
            new Regex(@"(X[0-9]+Y[0-9]+)\*M19\*X[0-9]+Y[0-9]+\*D2\*M15\*\1\*M14", RegexOptions.Compiled);

            private readonly static Regex OvercutsStitchingRegex_M15_M19_M14_M15_M14 =
            new Regex(@"(X[0-9]+Y[0-9]+)\*M15\*M19\*M14\*X[0-9]+Y[0-9]+\*M15\*\1\*M14", RegexOptions.Compiled);

            private readonly static Regex OvercutsStitchingRegex_M14_M19_M15_M14 =
            new Regex(@"M14\*(X[0-9]+Y[0-9]+)\*M19\*X[0-9]+Y[0-9]+\*M15\*\1\*M14", RegexOptions.Compiled);

            private readonly static Regex OvercutsStitchingRegex_M19_M15 =
            new Regex(@"(X[0-9]+Y[0-9]+)\*M19\*X[0-9]+Y[0-9]+\*M15\*\1", RegexOptions.Compiled);

            private readonly static Regex OvercutsStitchingRegex_D2_M15_M19_M14_D2_M15 =
            new Regex(@"(X[0-9]+Y[0-9]+)\\*D2\\*M15\\*M19\\*X[0-9]+Y[0-9]+\\*M14\\*X[0-9]+Y[0-9]+\\*D2\\*M15\\*\\1", RegexOptions.Compiled);

            private readonly static Regex FixUselessKnifeUpAndDownRegex =
            new Regex(@"M15\\*M14", RegexOptions.Compiled);

            private readonly static Regex FixMissingStarRegex =
            new Regex(@"(X[0-9]+Y[0-9]+)M(14|15|43)", RegexOptions.Compiled);

            private readonly static Regex RemoveDuplicatedKnifeUpRegex =
            new Regex(@"M15\\*M15", RegexOptions.Compiled);

            private readonly static Regex InvertM14WithPreviousCoordinatesWhenPreviousStarIsMissingRegex =
            new Regex(@"(X[0-9]+Y[0-9]+)M(14)", RegexOptions.Compiled);

            public string Clean(string content)
            {
                if (content == null) throw new ArgumentNullException(nameof(content));

                var rawBlocks = content.Trim().Split(new[] { '*', '\r', '\n' }, 1 | 2); 
                var cleaned = string.Join("*", rawBlocks);
                cleaned = OvercutsStitchingRegex_M19_D2_M15_M14.Replace(cleaned, string.Empty);
                cleaned = OvercutsStitchingRegex_M15_M19_M14_M15_M14.Replace(cleaned, string.Empty);
                cleaned = OvercutsStitchingRegex_M14_M19_M15_M14.Replace(cleaned, "M14*$1");
                cleaned = OvercutsStitchingRegex_M19_M15.Replace(cleaned, "$1");
                cleaned = OvercutsStitchingRegex_D2_M15_M19_M14_D2_M15.Replace(cleaned, "$1");
                cleaned = InvertM14WithPreviousCoordinatesWhenPreviousStarIsMissingRegex.Replace(cleaned, "M$2*$1");
                cleaned = FixMissingStarRegex.Replace(cleaned, "$1*M$2");
                cleaned = FixUselessKnifeUpAndDownRegex.Replace(cleaned, "M14");
                cleaned = RemoveDuplicatedKnifeUpRegex.Replace(cleaned, "M15");
                return cleaned;
            }
            private static void CoalesceCommands(GerberBlock block)
            {
                do
                {
                    var mustContinue = false;
                    for (var index = 0; index < block.Commands.Count - 1; index++)
                    {
                        var currentCommand = block.Commands[index];
                        var nextCommand = block.Commands[index + 1];
                        if (currentCommand is GerberCommandX xCommand && nextCommand is GerberCommandY yCommand)
                        {
                            var xyCommand = new GerberCommandXY
                            {
                                X = xCommand,
                                Y = yCommand
                            };

                            // replace the 2 distinct "X" and "Y" commands with a unified "XY" command
                            block.Commands.Insert(index, xyCommand);
                            block.Commands.Remove(currentCommand);
                            block.Commands.Remove(nextCommand);

                            mustContinue = true;
                        }
                    }

                    // if no alterations has been made to the commands list, exit the coalescing process
                    if (!mustContinue)
                        break;
                } while (true);
            }
            private static bool FillCommand(GerberCommand? previousCommand, string token)
            {
                if (previousCommand is GerberCommandM31 m31)
                {
                    m31.Text = token;
                    return true;
                }

                if (previousCommand is GerberCommandM20 m20)
                {
                    m20.Text = token;
                    return true;
                }

                return false;
            }
            private bool ProcessToken(GerberFile file, GerberBlock block, string token)
            {
                if (string.IsNullOrWhiteSpace(token))
                    return false;

                // is this a non-parametric command, not followed by other commands in the same block ?
                var resolvedMapping = _nonParametricCommands.FirstOrDefault(kvp => token == kvp.Command);
                if (!string.IsNullOrWhiteSpace(resolvedMapping.Command) && resolvedMapping.Factory != null)
                {
                    try
                    {
                        var command = resolvedMapping.Factory(token);
                        block.Commands.Add(command);
                        return true;
                    }
                    catch (Exception exc)
                    {
                        throw new InvalidOperationException($"Unsupported file format. Cannot parse the command '{token}'");
                    }
                }

                // is this a parametric command ?    
                resolvedMapping = _parametricCommands.FirstOrDefault(kvp => token.StartsWith(kvp.Command, StringComparison.InvariantCulture));
                if (!string.IsNullOrWhiteSpace(resolvedMapping.Command) && resolvedMapping.Factory != null)
                {
                    try
                    {
                        var command = resolvedMapping.Factory(token);
                        block.Commands.Add(command);

                        // if the parametric command is followed by other commands, let's process them too (recursively) and enqueue them into the same block
                        if (!string.IsNullOrWhiteSpace(command.TrailingContent))
                        {
                            ProcessToken(file, block, command.TrailingContent);
                        }

                        return true;
                    }
                    catch (Exception exc)
                    {
                        throw new InvalidOperationException($"Unsupported file format. Cannot parse the command '{token}'");
                    }
                }

                // if we didn't recognize the command, maybe this is an argument/parameter of the previous command
                var previousCommand = block.Commands.LastOrDefault();
                if (FillCommand(previousCommand, token))
                    return true;

                // if we didn't recognize the command, maybe this is an argument/parameter of the last command of the PREVIOUS BLOCK
                var previousBlock = file.Blocks.LastOrDefault();
                if (previousBlock != null)
                {
                    var lastCommandInPreviousBlock = previousBlock.Commands?.LastOrDefault();
                    if (FillCommand(lastCommandInPreviousBlock, token))
                        return false; // this is a "non-block", because it's purpose is just to complete the initialization of the previous one
                }

                // we couldn't recognize the command
                var unknownCommand = new GerberUnknownCommand(token);
                block.Commands.Add(unknownCommand);
                return true;
            }
            private void ParseAndAddBlock(GerberFile file, string blockContent)
            {
                if (string.IsNullOrWhiteSpace(blockContent)) throw new ArgumentException("Content cannot be null or whitespace.", nameof(blockContent));

                var block = new GerberBlock();
                if (!ProcessToken(file, block, blockContent))
                    return; // this is a "non-block", because it's purpose is just to complete the initialization of the previous one, so we must not add it

                foreach (var command in block.Commands)
                {
                    if (command is GerberCommandG70)
                    {
                        file.UnitOfMeasureSystem = GerberMeasurementSystem.Imperial_1_1000_Inch;
                        break;
                    }

                    if (command is GerberCommandG71)
                    {
                        file.UnitOfMeasureSystem = GerberMeasurementSystem.Metric_1_10_Millimeters;
                        break;
                    }
                }

                CoalesceCommands(block);

                file.Blocks.Add(block);
            }
            private void HandleKnifePiecesAndPenPieces(string gerberCommand)
            {
                if (string.Compare(gerberCommand, "M14") == 0 || string.Compare(gerberCommand, "B") == 0)
                {
                    parserStatus.CurrentKnife = Knife.Down;
                }
                else if (string.Compare(gerberCommand, "M15") == 0 || string.Compare(gerberCommand, "A") == 0)
                {
                    parserStatus.CurrentKnife = Knife.Up;
                }
                else if (string.Compare(gerberCommand, "D1") == 0)
                {
                    parserStatus.CurrentPen = Pen.Down;
                }
                else if (string.Compare(gerberCommand, "D2") == 0)
                {
                    parserStatus.CurrentPen = Pen.Up;
                }
                else if (gerberCommand.StartsWith("X"))
                {
                    try
                    {
                        int posY = gerberCommand.IndexOf('Y');
                        int x = int.Parse(new string(gerberCommand.Skip(1).Take(posY - 1).ToArray()));
                        int y = int.Parse(new string(gerberCommand.Skip(posY + 1).ToArray()));

                        parserStatus.CurrentPoint = new Point(x, y);
                        parserStatus.PointAdded = true;
                    }
                    catch
                    {
                        parsingErrors.Add(gerberCommand);
                    }
                }
                else if (gerberCommand.StartsWith("N"))
                {
                    int.TryParse(new string(gerberCommand.Skip(1).ToArray()), out parserStatus.CurrentPiece);
                    knifePieces.Add(new Piece());
                    penPieces.Add(new Piece());
                }

                // Aggiunge i punti nelle liste
                if (parserStatus.CurrentKnife == Knife.Down &&
                    parserStatus.CurrentKnife != parserStatus.PrecKnife &&
                    parserStatus.CurrentPiece >= 0)
                {
                    var point = new Point(parserStatus.CurrentPoint.X, parserStatus.CurrentPoint.Y);
                    knifePieces.Last().SubPieces.Add(new SubPiece());
                    knifePieces.Last().SubPieces.Last().Points.Add(point);
                }

                if (parserStatus.CurrentPen == Pen.Down &&
                    parserStatus.CurrentPen != parserStatus.PrecPen &&
                    parserStatus.CurrentPiece >= 0)
                {
                    var point = new Point(parserStatus.CurrentPoint.X, parserStatus.CurrentPoint.Y);
                    penPieces.Last().SubPieces.Add(new SubPiece());
                    penPieces.Last().SubPieces.Last().Points.Add(point);
                }

                if (parserStatus.PointAdded && parserStatus.CurrentPiece >= 0)
                {
                    var point = new Point(parserStatus.CurrentPoint.X, parserStatus.CurrentPoint.Y);
                    if (parserStatus.CurrentKnife == Knife.Down)
                    {
                        knifePieces.Last().SubPieces.Last().Points.Add(point);
                    }
                    else if (parserStatus.CurrentPen == Pen.Down)
                    {
                        penPieces.Last().SubPieces.Last().Points.Add(point);
                    }
                }

                // Ripristina lo stato precedente
                parserStatus.PrecKnife = parserStatus.CurrentKnife;
                parserStatus.PrecPen = parserStatus.CurrentPen;
                parserStatus.PointAdded = false;
            }
            public GerberFile ParseGerberCommands(string[] gerberCommands)
            //public void ParseGerberCommands(string[] gerberCommands)//←originale
            {
                parserStatus = new ParserStatus();
                parsingErrors.Clear();
                commandsNotParsed.Clear();

                knifePieces = new List<Piece>();
                penPieces = new List<Piece>();

                try
                {
                    
                    //unisco i comandi in una sola stringa e li pulisco
                    string content = string.Join("\n", gerberCommands);                  
                    content = Clean(content);
                    //separo i comandi puliti e rimuovo le stringhe vuote
                    string[] cleanedGerberCommands = content.Split(new[] {'*', '\r', '\n' },
                        StringSplitOptions.RemoveEmptyEntries).Select(command => command.Trim()).ToArray();
                    var file = new GerberFile();
                    //unità di misura di default
                    file.UnitOfMeasureSystem = GerberMeasurementSystem.Imperial_1_100_Inch;
                                       
                    foreach (var block in cleanedGerberCommands)
                    {            
                        //MMIx18
                        if(block != string.Empty)
                        {
                            ParseAndAddBlock(file, block);

                            // Popola le liste knifePieces e penPieces
                            HandleKnifePiecesAndPenPieces(block);

                            //raggiunta la fine del file
                            if (string.Equals(block, GerberCommandKind.M0, StringComparison.InvariantCultureIgnoreCase) ||
                                string.Equals(block, GerberCommandKind.M00, StringComparison.InvariantCultureIgnoreCase))
                                break;
                        }
                        //MMFx18
                    }
                    return file;
                    #region vecchio codice
                    //↓↓↓ VECCHIO CODICE ↓↓↓//
                    /*
                    int lastIdxCommandOvercutAndAdvance = 0;
                    for (int idxGerberCommand = 0; idxGerberCommand < gerberCommands.Length; idxGerberCommand++)
                    {
                        //--------------------------------------------------------------
                        parserStatus.IdxCurrentGerberCommand = idxGerberCommand;
                        string gerberCommand = gerberCommands[idxGerberCommand];
                        //--------------------------------------------------------------
                       
                        //if (gerberCommand.EndsWith("D2") || gerberCommand.EndsWith("M31"))
                        if (gerberCommand.EndsWith("M31"))    //GPIx230
                        {
                            ProConsole.WriteLine($"{gerberCommand}", ConsoleColor.Red);

                            if ((gerberCommands[idxGerberCommand + 1].Trim() == "") && (gerberCommands[idxGerberCommand + 2].Trim() == "D2")
                                && (gerberCommands[idxGerberCommand + 3].Trim() == "D2") && (gerberCommands[idxGerberCommand + 4].Trim() == "D1"))
                            {
                                gerberCommand = gerberCommand.Replace("M31", "");
                            }

                            //continue;\
                        }

                        if (gerberCommand.Length == 0)
                        {
                            //Console.WriteLine($"----> CMD LENGTH=0");
                            continue;
                        }

                        if (String.Compare(gerberCommand, "M19") == 0)
                        {
                            lastIdxCommandOvercutAndAdvance = idxGerberCommand;
                            //Console.WriteLine($"----> M19 CMD");
                        }

                        else if (String.Compare(gerberCommand, "B") == 0)
                        {
                            //Console.WriteLine($"Knife DOWN [{idxGerberCommand}]");
                            parserStatus.CurrentKnife = Knife.Down;
                        }

                        else if (String.Compare(gerberCommand, "A") == 0)
                        {
                            //Console.WriteLine($"Knife UP [{idxGerberCommand}]");
                            parserStatus.CurrentKnife = Knife.Up;
                        }

                        else if (String.Compare(gerberCommand, "M14") == 0)
                        {
                            //Console.WriteLine($"Knife DOWN [{idxGerberCommand}]");
                            parserStatus.CurrentKnife = Knife.Down;
                        }

                        else if (String.Compare(gerberCommand, "M15") == 0)
                        {
                            //Console.WriteLine($"Knife UP [{idxGerberCommand}]");
                            parserStatus.CurrentKnife = Knife.Up;
                        }

                        else if (String.Compare(gerberCommand, "D1") == 0)
                        {
                            //Console.WriteLine($"Pen DOWN [{idxGerberCommand}]");
                            parserStatus.CurrentPen = Pen.Down;
                        }

                        else if (String.Compare(gerberCommand, "D2") == 0)
                        {
                            //Console.WriteLine($"Pen UP [{idxGerberCommand}]");
                            parserStatus.CurrentPen = Pen.Up;
                        }

                        else if (String.Compare(gerberCommand, "G70") == 0)
                        {
                            InternationalSystemOfUnits = false;
                        }

                        else if (String.Compare(gerberCommand, "G71") == 0)
                        {
                            InternationalSystemOfUnits = true;
                        }

                        else if (gerberCommand[0] == 'X')
                        {
                            bool subCommand = false;

                            try
                            {
                                if ((gerberCommand.Substring(gerberCommand.Length - 3) == "M43") ||
                                   (gerberCommand.Substring(gerberCommand.Length - 3) == "M31"))
                                {
                                    gerberCommand = gerberCommand.Substring(0, gerberCommand.Length - 3);

                                    //Console.WriteLine($"----> M43 CMD");

                                    continue;
                                }
                                else if (gerberCommand.Substring(gerberCommand.Length - 3) == "D1")
                                {
                                    parserStatus.CurrentPen = Pen.Down;
                                    subCommand = true;
                                }
                                else if (gerberCommand.Substring(gerberCommand.Length - 3) == "D2")
                                {
                                    parserStatus.CurrentPen = Pen.Up;
                                    subCommand = true;
                                }
                                else if (String.Compare(gerberCommand, "B") == 0)
                                {
                                    //Console.WriteLine($"Knife DOWN [{idxGerberCommand}]");
                                    parserStatus.CurrentKnife = Knife.Down;
                                }

                                else if (String.Compare(gerberCommand, "A") == 0)
                                {
                                    //Console.WriteLine($"Knife UP [{idxGerberCommand}]");
                                    parserStatus.CurrentKnife = Knife.Up;
                                }
                                else if (gerberCommand.Substring(gerberCommand.Length - 3) == "M14")
                                {
                                    parserStatus.CurrentKnife = Knife.Down;
                                    subCommand = true;
                                }
                                else if (gerberCommand.Substring(gerberCommand.Length - 3) == "M15")
                                {
                                    parserStatus.CurrentKnife = Knife.Up;
                                    subCommand = true;
                                }

                                if (subCommand)
                                {
                                    gerberCommand = gerberCommand.Substring(0, gerberCommand.Length - 3);
                                }

                                int posY = gerberCommand.IndexOf('Y');
                                int x = int.Parse(new string(gerberCommand.Skip(1).Take(posY - 1).ToArray()));
                                int y = int.Parse(new string((gerberCommand.Skip(posY + 1)).ToArray()));

                                parserStatus.CurrentPoint.X = x;
                                parserStatus.CurrentPoint.Y = y;
                                parserStatus.PointAdded = true;
                            }
                            catch
                            {
                                parsingErrors.Add(gerberCommand);
                            }
                        }

                        else if (gerberCommand[0] == 'N')
                        {
                            int.TryParse(new string(gerberCommand.Skip(1).ToArray()), out parserStatus.CurrentPiece);

                            knifePieces.Add(new Piece());
                            penPieces.Add(new Piece());

                            //Console.WriteLine($"\n-------------------------------------\ncurrentPiece: {gerberParserStatus.currentPiece}\n-------------------------------------\n");
                        }
                        else if ((String.Compare(gerberCommand, "M0") == 0) || (String.Compare(gerberCommand, "M00") == 0))
                        {
                            //raggiunta la fine del file
                            break;
                        }
                        else
                        {
                            commandsNotParsed.Add(gerberCommand);
                        }

                        //-------------------------------------------------------------------------
                        //Inserimento di linee e punti

                        if (parserStatus.CurrentKnife == Knife.Down &&
                            parserStatus.CurrentKnife != parserStatus.PrecKnife &&
                            parserStatus.CurrentPiece >= 0)
                        {
                            var point = new Point(parserStatus.CurrentPoint.X, parserStatus.CurrentPoint.Y);

                            knifePieces.Last().SubPieces.Add(new SubPiece());
                            knifePieces.Last().SubPieces.Last().Points.Add(point);

                        }

                        if (parserStatus.CurrentPen == Pen.Down &&
                            parserStatus.CurrentPen != parserStatus.PrecPen &&
                            parserStatus.CurrentPiece >= 0)
                        {
                            var point = new Point(parserStatus.CurrentPoint.X, parserStatus.CurrentPoint.Y);

                            penPieces.Last().SubPieces.Add(new SubPiece());
                            penPieces.Last().SubPieces.Last().Points.Add(point);

                        }

                        if (parserStatus.PointAdded && parserStatus.CurrentPiece >= 0)
                        {
                            //Console.WriteLine($"current_piece: {gerberParserStatus.currentPiece}");
                            var point = new Point(parserStatus.CurrentPoint.X, parserStatus.CurrentPoint.Y);
                            if (parserStatus.CurrentKnife == Knife.Down)
                            {
                                //Console.WriteLine("Knife!");
                                knifePieces.Last().SubPieces.Last().Points.Add(point);
                            }
                            else if (parserStatus.CurrentPen == Pen.Down)
                            {
                                //Console.WriteLine("Pen!");
                                penPieces.Last().SubPieces.Last().Points.Add(point);
                            }
                        }
                        Console.WriteLine($"\t{gerberCommand} - {idxGerberCommand}");   //GPIx230

                        //Restore Gerber Parser Status
                        parserStatus.PrecKnife = parserStatus.CurrentKnife;
                        parserStatus.PrecPen = parserStatus.CurrentPen;
                        parserStatus.PointAdded = false;
                        //-------------------------------------------------------------------------
                    }
                    */
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }               
            }
            //MMFx16
        }
    }
}
