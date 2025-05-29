using System;
using System.Collections.Generic;
using System.Linq;


namespace Caron.FileFormats.Gerber
{
    public partial class GerberFileFormat
    {
        public partial class CutFile
        {
            private ParserStatus parserStatus;
            private readonly List<string> parsingErrors = new List<string>();
            private readonly List<string> commandsNotParsed = new List<string>();

            private List<Piece> knifePieces;
            private List<Piece> penPieces;

            public bool InternationalSystemOfUnits { get; set; } = false;
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
        }
    }
}
