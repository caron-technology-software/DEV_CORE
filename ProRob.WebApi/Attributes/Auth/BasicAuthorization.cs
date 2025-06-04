
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ProRob.WebApi.Auth
{
    public class BasicAuthorizationAttribute : AuthorizationFilterAttribute
    {
        private static Func<string, string, bool> authenticator = null;

        private string Username { get; set; }
        private string Password { get; set; }

        private const string Realm = "PROROB API";

        public static void SetAuthenticatorService(Func<string, string, bool> service)
        {
            authenticator = service;
        }

        static BasicAuthorizationAttribute()
        {
            //--
        }

        public BasicAuthorizationAttribute()
        {
            //--
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                   actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (authenticator is null)
            {
                return;
            }

            if (SkipAuthorization(actionContext))
            {
                return;
            }

            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
            }
            else
            {
                // Gets header parameters  
                string authenticationString = actionContext.Request.Headers.Authorization.Parameter;
                string originalString = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationString));

                // Gets username and password  
                string username = originalString.Split(':')[0];
                string password = originalString.Split(':')[1];

                // Validate username and password  
                if (authenticator(username, password))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, username)
                        // Add more claims if needed: Roles, ...
                    };

                    var identity = new ClaimsIdentity(claims, "Auth");

                    IPrincipal user = new ClaimsPrincipal(identity);

                    Thread.CurrentPrincipal = user;
                }
                else
                {
                    //-------------------------------------
                    // Returns unauthorized error  
                    //-------------------------------------

                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    actionContext.Response.Content = new StringContent("<html>\n<body>\n<pre><h1>Unauthorized</h1></pre>\n</body>\n</html>");
                    actionContext.Response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                }
            }

            base.OnAuthorization(actionContext);
        }
    }
}