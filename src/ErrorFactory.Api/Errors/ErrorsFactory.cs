using System.Linq;
using System.Net;
using System.Text;
using ErrorFactory.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ErrorFactory.Api.Errors
{
    public class ErrorsFactory : IErrorsFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private const string DefaultLanguage = "en";

        public ErrorsFactory(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public Result Create(HttpStatusCode statusCode, ErrorCode errorCode)
        {
            var result = Create<string>(statusCode, errorCode);
            return Result.Failure(statusCode, result.ErrorMessage);
        }

        public Result<T> Create<T>(HttpStatusCode statusCode, ErrorCode errorCode)
        {
            var language = ResolveLanguage();
            var messageFormat = GetMessageFormat(errorCode.Code, language);
            var message = FormatMessage(messageFormat, errorCode);
            return Result<T>.Failure(statusCode, message);
        }

        private string GetMessageFormat(string errorCode, string language) =>
            _configuration[$"{errorCode}:{language}"];

        private static string FormatMessage(string messageFormat, ErrorCode errorCode)
        {
            var parameters = errorCode.Parameters
                .GetType()
                .GetProperties()
                .ToDictionary(key => $"{{{key.Name}}}", value => value.GetValue(errorCode.Parameters));
            
            var sb = new StringBuilder(messageFormat);
            foreach (var (key, value) in parameters)
            {
                sb.Replace(key, value != null ? value.ToString() : "");
            }

            return sb.ToString();
        }

        private string ResolveLanguage() =>
            _contextAccessor.HttpContext.Request.Headers.TryGetValue("Accept-Language", out var language)
                ? (string) language
                : DefaultLanguage;
    }
}