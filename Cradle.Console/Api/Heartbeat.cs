using System;
using Microsoft.AspNetCore.Mvc;
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
    [ApiController]
    [Route("heartbeat")]
    public class HeartbeatController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public IActionResult Heartbeat()
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
