using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using OpenCvSharp;

namespace Caron.FabricsFileFormats
{
    public partial class GerberFileFormat
    {
        public partial class CutFile
        {
            public bool InternationalSystemOfUnits = false;
            public void ParseGerberCommands(string[] gerberCommands)
            {
                parserStatus = new ParserStatus();

                parsingErrors.Clear();
                commandsNotParsed.Clear();

                knifePieces = new List<Piece>();
                penPieces = new List<Piece>();

                int lastIdxCommandOvercutAndAdvance = 0;

                for (int idxGerberCommand = 0; idxGerberCommand < gerberCommands.Length; idxGerberCommand++)
                {
                    //--------------------------------------------------------------
                    parserStatus.IdxCurrentGerberCommand = idxGerberCommand;
                    string gerberCommand = gerberCommands[idxGerberCommand];
                    //--------------------------------------------------------------

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
                    else
                    {
                        commandsNotParsed.Add(gerberCommand);
                    }

                    //-------------------------------------------------------------------------


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
                    //Console.WriteLine($"\t{gerberCommand}");

                    //Restore Gerber Parser Status
                    parserStatus.PrecKnife = parserStatus.CurrentKnife;
                    parserStatus.PrecPen = parserStatus.CurrentPen;
                    parserStatus.PointAdded = false;
                    //-------------------------------------------------------------------------
                }

                //Console.WriteLine("commandsNotParsed:");
                //commandsNotParsed.ForEach(x => { Console.WriteLine(x); });
            }
        }
    }
}
