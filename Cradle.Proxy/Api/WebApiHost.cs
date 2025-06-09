using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ProRob.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cradle.Proxy.Api
{
    public static class WebApiHost
    {
        public static async Task RunAsync(string url, CancellationToken token)
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = Array.Empty<string>(),
                ContentRootPath = AppContext.BaseDirectory,
                WebRootPath = "wwwroot",
                ApplicationName = typeof(WebApiHost).Assembly.FullName
            });

            // Disattiva il log di sistema
            builder.Logging.ClearProviders();

            builder.WebHost.UseUrls(url);

            // Controller + JSON con Newtonsoft
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver
                    {
                        IgnoreSerializableAttribute = true
                    };
                });

            builder.Services.AddEndpointsApiExplorer();

            // Swagger + Basic Auth config
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "YORK API", Version = "v1" });
                c.CustomSchemaIds(type => type.FullName);

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Auth header (username & password)."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.Value ?? "";

                // Escludi swagger e statici
                if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase) ||
                    path.StartsWith("/favicon", StringComparison.OrdinalIgnoreCase) ||
                    path.StartsWith("/index.html"))
                {
                    await next();
                    return;
                }

                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic "))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"PROROB API\"";
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                try
                {
                    var encoded = authHeader.Substring("Basic ".Length).Trim();
                    var credentialBytes = Convert.FromBase64String(encoded);
                    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

                    if (credentials.Length != 2 || !ProxyApiController.Authenticator(credentials[0], credentials[1]))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"PROROB API\"";
                        await context.Response.WriteAsync("Invalid credentials");
                        return;
                    }

                    // Successo
                    var claims = new[] { new Claim(ClaimTypes.Name, credentials[0]) };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    context.User = new ClaimsPrincipal(identity);

                    await next();
                }
                catch
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"PROROB API\"";
                    await context.Response.WriteAsync("Malformed Authorization header");
                }
            });

            // Swagger UI + endpoint
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "YORK API V1");
                c.RoutePrefix = "swagger"; // opzionale
            });

            // Routing & Controller mapping
            app.MapControllers();

            await app.RunAsync(token);
        }

    }
}
