using Framework.Core.Pagination;

namespace Framework.Test.Mock.Pagination
{
    public static class PagedResponseMock
    {
        public static PagedResponse<TProxy> Create<TProxy>(params TProxy[] values)
        {
            return new PagedResponse<TProxy>(values);
        }
    }
}
