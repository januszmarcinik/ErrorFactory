using System.Linq;
using ErrorFactory.Core;
using ErrorFactory.Core.Mediator;

namespace ErrorFactory.Domain.Commands
{
    public class AddSubjectCommandHandler : CommandHandlerBase<AddSubjectCommand, IErrors>
    {
        private readonly ISubjectsRepository _repository;

        public AddSubjectCommandHandler(IErrorHandler<IErrors> errorHandler, ISubjectsRepository repository)
            : base(errorHandler)
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
                Errors.BadRequest()
            }
        }
    }
}