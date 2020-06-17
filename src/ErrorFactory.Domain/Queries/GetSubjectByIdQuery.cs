using ErrorFactory.Core.Mediator;

namespace ErrorFactory.Domain.Queries
{
    public class GetSubjectByIdQuery : IQuery<Subject>
    {
        public GetSubjectByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}