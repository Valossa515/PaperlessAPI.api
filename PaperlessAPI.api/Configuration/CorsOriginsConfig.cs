using PaperlessAPI.api.Repositories;

namespace PaperlessAPI.api.Configuration
{
    public static class CorsOriginsConfig
    {
        public static void AddCorsOrigins(this IServiceCollection services, ApplicationConfig applicationConfig)
        {
            if (applicationConfig.CorsOrigins?.Length > 0)
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.WithOrigins(applicationConfig.CorsOrigins)
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });
            }
        }
    }
}