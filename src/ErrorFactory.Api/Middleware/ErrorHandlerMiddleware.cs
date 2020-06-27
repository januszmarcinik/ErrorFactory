using System;
using System.Net;
using System.Threading.Tasks;
using ErrorFactory.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ErrorFactory.Api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IErrorsFactory _errorsFactory;

        public ErrorHandlerMiddleware(RequestDelegate next, IErrorsFactory errorsFactory)
        {
            _next = next;
            _errorsFactory = errorsFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorResult = _errorsFactory.Create(HttpStatusCode.InternalServerError, ApiErrors.UnhandledException(ex));
                context.Response.StatusCode = (int)errorResult.StatusCode;
                await context.Response.WriteAsync(errorResult.ErrorMessage);
            }
        }
    }

    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}