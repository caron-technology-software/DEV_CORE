using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenCvSharp;

namespace Caron
{
    internal class Color
    {
        private static readonly List<Scalar> colors;
        public static int NumberOfColors { get; private set; }

        static Color()
        {
            colors = new List<Scalar>
                {
                    Scalar.LightBlue,
                    Scalar.LightSalmon,
                    Scalar.LightGreen,
                    Scalar.LightPink,
                    Scalar.LightSteelBlue
                };


            NumberOfColors = colors.Count();
        }

        public static Scalar GetColor(int n)
        {
            int idx = n % NumberOfColors;
            //Console.WriteLine($"GetColor: {n}-{idx}");
            return colors[idx];
        }
    }
}
