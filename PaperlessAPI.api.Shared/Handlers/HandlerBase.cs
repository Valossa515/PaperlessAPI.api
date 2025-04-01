using FluentValidation;
using Microsoft.Extensions.Logging;
using PaperlessAPI.api.Shared.Models;
using PaperlessAPI.api.Shared.Properties;
using System.Net;
namespace PaperlessAPI.api.Shared.Handlers
{
    public abstract class HandlerBase<TSchema, TResponse>(ILogger logger, IValidator<TSchema>? validator = null)
    {
        private readonly ILogger _logger = logger;
        private readonly IValidator<TSchema>? _validator = validator;

        public async Task<HandlerResponse<TResponse>> Execute(TSchema request)
        {
            try
            {
                if (_validator != null)
                    await _validator.ValidateAndThrowAsync(request);

                return await DoExecute(request);
            }
            catch (ValidationException ex)
            {
                var retorno = new HandlerResponse<TResponse>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Messages = ex.Errors.Select(err => new Message(err.PropertyName, err.ErrorMessage))
                };

                return retorno;
            }
            catch (Exception e)
            {
                var retorno = new HandlerResponse<TResponse>()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Messages = [new Message("000", $"{Resources.UnexpectedError}")]
                };

                _logger.LogError(e, "{ErrorMessage}", e.Message);
                return retorno;
            }
        }

        public abstract Task<HandlerResponse<TResponse>> DoExecute(TSchema request);

        protected HandlerResponse<TResponse> NotFound(string? message = null)
           => new()
           {
               StatusCode = (int)HttpStatusCode.NotFound,
               Messages = message != null ? [new(Resources.ResourceNotFoundErrorCode, message)] : []
           };

        protected HandlerResponse<TResponse> Success(TResponse response)
            => new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Result = response
            };

        protected HandlerResponse<TResponse> Created(TResponse response)
            => new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Result = response
            };

        protected HandlerResponse<TResponse> BadRequest(string? code, string message)
           => new()
           {
               StatusCode = (int)HttpStatusCode.BadRequest,
               Messages = [new(code ?? string.Empty, message)]
           };

        protected HandlerResponse<TResponse> NoContent()
           => new()
           {
               StatusCode = (int)HttpStatusCode.NoContent,
               Result = default
           };
    }
}