using ErrorFactory.Core;
using ErrorFactory.Core.Mediator;
using ErrorFactory.Domain.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ErrorFactory.Api.Controllers
{
    [Route("subjects")]
    public class SubjectsControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectsControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var result = _mediator.Query(new GetSubjectsQuery());
            return FromResult(result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var result = _mediator.Query(new GetSubjectByIdQuery(id));
            return FromResult(result);
        }
        
        private IActionResult FromResult(Result result) =>
            StatusCode((int) result.StatusCode, result.Message);
        
        private IActionResult FromResult<T>(Result<T> result) =>
            result.IsSuccess
                ? StatusCode((int) result.StatusCode, result.Value)
                : StatusCode((int) result.StatusCode, result.Message);
    }
}