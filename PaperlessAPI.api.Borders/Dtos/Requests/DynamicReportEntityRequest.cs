namespace PaperlessAPI.api.Borders.Dtos.Requests
{
    public record DynamicReportEntityRequest(
        string Title,
        IEnumerable<string> ColumnHeaders,
        IEnumerable<IEnumerable<string>> Rows,
        DateTime CreatedAt)
    {
        public string Title { get; set; } = Title;
        public IEnumerable<string> ColumnHeaders { get; set; } = ColumnHeaders;
        public IEnumerable<IEnumerable<string>> Rows { get; set; } = Rows;
        public DateTime CreatedAt { get; set; } = CreatedAt;
    }
}