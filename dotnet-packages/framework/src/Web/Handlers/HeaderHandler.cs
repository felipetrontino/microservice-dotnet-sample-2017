﻿using Framework.Core.Common;
using Framework.Web.Common;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Web.Handlers
{
    public class HeaderHandler : DelegatingHandler
    {
        private readonly ITenantAccessor _tenantAccessor;
        private readonly IUserAccessor _userAccessor;
        private readonly ICultureAccessor _languageAccessor;

        public HeaderHandler(ITenantAccessor tenantAccessor, IUserAccessor userAccessor, ICultureAccessor languageAccessor)
        {
            _tenantAccessor = tenantAccessor;
            _userAccessor = userAccessor;
            _languageAccessor = languageAccessor;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains(HttpHeaderNames.Tenant))
                request.Headers.Add(HttpHeaderNames.Tenant, _tenantAccessor.Tenant);

            if (!request.Headers.Contains(HttpHeaderNames.UserName))
                request.Headers.Add(HttpHeaderNames.UserName, _userAccessor.UserName);

            if (!request.Headers.Contains(HttpHeaderNames.Culture))
                request.Headers.Add(HttpHeaderNames.Culture, _languageAccessor.Culture);

            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}