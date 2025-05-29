using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Flurl;
using Flurl.Http;

namespace ProRob.WebApi
{
    public class Rest
    {
        //var queryParams = new QueryParam[] { new QueryParam("title", title), new QueryParam("message", message) };

        public static T GetRequest<T>(string uri, params QueryParam[] queryParams)
        {
            foreach (var param in queryParams)
            {
                uri = uri.SetQueryParam(param.Name, param.Value);
            };

            T result = new Url(uri).GetJsonAsync<T>().Result;

            return result;
        }

        public static HttpResponseMessage GetRequest(string uri, params QueryParam[] queryParams)
        {
            foreach (var param in queryParams)
            {
                uri = uri.SetQueryParam(param.Name, param.Value);
            };

            var result = uri.AllowAnyHttpStatus().GetAsync().Result;

            return result.ResponseMessage;
        }
    }
}
