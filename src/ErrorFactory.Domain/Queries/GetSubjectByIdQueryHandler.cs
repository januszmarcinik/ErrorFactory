using ErrorFactory.Core;
using ErrorFactory.Core.Mediator;

namespace ErrorFactory.Domain.Queries
{
    public class GetSubjectByIdQueryHandler : QueryHandlerBase<GetSubjectByIdQuery, Subject>
    {
        private readonly ISubjectsRepository _repository;

        public GetSubjectByIdQueryHandler(IErrorsFactory errorsFactory, ISubjectsRepository repository) 
            : base(errorsFactory)
        {
            _repository = repository;
        }

        public override Result<Subject> Handle(GetSubjectByIdQuery query)
        {
            var result = _repository.GetById(query.Id);
            if (result == null)
            {
                return NotFound(SubjectErrors.SubjectDoesNotExists(query.Id));
            }

            return Ok(result);
        }
    }
}