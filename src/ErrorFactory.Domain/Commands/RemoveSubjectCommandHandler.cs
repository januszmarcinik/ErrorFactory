using ErrorFactory.Core;
using ErrorFactory.Core.Mediator;

namespace ErrorFactory.Domain.Commands
{
    public class RemoveSubjectCommandHandler : CommandHandlerBase<RemoveSubjectCommand>
    {
        private readonly ISubjectsRepository _repository;

        public RemoveSubjectCommandHandler(IErrorsFactory errorsFactory, ISubjectsRepository repository) 
            : base(errorsFactory)
        {
            _repository = repository;
        }
        
        public override Result Handle(RemoveSubjectCommand command)
        {
            var subject = _repository.GetById(command.Id);
            if (subject == null)
            {
                return NotFound(SubjectErrors.SubjectDoesNotExists(command.Id));
            }
            
            _repository.Remove(command.Id);
            return Ok();
        }
    }
}