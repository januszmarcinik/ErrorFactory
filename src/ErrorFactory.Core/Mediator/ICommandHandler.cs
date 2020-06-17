namespace ErrorFactory.Core.Mediator
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }
}
