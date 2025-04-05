namespace PaperlessAPI.api.Borders.Dtos.Requests
{
    public record DynamicReportEntityRequest(
        string Title,
        IEnumerable<string> ColumnHeaders,
        IEnumerable<IEnumerable<string>> Rows,
        DateTime CreatedAt)
    {
        /// <summary>
        /// Titulo do relatório
        /// </summary>
        public string Title { get; set; } = Title;
        /// <summary>
        /// Cabeçalhos do relatório
        /// </summary>
        public IEnumerable<string> ColumnHeaders { get; set; } = ColumnHeaders;
        /// <summary>
        /// linhas do relatório
        /// </summary>
        public IEnumerable<IEnumerable<string>> Rows { get; set; } = Rows;
        /// <summary>
        /// Data de criação do relatório
        /// </summary>
        public DateTime CreatedAt { get; set; } = CreatedAt;
    }
}