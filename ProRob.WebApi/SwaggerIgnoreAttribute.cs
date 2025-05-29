using System;
//MMIx67
namespace ProRob.WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SwaggerIgnoreAttribute : Attribute
    {
    }
}
