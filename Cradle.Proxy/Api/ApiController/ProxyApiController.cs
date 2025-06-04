using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using Flurl;
using Flurl.Http;

using ProRob.WebApi;
using ProRob.WebApi.Auth;

namespace Cradle.Proxy
{
    public class ProxyApiController : ControllerBase
    {
        public static IUserAuthetification UserAuthentification = new UserAuthentification();
        public static Func<string, string, bool> Authenticator = (user, pass) => { return UserAuthentification.CheckCredential(user, pass); };

        public static volatile int NumberOfRequests = 0;
        public static readonly DateTime StartUp;

        static ProxyApiController()
        {
            BasicAuthorizationAttribute.SetAuthenticatorService(Authenticator);

            StartUp = DateTime.Now;

            ProRob.ProConsole.WriteLine("[ProxyApiController] Initialization completed", ConsoleColor.DarkYellow);
        }

        public ProxyApiController()
        {
            NumberOfRequests++;
        }

        internal static T ProxyGetRequest<T>(string route, params QueryParam[] queryParams)
        {
            try
            {
                var uri = Machine.Constants.Networking.WebApiUri.AppendPathSegment(route);

                foreach (var param in queryParams)
                {
                    uri = uri.SetQueryParam(param.Name, param.Value);
                };

                return uri.GetJsonAsync<T>().Result;
            }
            catch
            {
                return default;
            }
        }

        internal static HttpResponseMessage ProxyGetRequest(string route, params QueryParam[] queryParams)
        {
            var uri = Machine.Constants.Networking.WebApiUri.AppendPathSegment(route);

            foreach (var param in queryParams)
            {
                uri = uri.SetQueryParam(param.Name, param.Value);
            };

            return uri.AllowAnyHttpStatus().GetAsync().Result.ResponseMessage;
        }

        internal static HttpResponseMessage ProxyPostRequest(string route, object value, params QueryParam[] queryParams)
        {
            var uri = Machine.Constants.Networking.WebApiUri.AppendPathSegment(route);

            foreach (var param in queryParams)
            {
                uri = uri.SetQueryParam(param.Name, param.Value);
            };

            return uri.AllowAnyHttpStatus().PostJsonAsync(value).Result.ResponseMessage;
        }

        //GPIx6
        //internal static HttpResponseMessage ProxyHtmlContentTypeRequest(string route)
        //{
        //    try
        //    {
        //        var content = Machine.Constants.Networking.WebApiUri
        //               .AppendPathSegment(route)
        //               .AllowAnyHttpStatus()
        //               .GetStringAsync().Result;

        //        var response = new HttpResponseMessage();
        //        response.Content = new StringContent(content);
        //        response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
        //        return response;
        //    }
        //    catch
        //    {
        //        return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
        //    }
        //}
        internal static HttpResponseMessage ProxyHtmlContentTypeRequest(string route, params QueryParam[] queryParams)
        {
            try
            {
                var uri = Machine.Constants.Networking.WebApiUri.AppendPathSegment(route);

                Console.WriteLine($"URI semplice: {uri}");

                foreach (var param in queryParams)
                {
                    uri = uri.SetQueryParam(param.Name, param.Value);
                };

                Console.WriteLine($"URI con query string: {uri}");

                var content = uri.AllowAnyHttpStatus().GetStringAsync().Result;

                //var content = Machine.Constants.Networking.WebApiUri
                //       .AppendPathSegment(route)
                //       .AllowAnyHttpStatus()
                //       .GetStringAsync().Result;

                var response = new HttpResponseMessage();
                response.Content = new StringContent(content);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return response;
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }
        }
        //GPFx6
    }
}