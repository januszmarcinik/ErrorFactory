using System.Collections.Generic;
using System.Linq;
using ErrorFactory.Core;
using ErrorFactory.Core.Mediator;

namespace ErrorFactory.Domain.Queries
{
    public class GetSubjectsQueryHandler : QueryHandlerBase<GetSubjectsQuery, IEnumerable<Subject>>
    {
        private readonly ISubjectsRepository _repository;

        public GetSubjectsQueryHandler(IErrorsFactory errorsFactory, ISubjectsRepository repository)
            : base(errorsFactory)
        {
            _repository = repository;
        }

        public override Result<IEnumerable<Subject>> Handle(GetSubjectsQuery query)
        {
            var result = _repository.GetAll().ToList();
            if (!result.Any())
            {
                return BadRequest(SubjectErrors.SubjectsListIsEmpty());
            }

            return Ok(result);
        }
    }
}