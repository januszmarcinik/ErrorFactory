using System;
using ErrorFactory.Core;

namespace ErrorFactory.Api
{
    public static class ApiErrors
    {
        public static ErrorCode UnhandledException(Exception exception) =>
            new ErrorCode("UnhandledException", new { Exception = exception });
    }
}