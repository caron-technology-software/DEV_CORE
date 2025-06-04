using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ProRob.WebApi
{
    public class Helpers
    {
        public static async Task<string> GetContentFromBody(HttpRequest request)
        {
            request.EnableBuffering();

            request.Body.Position = 0;

            using (StreamReader reader = new StreamReader(request.Body))
            {
                var content = await reader.ReadToEndAsync();

                request.Body.Position = 0;

                return content;
            }
        }

        //public static void PrintControllersList(Assembly assembly)
        //{
        //    //Assembly.GetExecutingAssembly()

        //    var result = assembly
        //                .GetTypes()
        //                .Where(type => typeof(ApiController).IsAssignableFrom(type))
        //                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
        //                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
        //                .GroupBy(x => x.DeclaringType.Name)
        //                .Select(x => new { Controller = x.Key, Actions = x.Select(s => s.Name).ToList() })
        //                .ToList();

        //    for (int i = 0; i < result.Count(); i++)
        //    {
        //        var c = result[i];
        //        ProConsole.WriteLine($"[{c.Controller}]", ConsoleColor.Red);

        //        for (int j = 0; j < c.Actions.Count; j++)
        //        {
        //            Console.WriteLine($"    {c.Actions[j]}");
        //        }
        //    }
        //}
    }
}