using ErrorFactory.Core.Mediator;

namespace ErrorFactory.Domain.Queries
{
    public class GetSubjectByIdQuery : IQuery<Subject>
    {
        public GetSubjectByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}