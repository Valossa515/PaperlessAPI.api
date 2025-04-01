using FluentValidation;
using PaperlessAPI.api.Shared.Handlers;
using PaperlessAPI.api.Shared.Models;
using PaperlessAPI.api.Shared.Properties;
using System.Net;
using System.Text.Json;

namespace PaperlessAPI.api.Middlewares
{
    public class ErrorHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate next = next;

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlingMiddleware> logger)
        {
            var response = exception switch
            {
                ValidationException ex => new HandlerResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Messages = ex.Errors.Select(error => new Message(error.PropertyName, error.ErrorMessage))
                },
                _ =>
                new HandlerResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Messages = [new("000", $"{Resources.UnexpectedError}")]
                }
            };

            if (response.StatusCode != (int)HttpStatusCode.BadRequest)
            {
                logger.LogError(exception, "{ErrorDescription}", response.Messages.First().Description);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
