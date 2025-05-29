using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.UI.Controls.Extensions
{
    public static class FloatExtensions
    {
        public static bool NearlyEquals(this float value1, float value2, float epsilon = 0.0001f)
        {
            return Math.Abs(value1 - value2) < epsilon;
        }
    }

    public static class DoubleExtensions
    {
        public static bool NearlyEquals(this double value1, double value2, double epsilon = 0.0001)
        {
            return Math.Abs(value1 - value2) < epsilon;

        }
    }
}
