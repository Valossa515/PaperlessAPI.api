using Microsoft.OpenApi.Models;
using PaperlessAPI.api.Extensions;

namespace PaperlessAPI.api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, WebApplicationBuilder builder) =>
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"PaperlessAPI Apis - {builder.Environment.EnvironmentName}",
                });

                options.OperationFilter<SwaggerJsonIgnore>();

                List<string> xmlFiles = [.. Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)];
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
            });
    }
}