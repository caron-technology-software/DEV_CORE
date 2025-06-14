﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

using ProRob.WebApi.Auth;
using ProRob.WebApi;
using Cradle.Proxy.Api.ApiController;

namespace Cradle.Proxy.Api
{
    [BasicAuth]
    [Route("machine_events")]
    public class MachineEventsController : ProxyApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetMachineEvents(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            string uri = "machine_events";
            var queryParams = new QueryParam[] { new QueryParam("fromDate", fromDate), new QueryParam("toDate", toDate) };

            return ProxyGetRequest(uri, queryParams);
        }

        [HttpGet]
        [Route("stats")]
        public HttpResponseMessage GetMachineEventsStatistics(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            string uri = "machine_events/stats";
            var queryParams = new QueryParam[] { new QueryParam("fromDate", fromDate), new QueryParam("toDate", toDate) };

            return ProxyGetRequest(uri, queryParams);
        }
    }
}
