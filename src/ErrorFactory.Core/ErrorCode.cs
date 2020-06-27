namespace ErrorFactory.Core
{
    public class ErrorCode
    {
        public ErrorCode(string code, object parameters)
        {
            Code = code;
            Parameters = parameters;
        }

        public string Code { get; }
        public object Parameters { get; }
    }
}