using FluentValidation;
using Microsoft.Extensions.Logging;
using PaperlessAPI.api.Borders.Adapters.Interfaces;
using PaperlessAPI.api.Borders.Dtos;
using PaperlessAPI.api.Borders.Dtos.Requests;
using PaperlessAPI.api.Borders.Dtos.Responses;
using PaperlessAPI.api.Borders.Handlers;
using PaperlessAPI.api.Borders.Repositories;
using PaperlessAPI.api.Borders.Services;
using PaperlessAPI.api.Shared.Handlers;
using PaperlessAPI.api.Shared.Properties;

namespace PaperlessAPI.api.Handlers.Reports
{
    public class ReportsHandler(
        IReportsRepository reportsRepository,
        IReportPdfGeneratorService reportPdfGeneratorService,
        IReportAdapter reportAdapter,
        IValidator<DynamicReportEntityRequest> validator,
        ILogger<ReportsHandler> logger) : HandlerBase<DynamicReportEntityRequest, CreateReportResponse>(logger, validator), ICreateReportHandler
    {
        private readonly IReportPdfGeneratorService reportPdfGenerator = reportPdfGeneratorService;
        private readonly IReportsRepository _reportsRepository = reportsRepository;
        private readonly IReportAdapter _reportAdapter = reportAdapter;

        public override async Task<HandlerResponse<CreateReportResponse>> DoExecute(DynamicReportEntityRequest request)
        {
            var report = _reportAdapter.ToReport(request);
            var reportId = await _reportsRepository.InsertAsync(report);

            if (reportId is null)
                return BadRequest(Resources.CreateReportErrorCode, Resources.CreateReportError);

            var reportDto = new DynamicReportEntityDto(
                reportId.Id,
                report.Title,
                report.ColumnHeaders,
                report.Rows,
                request.CreatedAt
            );

            var pdfBytes = reportPdfGenerator.GeneratePdf(reportDto);

            var response = new CreateReportResponse(reportDto, pdfBytes);

            return Success(response);
        }
    }
}