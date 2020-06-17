namespace ErrorFactory.Core
{
    public class Result
    {
        protected Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            IsFailure = !isSuccess;
            Message = message;
        }

        public string Message { get; }

        public bool IsSuccess { get; }
        
        public bool IsFailure { get; }

        public static Result Fail(string message)
            => new Result(false, message);

        public static Result<T> Fail<T>(string message)
            => new Result<T>(false, message, default);

        public static Result Ok()
            => new Result(true, "");
        
        public static Result Ok(string message)
            => new Result(true, message);

        public static Result<T> Ok<T>(T value)
            => new Result<T>(true, "", value);
    }

    public class Result<T> : Result
    {
        internal Result(bool isSuccess, string message, T value)
            : base(isSuccess, message)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
