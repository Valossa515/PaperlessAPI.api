namespace PaperlessAPI.api.Borders.Dtos
{
    public record DynamicReportEntityDto(
        Guid Id,
        string Title,
        IEnumerable<string> ColumnHeaders,
        IEnumerable<IEnumerable<string>> Rows,
        DateTime CreatedAt)
    {
        public Guid Id { get; set; } = Id;
        public string Title { get; set; } = Title;
        public IEnumerable<string> ColumnHeaders { get; set; } = ColumnHeaders;
        public IEnumerable<IEnumerable<string>> Rows { get; set; } = Rows;
        public DateTime CreatedAt { get; set; } = CreatedAt;
    }
}