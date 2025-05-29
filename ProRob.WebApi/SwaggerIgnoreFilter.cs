using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;
//MMIx67
namespace ProRob.WebApi
{
    public class SwaggerIgnoreFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            var pathsToRemove = new List<string>();

            foreach (var apiDescription in apiExplorer.ApiDescriptions)
            {
                var action = apiDescription.ActionDescriptor;

                var controllerHasIgnore = action.ControllerDescriptor.GetCustomAttributes<SwaggerIgnoreAttribute>().Any();
                var actionHasIgnore = action.GetCustomAttributes<SwaggerIgnoreAttribute>().Any();

                if (controllerHasIgnore || actionHasIgnore)
                {
                    var path = "/" + apiDescription.RelativePath.TrimEnd('/');
                    path = path.Contains("?") ? path.Substring(0, path.IndexOf("?")) : path;

                    pathsToRemove.Add(path);
                }
            }

            foreach (var path in pathsToRemove)
            {
                swaggerDoc.paths.Remove(path);
            }
        }
    }
}
