using System.Net;

namespace ErrorFactory.Core
{
    public class Result : Result<string>
    {
        private Result(bool isSuccess, HttpStatusCode statusCode, string value, string errorMessage) 
            : base(isSuccess, statusCode, value, errorMessage)
        {
        }
        
        public new static Result Success(HttpStatusCode statusCode, string value) => 
            new Result(true, statusCode, value, "");
        
        public new static Result Failure(HttpStatusCode statusCode, string errorMessage) =>
            new Result(false, statusCode, default, errorMessage);
    }

    public class Result<T>
    {
        protected Result(bool isSuccess, HttpStatusCode statusCode, T value, string errorMessage)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Value = value;
            ErrorMessage = errorMessage;
        }
        
        public HttpStatusCode StatusCode { get; }

        public string ErrorMessage { get; }

        public bool IsSuccess { get; }

        public T Value { get; }

        public static Result<T> Success(HttpStatusCode statusCode, T value) => 
            new Result<T>(true, statusCode, value, "");
        
        public static Result<T> Failure(HttpStatusCode statusCode, string errorMessage) =>
            new Result<T>(false, statusCode, default, errorMessage);
    }
}
