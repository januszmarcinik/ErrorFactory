using ErrorFactory.Core.Mediator;

namespace ErrorFactory.Domain.Commands
{
    public class AddSubjectCommand : ICommand
    {
        public AddSubjectCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}