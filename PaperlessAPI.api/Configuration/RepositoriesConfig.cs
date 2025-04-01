using PaperlessAPI.api.Borders.Database;
using PaperlessAPI.api.Borders.Repositories;
using PaperlessAPI.api.Repositories;
using PaperlessAPI.api.Repositories.Database;

namespace PaperlessAPI.api.Configuration
{
    public static class RepositoriesConfig
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services) => services
            .AddScoped<IReportsRepository, ReportsRepository>()
            .AddSingleton<IDatabaseFactory, DatabaseFactory>();
    }
}