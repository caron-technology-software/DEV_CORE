using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

[assembly: OwinStartup(typeof(ProRob.WebApi.WebApiStartup))]

namespace ProRob.WebApi
{
    public class WebApiStartup
    {
        public void Configuration(IAppBuilder app)
        {
            //MMIx67 Abilita CORS per tutte le origini
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //MMFx67

            HttpConfiguration config = new HttpConfiguration();

            // Enable Attribute Routing
            config.MapHttpAttributeRoutes();

            //MMIx67
            config
            .EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "YORK API");
                // ↓ per evitare prblemi di ID duplicati che si creano quando ci sono 2 classi con nome uguale in manespace diversi
                c.UseFullTypeNameInSchemaIds();
                // ↓ Esclude controller/metodi decorati con [SwaggerIgnore]
                c.DocumentFilter<SwaggerIgnoreFilter>();
                // ↓ Sostituisce i caratteri che swagger non accetta come le parentesi graffe
                c.DocumentFilter<FixGenericSchemaIdsFilter>();
            })
            .EnableSwaggerUi(c =>
               c.DisableValidator() // disabilita il pulsante validate
            );
            //MMFx67

            // Json output
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            config.Services.Replace(typeof(IHttpActionSelector), new SnakeCaseActionSelector());

            ////Filters
            //config.Filters.Add(new BasicAuthorizationAttribute());

            //Json Converters
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            //Json Contract Resolver
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            ((DefaultContractResolver)(config.Formatters.JsonFormatter.SerializerSettings.ContractResolver)).IgnoreSerializableAttribute = true;

            //Verifica della configurazione
            config.EnsureInitialized();

            app.UseWebApi(config);
        }
    }
}