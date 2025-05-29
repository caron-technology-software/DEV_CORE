using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProRob.Extensions
{
    public static class BoolExtensions
    {
        public static int ToInt(this bool value)
        {
            return Convert.ToInt32(value);
        }
    }
}