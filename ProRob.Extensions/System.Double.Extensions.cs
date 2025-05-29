using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.Extensions.Double
{
    public static class DoubleExtensions
    {
        public static bool NearlyEquals(this double value1, double value2, double epsilon = 0.0001)
        {
            return Math.Abs(value1 - value2) < epsilon;
        }
    }
}
