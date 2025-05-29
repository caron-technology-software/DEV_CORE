using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProRob.DataBase.MySql.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToMySqlString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}