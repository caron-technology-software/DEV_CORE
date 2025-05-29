using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.Utility
{
    public class EnduranceLimit
    {
        public static bool Check(int limit, int endurance)
        {
            if (limit > 0 && endurance > limit)
            {
                return true;
            }

            return false;
        }

        public static bool Check(float limit, float endurance)
        {
            if (limit > 0.1f && endurance > limit)
            {
                return true;
            }

            return false;
        }

        public static bool Check(double limit, double endurance)
        {
            if (limit > 0.1 && endurance > limit)
            {
                return true;
            }

            return false;
        }
    }
}
