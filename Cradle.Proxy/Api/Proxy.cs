using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

using ProRob.WebApi;
using ProRob.WebApi.Auth;
using Cradle.Proxy.Api.ApiController;

namespace Cradle.Proxy
{
    [ApiController]
    [BasicAuth]
    [Route("")]
    public class ProxyController : ProxyApiController
    {
        [HttpGet]
        [Route("heartbeat")]
        public HttpResponseMessage Heartbeat()
        {
            return ProxyGetRequest("heartbeat");
        }

        [HttpGet]
        [Route("endurance")]
        public HttpResponseMessage Endurance()
        {
            return ProxyGetRequest("endurance");
        }

        [HttpGet]
        [Route("control_status")]
        public HttpResponseMessage ControlStatus()
        {
            return ProxyGetRequest("control_status");
        }

        [HttpGet]
        [Route("time_series")]
        public HttpResponseMessage TimeSeries()
        {
            return ProxyGetRequest("time_series");
        }

        [HttpGet]
        [Route("commands/cut")]
        public HttpResponseMessage Cut()
        {
            Console.WriteLine("commands/cut");
            return ProxyGetRequest("external_commands/cut");
        }

        //GPIx23
        //[HttpGet]
        //[Route("commands/shutdownMachine")]
        //public HttpResponseMessage ShutdownMachine()
        //{
        //    Console.WriteLine("commands/shutdownMachine");
        //    return ProxyGetRequest("external_commands/shutdownMachine");
        //}
        //GPFx23

        [HttpGet]
        [Route("commands/cut/status")]
        public HttpResponseMessage GetCutStatus()
        {
            return ProxyGetRequest("external_commands/cut/status");
        }


        [HttpGet]
        [Route("commands/sync/status")]
        public HttpResponseMessage GetSyncStatus()
        {
            return ProxyGetRequest("external_commands/sync/status");
        }

        [HttpGet]
        [Route("commands/sync/enable")]
        public HttpResponseMessage EnableSync()
        {
            return ProxyGetRequest("external_commands/sync/enable");
        }

        [HttpGet]
        [Route("commands/sync/disable")]
        public HttpResponseMessage DisableSync()
        {
            return ProxyGetRequest("external_commands/sync/disable");
        }

        [HttpGet]
        [Route("commands/status")]
        public HttpResponseMessage Status()
        {
            return ProxyGetRequest("external_commands/status");
        }

        [HttpGet]
        [Route("message_box")]
        public HttpResponseMessage Message(string title, string message)
        {
            string uri = "message";
            var queryParams = new QueryParam[] { new QueryParam("title", title), new QueryParam("message", message) };

            return ProxyGetRequest(uri, queryParams);
        }

        [HttpGet]
        [Route("signal/ui")]
        public HttpResponseMessage SignalUI()
        {
            return ProxyGetRequest("signal/ui");
        }

        [HttpGet]
        [Route("application/reboot")]
        public HttpResponseMessage RebootMachine()
        {
            return ProxyGetRequest("application/reboot");
        }

        //GPIx23
        [HttpGet]
        [Route("application/shutDown")]
        public HttpResponseMessage ShutdownMachine()
        {
            Console.WriteLine("application/shutDown");
            return ProxyGetRequest("application/shutdown");
        }
        //GPFx23
    }
}
