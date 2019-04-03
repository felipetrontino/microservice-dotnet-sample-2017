using Framework.Core.Common;
using Framework.Core.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Framework.Web.Middlewares
{
    public class GlobalErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            ServiceAccessor.Instance = httpContext.RequestServices;

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                await _next(httpContext);
            }
        }
    }
}
