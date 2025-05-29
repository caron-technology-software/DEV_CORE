using System;
using System.Collections.Generic;
using System.Linq;

using OpenCvSharp;

namespace Caron.FileFormats.Gerber
{
    internal class ParserStatus
    {
        public int IdxCurrentGerberCommand;
        public Knife CurrentKnife;
        public Pen CurrentPen;
        public Knife PrecKnife;
        public Pen PrecPen;
        public Point CurrentPoint;
        public int CurrentPiece;
        public bool PointAdded;

        public ParserStatus()
        {
            IdxCurrentGerberCommand = -1;
            CurrentKnife = Knife.Up;
            CurrentPen = Pen.Up;
            PrecKnife = Knife.Up;
            PrecPen = Pen.Up;
            CurrentPoint = new Point(-1, -1);
            CurrentPiece = -1;
            PointAdded = false;
        }
    }
}
