using ErrorFactory.Core;

namespace ErrorFactory.Domain
{
    public static class SubjectErrors
    {
        public static ErrorCode SubjectAlreadyExists(string name) => 
            new ErrorCode("SubjectAlreadyExists", new
            {
                Name = name
            });
        
        public static ErrorCode SubjectDoesNotExists(int id) => 
            new ErrorCode("SubjectDoesNotExists", new
            {
                Id = id
            });
        
        public static ErrorCode SubjectsListIsEmpty() => 
            new ErrorCode("SubjectsListIsEmpty", new {});
    }
}