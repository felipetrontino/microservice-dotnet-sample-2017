using Framework.Core.Pagination;
using System;

namespace Framework.Test.Mock.Pagination
{
    public static class PagedRequestMock
    {
        public static PagedRequest Create()
        {
            return new PagedRequest();
        }

        public static PagedRequest<TFilter> Create<TFilter>(TFilter filter = null)
            where TFilter : class
        {
            if (filter == null)
                filter = Activator.CreateInstance<TFilter>();

            return new PagedRequest<TFilter>(filter);
        }
    }
}
