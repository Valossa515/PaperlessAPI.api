using PaperlessAPI.api.Borders.Dtos.Requests;
using PaperlessAPI.api.Borders.Dtos.Responses;
using PaperlessAPI.api.Shared.Handlers;

namespace PaperlessAPI.api.Borders.Handlers
{
    public interface ICreateReportHandler : IHandler<DynamicReportEntityRequest, CreateReportResponse>;
}