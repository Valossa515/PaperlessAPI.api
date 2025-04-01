using Microsoft.AspNetCore.Mvc;

namespace PaperlessAPI.api.Borders.Dtos.Responses
{
    public class CreateReportResponse : FileContentResult
    {
        public DynamicReportEntityDto Report { get; set; }

        public CreateReportResponse(DynamicReportEntityDto report, byte[] pdfBytes)
            : base(pdfBytes, "application/pdf")
        {
            Report = report;
            FileDownloadName = $"{report.Title}.pdf";
        }
    }
}