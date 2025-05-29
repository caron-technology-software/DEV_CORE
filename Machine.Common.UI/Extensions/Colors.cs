using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace Machine.UI
{
    public static class ColorExtensions
    {
        public static Scalar ToScalar(this Color color)
        {
            return new Scalar(color.R, color.G, color.B);
        }
    }
}
