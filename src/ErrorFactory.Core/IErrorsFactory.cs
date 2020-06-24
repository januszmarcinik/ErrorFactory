using System.Net;

namespace ErrorFactory.Core
{
    public interface IErrorsFactory
    {
        Result Create(HttpStatusCode statusCode, ErrorCode errorCode);

        Result<T> Create<T>(HttpStatusCode statusCode, ErrorCode errorCode, T value = default); 
    }
}