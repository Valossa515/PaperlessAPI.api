using FluentValidation;
using PaperlessAPI.api.Borders.Dtos.Requests;
using PaperlessAPI.api.Handlers.Validators;

namespace PaperlessAPI.api.Configuration
{
    public static class ValidatorsConfig
    {
        public static IServiceCollection ConfigureValidators(this IServiceCollection services) => services
            .AddScoped<IValidator<DynamicReportEntityRequest>, DynamicReportEntityRequestValidator>();
    }
}
