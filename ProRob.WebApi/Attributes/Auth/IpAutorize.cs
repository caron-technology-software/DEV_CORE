
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ProRob.WebApi
{
    public class IpAuthorizeAttribute : AuthorizationFilterAttribute
    {
        private readonly string ipAllowed;

        public IpAuthorizeAttribute(string ipAllowed)
        {
            this.ipAllowed = ipAllowed;
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                    actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any());
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext))
            {
                return;
            }

            var ip = actionContext.Request.GetOwinContext().Request.RemoteIpAddress;

            //Console.WriteLine($"DEBUG AUTH {ip} {ipAllowed}");

            // Validate username and password  
            if (String.Equals(ip, ipAllowed))
            {
                ////////var claims = new List<Claim>()
                ////////{
                ////////    new Claim("RemoteIpAddress",ip)
                ////////};

                ////////var identity = new ClaimsIdentity(claims, "Auth");
                ////////IPrincipal user = new ClaimsPrincipal(identity);

                ////////Thread.CurrentPrincipal = user;
            }
            else
            {
                // returns unauthorized error
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            base.OnAuthorization(actionContext);

        }
    }
}