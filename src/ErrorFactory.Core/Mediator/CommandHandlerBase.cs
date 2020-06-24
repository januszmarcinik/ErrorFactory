using System.Net;

namespace ErrorFactory.Core.Mediator
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly IErrorsFactory _errorsFactory;

        protected CommandHandlerBase(IErrorsFactory errorsFactory)
        {
            _errorsFactory = errorsFactory;
        }
        
        public abstract Result Handle(TCommand command);

        protected Result Ok(string message = "") =>
            new Result(HttpStatusCode.OK, message);

        protected Result BadRequest(ErrorCode errorCode) =>
            _errorsFactory.Create(HttpStatusCode.BadRequest, errorCode);
        
        public Result NotFound(ErrorCode errorCode) => 
            _errorsFactory.Create(HttpStatusCode.NotFound, errorCode);
        
        public Result Forbidden(ErrorCode errorCode) => 
            _errorsFactory.Create(HttpStatusCode.Forbidden, errorCode);

        public Result InternalServerError(ErrorCode errorCode) => 
            _errorsFactory.Create(HttpStatusCode.InternalServerError, errorCode);
    }
}