using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;

namespace PaperlessAPI.api.Extensions
{
    public class SwaggerJsonIgnore : IOperationFilter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var ignoredProperties = context.MethodInfo?.GetParameters()
                 .SelectMany(p => p.ParameterType.GetProperties()
                                  .Where(prop => prop.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                                  );
            if (ignoredProperties != null && ignoredProperties.Any())
            {
                foreach (var property in ignoredProperties)
                {
                    operation.Parameters = operation.Parameters
                        .Where(p => !p.Name.Equals(property.Name, StringComparison.InvariantCulture))
                        .ToList();
                }
            }
        }
    }
}
