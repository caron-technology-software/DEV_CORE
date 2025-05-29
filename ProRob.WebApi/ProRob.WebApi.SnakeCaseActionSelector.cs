using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ProRob.WebApi
{
    public class SnakeCaseActionSelector : ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            try
            {
                //Verifico presenza di parametri nell'url
                if (controllerContext.Request.GetQueryNameValuePairs().Count() == 0)
                {
                    return base.SelectAction(controllerContext);
                }

                var newUri = CreateNewUri(
                    controllerContext.Request.RequestUri,
                    controllerContext.Request.GetQueryNameValuePairs());

                controllerContext.Request.RequestUri = newUri;

                return base.SelectAction(controllerContext);
            }
            catch
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
        }

        private Uri CreateNewUri(Uri requestUri, IEnumerable<KeyValuePair<string, string>> queryPairs)
        {
            var currentQuery = requestUri.Query;
            var newQuery = ConvertQueryToCamelCase(queryPairs);
            var builder = new UriBuilder(requestUri)
            {
                Query = newQuery
            };

            return builder.Uri;
        }

        private static string ConvertQueryToCamelCase(IEnumerable<KeyValuePair<string, string>> queryPairs)
        {
            queryPairs = queryPairs
                .Select(x => new KeyValuePair<string, string>(x.Key.ToCamelCase(), x.Value));

            return queryPairs
                .Select(x => String.Format("{0}={1}", x.Key, x.Value))
                .Aggregate((x, y) => x + "&" + y);
        }

    }
}