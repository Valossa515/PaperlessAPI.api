namespace PaperlessAPI.api.Shared.Handlers
{
    public interface IHandler<TSchema, TResponse>
    {
        Task<HandlerResponse<TResponse>> Execute(TSchema request);
    }
}