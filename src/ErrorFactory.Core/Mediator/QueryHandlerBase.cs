using System.Net;

namespace ErrorFactory.Core.Mediator
{
    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IErrorsFactory _errorsFactory;

        protected QueryHandlerBase(IErrorsFactory errorsFactory)
        {
            _errorsFactory = errorsFactory;
        }

        public abstract Result<TResult> Handle(TQuery query);

        protected Result<TResult> Ok(TResult value) =>
            new Result<TResult>(HttpStatusCode.OK, value);

        protected Result<TResult> BadRequest(ErrorCode errorCode) =>
            _errorsFactory.Create<TResult>(HttpStatusCode.BadRequest, errorCode);
        
        public Result<TResult> NotFound(ErrorCode errorCode) => 
            _errorsFactory.Create<TResult>(HttpStatusCode.NotFound, errorCode);
        
        public Result<TResult> Forbidden(ErrorCode errorCode) => 
            _errorsFactory.Create<TResult>(HttpStatusCode.Forbidden, errorCode);

        public Result<TResult> InternalServerError(ErrorCode errorCode) => 
            _errorsFactory.Create<TResult>(HttpStatusCode.InternalServerError, errorCode);
    }
}