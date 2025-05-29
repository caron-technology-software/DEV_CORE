using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.UI
{
    public static class Zoom
    {
        public static bool Enabled { get; set; } = false;

        public static double X { get; set; } = 0;
        public static double Y { get; set; } = 0;
        public static int W { get; set; } = 0;
        public static int H { get; set; } = 0;

        public static ImageSize ImageSize { get; set; }
    }
}
