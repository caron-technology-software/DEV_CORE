using System;
using System.Collections.Generic;
using System.Linq;

namespace Caron.FileFormats.Gerber
{
    public class Piece
    {
        public List<SubPiece> SubPieces;

        public Piece()
        {
            SubPieces = new List<SubPiece>();
        }
    }
}
