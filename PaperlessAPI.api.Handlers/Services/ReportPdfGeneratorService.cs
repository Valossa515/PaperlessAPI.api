using PaperlessAPI.api.Borders.Dtos;
using PaperlessAPI.api.Borders.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace PaperlessAPI.api.Handlers.Services
{
    public class ReportPdfGeneratorService : IReportPdfGeneratorService
    {
        public byte[] GeneratePdf(DynamicReportEntityDto report)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Header().PaddingBottom(30).Element(container =>
                    {
                        container.AlignCenter()
                            .Text(report.Title)
                            .FontSize(24)
                            .FontColor(Colors.Blue.Darken4)
                            .Bold()
                            .Underline();
                    });

                    page.Content().Table(table =>
                    {
                        // Define as colunas (igual número de cabeçalhos)
                        table.ColumnsDefinition(columns =>
                        {
                            foreach (var _ in report.ColumnHeaders)
                            {
                                columns.RelativeColumn();
                            }
                        });

                        // Cabeçalho da tabela
                        table.Header(header =>
                        {
                            foreach (var column in report.ColumnHeaders)
                            {
                                header.Cell()
                                    .BorderBottom(2)
                                    .Padding(8)
                                    .Background(Colors.Grey.Lighten2)
                                    .Text(column)
                                    .Bold()
                                    .FontColor(Colors.White)
                                    .AlignCenter();
                            }
                        });

                        // Linhas da tabela
                        foreach (var row in report.Rows)
                        {
                            foreach (var cell in row)
                            {
                                table.Cell()
                                    .BorderBottom(1)
                                    .Padding(8)
                                    .Text(cell)
                                    .FontSize(10)
                                    .AlignCenter();
                            }
                        }
                    });

                    page.Footer()
                        .AlignRight()
                        .Text($"Gerado em {DateTime.UtcNow:dd/MM/yyyy HH:mm:ss}")
                        .FontSize(10)
                        .FontColor(Colors.Grey.Darken1)
                        .Italic();
                });
            }).GeneratePdf();
        }
    }
}