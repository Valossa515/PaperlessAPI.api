using Microsoft.AspNetCore.Mvc;
using PaperlessAPI.api.Borders.Dtos;
using PaperlessAPI.api.Borders.Dtos.Requests;
using PaperlessAPI.api.Borders.Handlers;
using PaperlessAPI.api.Shared.Models;
using System.Net;

namespace PaperlessAPI.api.Controllers
{
    [ApiController]
    [Route("report/v1")]
    public class ReportController(IActionResultConverter actionResultConverter) : ControllerBase
    {

        private readonly IActionResultConverter _actionResultConverter = actionResultConverter;

        [HttpPost]
        [Route("generate")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(DynamicReportEntityDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Message[]))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(Message[]))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Message[]))]
        public async Task<IActionResult> GenerateReport(DynamicReportEntityRequest request,
            [FromServices] ICreateReportHandler useCase)
        {
            var response = await useCase.Execute(request);
            return _actionResultConverter.Convert(response);
        }
    }
}