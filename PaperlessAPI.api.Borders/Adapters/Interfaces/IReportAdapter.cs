using PaperlessAPI.api.Borders.Dtos.Requests;
using PaperlessAPI.api.Borders.Entities;

namespace PaperlessAPI.api.Borders.Adapters.Interfaces
{
    public interface IReportAdapter
    {
        DynamicReportEntity ToReport(DynamicReportEntityRequest request);
    }
}
