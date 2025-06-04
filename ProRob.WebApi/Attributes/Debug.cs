using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProRob.WebApi
{
    public class TicToc : ActionFilterAttribute
    {
        private const string StopwatchKey = "__TICTOC__";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items[StopwatchKey] = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Items[StopwatchKey] is Stopwatch sw)
            {
                sw.Stop();
                var controllerName = context.Controller.GetType().Name;
                Console.WriteLine($"[{controllerName}] Elapsed time: {sw.Elapsed.TotalMilliseconds} ms");
            }
        }
    }

    public class PrintRemoteIpAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine($"[{ip}]");
        }
    }

    public class PrintSourceUrlAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var url = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
            Console.WriteLine($"url: {url}");
        }
    }

    public class BeepAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Task.Run(() => Console.Beep());
        }
    }
}