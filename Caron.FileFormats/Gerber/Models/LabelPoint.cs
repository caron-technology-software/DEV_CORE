using System;
using System.Collections.Generic;
using System.Linq;

using OpenCvSharp;

namespace Caron.FileFormats.Gerber
{
    public class LabelPoint
    {
        public Point Point { get; set; } = new Point();
        public string Label { get; set; }

        public LabelPoint()
        {
            //--
        }

        public LabelPoint(int x, int y, string text)
        {
            var point = new Point(x, y);

            Point = point;
            Label = text;
        }
    }
}
