using Framework.Core.Auth;
using Framework.Core.Common;
using Microsoft.AspNetCore.Http;

namespace Framework.Web.Common
{
    public class HttpContextInfoAccessor : IJwtAccessor, ITenantAccessor, IUserAccessor, IRequestIdAccessor, ICultureAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public HttpContextInfoAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public JwtData JwtData
        {
            get
            {
                if (_accessor.HttpContext != null)
                {
                    return _accessor.HttpContext.Request.Headers.ContainsKey(HttpHeaderNames.Authorization)
                        ? new JwtData(_accessor.HttpContext.Request.Headers[HttpHeaderNames.Authorization].ToString().Replace("Bearer ", ""))
                        : null;
                }

                return null;
            }
        }

        public string Tenant
        {
            get
            {
                if (_accessor.HttpContext != null)
                {
                    return _accessor.HttpContext.Request.Headers.ContainsKey(HttpHeaderNames.Tenant)
                        ? _accessor.HttpContext.Request.Headers[HttpHeaderNames.Tenant].ToString()
                        : null;
                }

                return null;
            }
        }

        public string RequestId
        {
            get
            {
                if (_accessor.HttpContext != null)
                {
                    return _accessor.HttpContext.Request.Headers.ContainsKey(HttpHeaderNames.RequestId)
                        ? _accessor.HttpContext.Request.Headers[HttpHeaderNames.RequestId].ToString()
                        : null;
                }

                return null;
            }
        }

        public string UserName
        {
            get
            {
                if (_accessor.HttpContext != null)
                {
                    return _accessor.HttpContext.Request.Headers.ContainsKey(HttpHeaderNames.UserName)
                       ? _accessor.HttpContext.Request.Headers[HttpHeaderNames.UserName].ToString()
                       : null;
                }

                return null;
            }
        }

        public string Culture
        {
            get
            {
                if (_accessor.HttpContext != null)
                {
                    return _accessor.HttpContext.Request.Headers.ContainsKey(HttpHeaderNames.Culture)
                        ? _accessor.HttpContext.Request.Headers[HttpHeaderNames.Culture].ToString()
                        : null;
                }

                return null;
            }
        }
    }
}