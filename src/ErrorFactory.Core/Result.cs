using System.Net;

namespace ErrorFactory.Core
{
    public class Result
    {
        public Result(HttpStatusCode statusCode, string message = "")
        {
            StatusCode = statusCode;
            Message = message;
        }

        public HttpStatusCode StatusCode { get; }

        public string Message { get; }

        public bool IsSuccess => (int)StatusCode >= 200 && (int)StatusCode <= 299;
    }

    public class Result<T> : Result
    {
        public Result(HttpStatusCode statusCode, T value, string message = "")
            : base(statusCode, message)
        {
            Value = value;
        }
        
        public Result(HttpStatusCode statusCode, string message = "")
            : base(statusCode, message)
        {
            Value = default;
        }

        public T Value { get; }
    }
}
