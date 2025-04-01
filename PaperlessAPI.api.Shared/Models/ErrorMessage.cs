namespace PaperlessAPI.api.Shared.Models
{
    public class ErrorMessage
    {
        public string Code { get; set; } = "000";
        public string? Message { get; set; }

        public ErrorMessage(string? message)
        {
            Message = message;
        }

        public ErrorMessage(string code, string message)
        {
            this.Code = code;
            Message = message;
        }
    }
}
