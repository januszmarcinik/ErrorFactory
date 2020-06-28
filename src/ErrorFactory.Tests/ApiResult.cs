using System.Net;

namespace ErrorFactory.Tests
{
    public class ApiResult
    {
        public ApiResult(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public HttpStatusCode StatusCode { get; }
        public string Message { get; }
    }
    
    public class ApiResult<T>
    {
        public ApiResult(HttpStatusCode statusCode, string errorMessage, T value)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            Value = value;
        }

        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
        public T Value { get; }
    }
}