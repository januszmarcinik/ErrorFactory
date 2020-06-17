namespace ErrorFactory.Core.Mediator
{
    public abstract class CommandHandlerBase<TCommand, TErrors> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        protected readonly IErrorHandler<TErrors> Errors;

        public CommandHandlerBase(IErrorHandler<TErrors> errors)
        {
            Errors = errors;
        }
        
        public abstract Result Handle(TCommand command);

        protected Result Ok() =>
            Result.Ok();
        
        protected Result Fail(string message) =>
            Result.Fail(message);
    }
}