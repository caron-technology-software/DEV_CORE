using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProRob.WebApi
{
    public static class Extensions
    {
        public static string GetClientIpAddress(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";
        }

        public static string ToCamelCase(this string source)
        {
            var parts = source
                .Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            return parts
                .First().ToLower() +
                String.Join("", parts.Skip(1).Select(ToCapital));
        }

        public static string ToCapital(this string source)
        {
            return String.Format("{0}{1}", char.ToUpper(source[0]), source.Substring(1).ToLower());
        }
    }
}
