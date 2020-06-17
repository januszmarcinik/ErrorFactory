namespace ErrorFactory.Core.Mediator
{
    public interface IMediator
    {
        Result Command<TCommand>(TCommand command) where TCommand : ICommand;

        Result<TResult> Query<TResult>(IQuery<TResult> query);
    }
}