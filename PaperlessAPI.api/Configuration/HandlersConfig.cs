using PaperlessAPI.api.Borders.Handlers;
using PaperlessAPI.api.Borders.Services;
using PaperlessAPI.api.Handlers.Reports;
using PaperlessAPI.api.Handlers.Services;

namespace PaperlessAPI.api.Configuration
{
    public static class HandlersConfig
    {
        public static IServiceCollection ConfigureHandlers(this IServiceCollection services) => services
            .AddScoped<ICreateReportHandler, ReportsHandler>()
            .AddTransient<IReportPdfGeneratorService, ReportPdfGeneratorService>();
    }
}