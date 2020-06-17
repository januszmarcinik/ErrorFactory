using ErrorFactory.Core;

namespace ErrorFactory.Domain
{
    public interface IErrors
    {
        Error SubjectAlreadyExists(string title);
    }
}