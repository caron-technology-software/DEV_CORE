using System;
using System.Collections.Generic;
using System.Linq;

using OpenCvSharp;

namespace Caron.FileFormats.Gerber
{
    public struct GerberPointCommand
    {
        public int IdxGerberCommand;
        public int CurrentNumberOfPiece;
        public Point Point;
        public int Knife;
        public int Pen;

        public GerberPointCommand(int idxGerberCommand, int currentNumberOfPiece, Point point, int knife, int pen)
        {
            IdxGerberCommand = idxGerberCommand;
            CurrentNumberOfPiece = currentNumberOfPiece;
            Point = point;
            Knife = knife;
            Pen = pen;
        }
    }
}
