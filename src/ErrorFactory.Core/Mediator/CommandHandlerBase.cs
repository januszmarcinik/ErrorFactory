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

        protected Result Ok(string value = "") =>
            Result.Success(HttpStatusCode.OK, value);

        protected Result BadRequest(ErrorCode errorCode) =>
            _errorsFactory.Create(HttpStatusCode.BadRequest, errorCode);

        protected Result NotFound(ErrorCode errorCode) => 
            _errorsFactory.Create(HttpStatusCode.NotFound, errorCode);

        protected Result Forbidden(ErrorCode errorCode) => 
            _errorsFactory.Create(HttpStatusCode.Forbidden, errorCode);

        protected Result InternalServerError(ErrorCode errorCode) => 
            _errorsFactory.Create(HttpStatusCode.InternalServerError, errorCode);
    }
}