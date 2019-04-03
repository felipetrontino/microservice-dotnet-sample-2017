using Framework.Web.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Framework.Web.Common
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorMiddleware>();
        }        
    }
}
