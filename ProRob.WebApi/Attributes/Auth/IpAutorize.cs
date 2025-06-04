
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
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
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ProRob.WebApi
{
    public class IpAuthorizeAttribute : Attribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
        private readonly string _ipAllowed;

        public IpAuthorizeAttribute(string ipAllowed)
        {
            _ipAllowed = ipAllowed;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var endpoint = context.ActionDescriptor.EndpointMetadata;
            if (endpoint.OfType<Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute>().Any())
                return;

            var remoteIp = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            if (!string.Equals(remoteIp, _ipAllowed, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }

}