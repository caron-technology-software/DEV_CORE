using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cradle.Proxy.Api.ApiController
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthAttribute : Attribute, IAuthorizationFilter
    {
        private const string Realm = "PROROB API";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var path = context.HttpContext.Request.Path.Value;

            if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase))
                return;

            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic "))
            {
                context.Result = new UnauthorizedResult();
                context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{Realm}\"";
                return;
            }

            var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
            var credentialBytes = Convert.FromBase64String(encodedCredentials);
            var credentials = System.Text.Encoding.UTF8.GetString(credentialBytes).Split(':');

            if (credentials.Length != 2)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var username = credentials[0];
            var password = credentials[1];

            if (!ProxyApiController.Authenticator(username, password))
            {
                context.Result = new UnauthorizedResult();
                context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{Realm}\"";
                return;
            }

            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, "Basic");
            var principal = new ClaimsPrincipal(identity);
            context.HttpContext.User = principal;
        }
    }

}
