using Microsoft.ApplicationInsights.Extensibility;
using PaperlessAPI.api.Configuration;
using QuestPDF.Infrastructure;
using Serilog;
using Serilog.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

var services = builder.Services;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
     .MinimumLevel.Information()
     .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
     .MinimumLevel.Override("System", LogEventLevel.Warning)
     .MinimumLevel.Override("Worker", LogEventLevel.Warning)
     .MinimumLevel.Override("Host", LogEventLevel.Warning)
     .MinimumLevel.Override("Function", LogEventLevel.Warning)
     .MinimumLevel.Override("DurableTask", LogEventLevel.Warning)
     .Enrich.FromLogContext()
     .Enrich.WithMachineName()
     .Enrich.WithProcessId()
     .Enrich.WithThreadId()
     .WriteTo.Console()
     .WriteTo.ApplicationInsights(
         TelemetryConverter.Events,
         LogEventLevel.Information)
    .Filter.ByExcluding(log =>
        log.Properties.ContainsKey("SourceContext") &&
        log.Properties["SourceContext"].ToString().Contains("Serilog.AspNetCore.RequestLoggingMiddleware") &&
        log.Properties.ContainsKey("StatusCode") &&
        log.Properties["StatusCode"].ToString().Contains("404") &&
        log.Properties.ContainsKey("RequestPath") &&
        log.Properties["RequestPath"].ToString().Contains('/'))
     .CreateLogger();

try
{
    Log.Information("Starting PaperlessAPI Apis application - {EnvironmentName}", builder.Environment.EnvironmentName);

    // 1. Configure basic services - REMOVENDO AuthorizeFilter
    services.AddControllers() // Removido options.Filters.Add(new AuthorizeFilter())
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)));
    services.AddEndpointsApiExplorer();

    // 2. Configure Swagger
    services.ConfigureSwagger(builder);

    // 3. Configure application services (including IDatabaseFactory)
    var telemetryConfig = new TelemetryConfiguration(); // Configure this properly
    services.ConfigureApplication(builder, telemetryConfig);

    // Build the application
    var app = builder.Build();

    // 4. Now configure database (after all services are registered)
    await services.ConfigureDatabase(app);

    // 5. Configure middleware pipeline
    if (!builder.Environment.IsProduction())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors();
    app.UseRouting();
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
