using PaperlessAPI.api.Borders.Adapters;
using PaperlessAPI.api.Borders.Adapters.Interfaces;

namespace PaperlessAPI.api.Configuration
{
    public static class AdaptersConfig
    {
        public static IServiceCollection ConfigureAdapters(this IServiceCollection services) => services
            .AddScoped<IReportAdapter, ReportAdapter>();
    }
}
