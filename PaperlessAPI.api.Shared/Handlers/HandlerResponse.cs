using PaperlessAPI.api.Shared.Models;

namespace PaperlessAPI.api.Shared.Handlers
{
    public record HandlerResponse
    {
        public int StatusCode { get; set; }

        public IEnumerable<Message> Messages { get; set; } = [];

        public bool IsSuccess { get => StatusCode >= 200 && StatusCode <= 299; }
    }

    public record HandlerResponse<T> : HandlerResponse
    {
        public T? Result { get; set; }
    }
}