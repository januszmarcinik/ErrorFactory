namespace ErrorFactory.Core.Mediator
{
    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public abstract Result<TResult> Handle(TQuery query);

        protected Result<TResult> Ok(TResult value) =>
            Result.Ok(value);

        protected Result<TResult> Fail(string message) =>
            Result.Fail<TResult>(message);
    }
}