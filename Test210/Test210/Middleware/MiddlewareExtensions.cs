
using Microsoft.AspNetCore.Builder;

namespace Test210.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder LogRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
