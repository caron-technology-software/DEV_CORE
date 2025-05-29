using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ProRob.WebApi
{
    public class TicToc : ActionFilterAttribute
    {
        private static DateTime timestamp;
        private static string controllerName;

        public override void OnActionExecuting(HttpActionContext context)
        {
            timestamp = DateTime.Now;
            controllerName = context.ControllerContext.ControllerDescriptor.ControllerName;
        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            Console.WriteLine($"[{controllerName}] Elapsed time: {(DateTime.Now - timestamp).TotalMilliseconds} ms\n");
        }
    }

    public class PrintRemoteIp : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            var ip = context.Request.GetOwinContext().Request.RemoteIpAddress;

            Console.WriteLine($"[{ip}]");
        }
    }

    public class PrintSourceUrl : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            var url = context.Request.RequestUri;

            Console.WriteLine($"url:{url}");
        }
    }

    public class Beep : ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext context)
        {

        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            Task.Run(() => { Console.Beep(); });
        }
    }

}