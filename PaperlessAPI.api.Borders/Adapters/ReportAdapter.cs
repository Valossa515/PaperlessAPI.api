using PaperlessAPI.api.Borders.Adapters.Interfaces;
using PaperlessAPI.api.Borders.Dtos.Requests;
using PaperlessAPI.api.Borders.Entities;

namespace PaperlessAPI.api.Borders.Adapters
{
    public class ReportAdapter : IReportAdapter
    {
        public DynamicReportEntity ToReport(DynamicReportEntityRequest request)
            => new(
                request.Title,
                request.ColumnHeaders,
                request.Rows,
                request.CreatedAt)
            {
                Title = request.Title,
                ColumnHeaders = request.ColumnHeaders,
                Rows = request.Rows,
                CreatedAt = request.CreatedAt
            };
    }
}