﻿using System.Net;
using ErrorFactory.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ErrorFactory.Api
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

        public Result Create(HttpStatusCode statusCode, ErrorCode errorCode) => 
            new Result(statusCode, MakeMessage(errorCode));

        public Result<T> Create<T>(HttpStatusCode statusCode, ErrorCode errorCode, T value = default) => 
            new Result<T>(statusCode, value, MakeMessage(errorCode));

        private string MakeMessage(ErrorCode errorCode)
        {
            var language = ResolveLanguage();
            var messageFormat = _configuration[$"{errorCode.Code}:{language}"];
            var message = string.IsNullOrWhiteSpace(messageFormat)
                ? $"{errorCode.Code}: {string.Join(", ", errorCode.Parameters)}"
                : string.Format(messageFormat, errorCode.Parameters);

            return message;
        }

        private string ResolveLanguage() =>
            _contextAccessor.HttpContext.Request.Headers.TryGetValue("Accept-Language", out var language)
                ? (string) language
                : DefaultLanguage;
    }
}