using System;
using System.Web.Http;
//aggiunte 29/06/2022:
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;

using System.Threading;
using System.Threading.Tasks;

using ProRob;
using ProRob.WebApi;
using ProRob.Log;

using Machine;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("heartbeat")]
    public class HeartbeatController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Heartbeat()
        {
            var heartbeat = new Heartbeat()
            {
                ApplicationName = ApplicationInfo.ApplicationName,
                ApplicationVersion = ApplicationInfo.ApplicationVersion,
                Uptime = MachineController.Uptime
            };

            return Ok(heartbeat);
        }
    }
}
