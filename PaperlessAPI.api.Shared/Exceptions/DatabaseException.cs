using PaperlessAPI.api.Shared.Models;
using System.Net;

namespace PaperlessAPI.api.Shared.Exceptions
{
    public class DatabaseException : Exception
    {
        public IEnumerable<ErrorMessage> Errors { get; } = [];
        public HttpStatusCode StatusCode { get; }
        public DatabaseException()
        { }

        public DatabaseException(string message) : base(message)
        {
            Errors = Errors.Prepend(new ErrorMessage("", message));
        }

        public DatabaseException(
           string message,
           Exception inner
       ) : base(message, inner) { }

        public DatabaseException(
             string message,
             IEnumerable<ErrorMessage> errors,
             HttpStatusCode statusCode
             ) : base(message)
        {
            Errors = errors;
            StatusCode = statusCode;
        }
    }
}
