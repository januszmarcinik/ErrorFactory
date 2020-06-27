using System.Net;
using ErrorFactory.Core;

namespace ErrorFactory.Api.Errors
{
    public abstract class ErrorsFactoryDecoratorBase : IErrorsFactory
    {
        private readonly IErrorsFactory _errorsFactory;

        protected ErrorsFactoryDecoratorBase(IErrorsFactory errorsFactory) => 
            _errorsFactory = errorsFactory;

        public virtual Result Create(HttpStatusCode statusCode, ErrorCode errorCode) => 
            _errorsFactory.Create(statusCode, errorCode);

        public virtual Result<T> Create<T>(HttpStatusCode statusCode, ErrorCode errorCode) => 
            _errorsFactory.Create<T>(statusCode, errorCode);
    }
}