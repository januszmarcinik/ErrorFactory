using ErrorFactory.Core;

namespace ErrorFactory.Domain
{
    public static class SubjectErrors
    {
        public static ErrorCode SubjectAlreadyExists(string name) => 
            new ErrorCode("SubjectAlreadyExist", name);
        
        public static ErrorCode SubjectDoesNotExists(int id) => 
            new ErrorCode("SubjectDoesNotExists", id);
    }
}