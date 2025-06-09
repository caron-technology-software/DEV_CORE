using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Cradle.Proxy.Api.ApiController;
using Microsoft.AspNetCore.Mvc;

using ProRob.WebApi;
using ProRob.WebApi.Auth;

namespace Cradle.Proxy
{
    [ApiController]
    [BasicAuth]
    [Route("logs")]
    public class LogsController : ProxyApiController
    {
        //GPIx6
        //[HttpGet]
        //[Route("control")]
        //public HttpResponseMessage LogControl()
        //{
        //    return ProxyHtmlContentTypeRequest("logs/control");
        //}
        [HttpGet]
        [Route("control")]
        public HttpResponseMessage LogControl(string DateX = null)
        {

            if (string.IsNullOrEmpty(DateX))
            {
                //DateX = DateTime.Now.Date.ToString();
                DateX = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                DateX = DateX.Replace("-", "");
            }

            Console.WriteLine($"DateX: {DateX}");

            string uri = "logs/control";
            var queryParams = new QueryParam[] { new QueryParam("DateX", DateX) };

            return ProxyHtmlContentTypeRequest(uri, queryParams);
        }
        //GPFx6

        /*
        #region Low Level
        [HttpGet]
        [Route("low_level/session")]
        public HttpResponseMessage LogsSessionLowLevel()
        {
            return ProxyHtmlContentTypeRequest("logs/low_level/session/consoleoutput/html");
        }

        //[HttpGet]
        //[Route("low_level/last_session")]
        //public HttpResponseMessage LogsLastSessionLowLevel()
        //{
        //    return ProxyHtmlContentTypeRequest("logs/low_level/last_session/consoleoutput/html");
        //}

        [HttpGet]
        [Route("low_level/{session}")]
        public HttpResponseMessage LogsSessionLowLevel(Guid session)
        {
            return ProxyHtmlContentTypeRequest($"logs/low_level/{session}/consoleoutput/html");
        }
        #endregion

        #region High Level
        [HttpGet]
        [Route("high_level/session")]
        public HttpResponseMessage LogsSessionHighLevel()
        {
            return ProxyHtmlContentTypeRequest("logs/high_level/session/all/html");
        }

        //[HttpGet]
        //[Route("high_level/last_session")]
        //public HttpResponseMessage LogsLastSessionHighLevel()
        //{
        //    return ProxyHtmlContentTypeRequest("logs/high_level/last_session/all/html");
        //}

        [HttpGet]
        [Route("high_level/{session}")]
        public HttpResponseMessage LogsSessionHighLevel(Guid session)
        {
            return ProxyHtmlContentTypeRequest($"logs/high_level/{session}/all/html");
        }
        #endregion

        #region UI
        [HttpGet]
        [Route("ui/session")]
        public HttpResponseMessage LogsSessionUI()
        {
            return ProxyHtmlContentTypeRequest("logs/ui/session/all/html");
        }

        //[HttpGet]
        //[Route("ui/last_session")]
        //public HttpResponseMessage LogsLastSessionUI()
        //{
        //    return ProxyHtmlContentTypeRequest("logs/ui/last_session/all/html");
        //}

        [HttpGet]
        [Route("ui/{session}")]
        public HttpResponseMessage LogsSessionUI(Guid session)
        {
            return ProxyHtmlContentTypeRequest($"logs/ui/{session}/all/html");
        }
        #endregion
        */
    }
}
