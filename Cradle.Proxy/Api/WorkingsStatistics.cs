using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;

using ProRob.WebApi;
using ProRob.WebApi.Auth;

namespace Cradle.Proxy.Api
{
    [BasicAuthorization]
    [RoutePrefix("workings_statistics")]
    public class WorkingsStatisticsController : ProxyApiController
    {
        [HttpGet]
        [Route("workings")]
        public HttpResponseMessage Workings(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            string uri = "workings_statistics";
            var queryParams = new QueryParam[] { new QueryParam("fromDate", fromDate), new QueryParam("toDate", toDate) };

            return ProxyGetRequest(uri, queryParams);
        }

        [HttpGet]
        [Route("daily")]
        public HttpResponseMessage DailyWorkingsStatistics(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            string uri = "workings_statistics/daily";
            var queryParams = new QueryParam[] { new QueryParam("fromDate", fromDate), new QueryParam("toDate", toDate) };

            return ProxyGetRequest(uri, queryParams);
        }

        [HttpGet]
        [Route("material")]
        public HttpResponseMessage GetMaterialGroupByDay(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            string uri = "workings_statistics/material";
            var queryParams = new QueryParam[] { new QueryParam("fromDate", fromDate), new QueryParam("toDate", toDate) };

            return ProxyGetRequest(uri, queryParams);
        }

        [HttpGet]
        [Route("material_code")]
        public HttpResponseMessage GetMaterialCodeGroupByDay(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            string uri = "workings_statistics/material_code";
            var queryParams = new QueryParam[] { new QueryParam("fromDate", fromDate), new QueryParam("toDate", toDate) };

            return ProxyGetRequest(uri, queryParams);
        }
    }
}
