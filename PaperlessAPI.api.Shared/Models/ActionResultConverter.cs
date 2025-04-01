using Microsoft.AspNetCore.Mvc;
using PaperlessAPI.api.Shared.Handlers;
using PaperlessAPI.api.Shared.Helpers;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaperlessAPI.api.Shared.Models
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(HandlerResponse<T> response, bool withContentOnSuccess = true);
    }

    public class ActionResultConverter : IActionResultConverter
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
                {
                    new TrimmingJsonConverterHelper(),
                    new JsonStringEnumConverter()
                }
        };

        public IActionResult Convert<T>(HandlerResponse<T> response, bool withContentOnSuccess = true)
        {
            if (response == null)
                return BuildError(new[] { new ErrorMessage("000", "ActionResultConverter Error") }, 500);

            if (response.Messages is not null && response.Messages.Any())
                return BuildError(response.Messages, response.StatusCode);

            if (response.IsSuccess)
            {
                if (withContentOnSuccess)
                {
                    return BuildResult(response.Result, response.StatusCode);
                }

                return new NoContentResult();
            }

            if (!Equals(response.Result, default(T)))
                return BuildResult(response.Result, response.StatusCode);

            return BuildResult(response.Result, response.StatusCode);
        }

        private static JsonResult BuildError(object data, int statusCode)
        {
            Log.Error("[ERROR] {@Data}", data);

            return ToJsonResult(data, statusCode);
        }

        private static ActionResult BuildResult<T>(T data, int statusCode)
        {
            if (data is FileContentResult result)
                return new FileContentResult(result.FileContents, result.ContentType)
                {
                    FileDownloadName = result.FileDownloadName,
                };

            return ToJsonResult(data!, statusCode);
        }

        private static JsonResult ToJsonResult(object data, int statusCode)
        {
            var json = JsonSerializer.Serialize(data, _jsonSerializerOptions);

            return new JsonResult(json)
            {
                StatusCode = statusCode
            };
        }
    }
}
