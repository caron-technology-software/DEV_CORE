using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;
//MMIx67
public class FixGenericSchemaIdsFilter : IDocumentFilter
{
    public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
    {
        var refMap = swaggerDoc.definitions.Keys.ToDictionary(
            key => key,
            key => SanitizeKey(key)
        );

        // Rinomina le definizioni
        var updatedDefinitions = new Dictionary<string, Schema>();
        foreach (var kvp in swaggerDoc.definitions)
        {
            var newKey = refMap[kvp.Key];
            updatedDefinitions[newKey] = kvp.Value;
        }
        swaggerDoc.definitions = updatedDefinitions;

        // Correggi riferimenti nei paths
        foreach (var path in swaggerDoc.paths.Values)
        {
            foreach (var operation in new[] { path.get, path.put, path.post, path.delete, path.options, path.head, path.patch })
            {
                if (operation == null) continue;

                if (operation.parameters != null)
                {
                    foreach (var param in operation.parameters)
                    {
                        var schemaProp = param.GetType().GetProperty("schema");
                        if (schemaProp != null)
                        {
                            var schema = schemaProp.GetValue(param) as Schema;
                            if (schema != null && !string.IsNullOrEmpty(schema.@ref))
                            {
                                schema.@ref = FixRef(schema.@ref, refMap);
                            }
                        }
                    }
                }

                foreach (var response in operation.responses.Values)
                {
                    if (response.schema != null && !string.IsNullOrEmpty(response.schema.@ref))
                    {
                        response.schema.@ref = FixRef(response.schema.@ref, refMap);
                    }

                    // Se è un array di oggetti referenziati
                    if (response.schema?.items?.@ref != null)
                    {
                        response.schema.items.@ref = FixRef(response.schema.items.@ref, refMap);
                    }
                }
            }
        }

        // Correggi riferimenti interni alle definizioni
        foreach (var schema in swaggerDoc.definitions.Values)
        {
            if (schema.properties == null) continue;

            foreach (var prop in schema.properties)
            {
                if (!string.IsNullOrEmpty(prop.Value?.@ref))
                {
                    prop.Value.@ref = FixRef(prop.Value.@ref, refMap);
                }

                if (prop.Value?.items?.@ref != null)
                {
                    prop.Value.items.@ref = FixRef(prop.Value.items.@ref, refMap);
                }
            }
        }
    }

    private string SanitizeKey(string key)
    {
        return key;
    }

    private string FixRef(string originalRef, Dictionary<string, string> refMap)
    {
        foreach (var kvp in refMap)
        {
            var plainKey = kvp.Key;
            var encodedKey = Uri.EscapeDataString(plainKey); // encode [ e ]
            var expectedRef = $"#/definitions/{encodedKey}";
            if (originalRef == $"#/definitions/{plainKey}")
            {
                return expectedRef;
            }
        }
        return originalRef;

    }
}



