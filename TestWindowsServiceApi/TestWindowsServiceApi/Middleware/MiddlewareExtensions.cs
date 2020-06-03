using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWindowsServiceApi.Middleware
{
    public static class RequestResponseMiddlewareExtensions
    {
        public static IApplicationBuilder LogRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogRequestMiddleware>();
        }
    }
}
