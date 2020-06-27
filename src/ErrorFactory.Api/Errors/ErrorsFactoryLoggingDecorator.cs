using System.Net;
using ErrorFactory.Core;
using Microsoft.Extensions.Logging;

namespace ErrorFactory.Api.Errors
{
    public class ErrorsFactoryLoggingDecorator : ErrorsFactoryDecoratorBase
    {
        private readonly ILogger<IErrorsFactory> _logger;

        public ErrorsFactoryLoggingDecorator(IErrorsFactory errorsFactory, ILogger<IErrorsFactory> logger) 
            : base(errorsFactory)
        {
            _logger = logger;
        }

        public override Result Create(HttpStatusCode statusCode, ErrorCode errorCode)
        {
            LogError(statusCode, errorCode);
            return base.Create(statusCode, errorCode);
        }

        public override Result<T> Create<T>(HttpStatusCode statusCode, ErrorCode errorCode)
        {
            LogError(statusCode, errorCode);
            return base.Create<T>(statusCode, errorCode);
        }

        private void LogError(HttpStatusCode statusCode, ErrorCode errorCode) =>
            _logger.LogError(
                "Error {0} occurred with status code {1} and parameters: {2}", 
                errorCode.Code, 
                statusCode, 
                errorCode.Parameters);
    }
}