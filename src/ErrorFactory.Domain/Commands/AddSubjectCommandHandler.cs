using System.Linq;
using ErrorFactory.Core;
using ErrorFactory.Core.Mediator;

namespace ErrorFactory.Domain.Commands
{
    public class AddSubjectCommandHandler : CommandHandlerBase<AddSubjectCommand>
    {
        private readonly ISubjectsRepository _repository;

        public AddSubjectCommandHandler(IErrorsFactory errorFactory, ISubjectsRepository repository)
            : base(errorFactory)
        {
            _repository = repository;
        }
        
        public override Result Handle(AddSubjectCommand command)
        {
            var existedSubject = _repository
                .GetAll()
                .SingleOrDefault(x => x.Name == command.Name);

            if (existedSubject != null)
            {
                return BadRequest(SubjectErrors.SubjectAlreadyExists(command.Name));
            }

            var nextId = _repository.GetNextId();
            var subject = new Subject(nextId, command.Name);
            
            _repository.Add(subject);

            return Ok();
        }
    }
}