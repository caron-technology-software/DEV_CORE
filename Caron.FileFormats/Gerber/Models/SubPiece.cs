using System;
using System.Collections.Generic;
using System.Linq;

using OpenCvSharp;

namespace Caron.FileFormats.Gerber
{
    public class SubPiece
    {
        public List<Point> Points { get; set; }

        public SubPiece()
        {
            this.Points = new List<Point>();
        }
    }
}
