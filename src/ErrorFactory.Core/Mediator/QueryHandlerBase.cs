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
            Result<TResult>.Success(HttpStatusCode.OK, value);

        protected Result<TResult> BadRequest(ErrorCode errorCode) =>
            _errorsFactory.Create<TResult>(HttpStatusCode.BadRequest, errorCode);

        protected Result<TResult> NotFound(ErrorCode errorCode) => 
            _errorsFactory.Create<TResult>(HttpStatusCode.NotFound, errorCode);

        protected Result<TResult> Forbidden(ErrorCode errorCode) => 
            _errorsFactory.Create<TResult>(HttpStatusCode.Forbidden, errorCode);

        protected Result<TResult> InternalServerError(ErrorCode errorCode) => 
            _errorsFactory.Create<TResult>(HttpStatusCode.InternalServerError, errorCode);
    }
}