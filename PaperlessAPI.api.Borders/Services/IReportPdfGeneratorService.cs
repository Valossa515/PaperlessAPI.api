using PaperlessAPI.api.Borders.Dtos;

namespace PaperlessAPI.api.Borders.Services
{
    public interface IReportPdfGeneratorService
    {
        byte[] GeneratePdf(DynamicReportEntityDto report);
    }
}