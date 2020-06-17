namespace ErrorFactory.Core.Mediator
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Result<TResult> Handle(TQuery query);
    }
}