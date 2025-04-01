using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using PaperlessAPI.api.Borders.Database;
using PaperlessAPI.api.Repositories;
using PaperlessAPI.api.Repositories.Database;
using PaperlessAPI.api.Shared.Models;
using Serilog;

namespace PaperlessAPI.api.Configuration
{
    public static class ServiceConfig
    {
        public static void ConfigureApplication(this IServiceCollection services, WebApplicationBuilder builder, TelemetryConfiguration teleConfig)
        {
            var applicationConfig = builder.Configuration.Get<ApplicationConfig>();

            if (applicationConfig == null)
            {
                throw new InvalidOperationException("ApplicationConfig cannot be null");
            }

            services.AddLogging(configure => configure.AddSerilog(Log.Logger));

            services
                .AddSingleton(applicationConfig)
                .AddSingleton(Log.Logger)
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<IActionResultConverter, ActionResultConverter>()
                .AddSingleton<IDatabaseFactory, DatabaseFactory>()
                .AddSingleton(new TelemetryClient(teleConfig))
                .ConfigureAdapters()
                .ConfigureHandlers()
                .ConfigureValidators()
                .ConfigureRepositories();
        }
    }
}
