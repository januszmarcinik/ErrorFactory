using ErrorFactory.Core.Mediator;

namespace ErrorFactory.Domain.Commands
{
    public class RemoveSubjectCommand : ICommand
    {
        public RemoveSubjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}