namespace PaperlessAPI.api.Repositories
{
    public record ApplicationConfig
    {
        public ApplicationConfig()
        {
            ApplicationInsights = new Logging();
            ConnectionStrings = new ConnectionStrings();
        }

        public Logging ApplicationInsights { get; init; }
        public string[]? CorsOrigins { get; init; }
        public ConnectionStrings ConnectionStrings { get; init; }
        public int ProposalExpirationPeriodInDays { get; init; }
    }

    public record Logging
    {
        public string? ConnectionString { get; init; }
    }

    public record ConnectionStrings
    {
        public string? DefaultConnection { get; init; }
        public string? DatabaseName { get; init; }
    }
}