using System.Net;
using Microsoft.Extensions.Configuration;

namespace ErrorFactory.Core
{
    public class ErrorsFactory
    {
        private readonly IConfiguration _configuration;
        protected virtual string Lang => "en";

        public ErrorsFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public Result Create(HttpStatusCode statusCode, ErrorCode errorCode) => 
            new Result(statusCode, MakeMessage(errorCode));

        public Result<T> Create<T>(HttpStatusCode statusCode, ErrorCode errorCode, T value = default) => 
            new Result<T>(statusCode, value, MakeMessage(errorCode));

        private string MakeMessage(ErrorCode errorCode)
        {
            var messageFormat = _configuration[$"{errorCode.Code}:{Lang}"];
            var message = string.IsNullOrWhiteSpace(messageFormat)
                ? $"{errorCode.Code}: {string.Join(", ", errorCode.Parameters)}"
                : string.Format(messageFormat, errorCode.Parameters);

            return message;
        }
    }
}