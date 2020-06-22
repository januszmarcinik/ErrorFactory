using System.Collections.Generic;
using ErrorFactory.Core;
using ErrorFactory.Core.Mediator;

namespace ErrorFactory.Domain.Queries
{
    public class GetSubjectsQueryHandler : QueryHandlerBase<GetSubjectsQuery, IEnumerable<Subject>>
    {
        private readonly ISubjectsRepository _repository;

        public GetSubjectsQueryHandler(ErrorsFactory errorsFactory, ISubjectsRepository repository) 
            : base(errorsFactory)
        {
            _repository = repository;
        }

        public override Result<IEnumerable<Subject>> Handle(GetSubjectsQuery query)
        {
            var result = _repository.GetAll();
            return Ok(result);
        }
    }
}