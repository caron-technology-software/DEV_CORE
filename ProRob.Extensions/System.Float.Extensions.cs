using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.Extensions.Float
{
    public static class FloatExtensions
    {
        public static bool NearlyEquals(this float value1, float value2, float epsilon = 0.0001f)
        {
            return Math.Abs(value1 - value2) < epsilon;
        }
    }
}
