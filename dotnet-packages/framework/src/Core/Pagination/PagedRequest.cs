namespace Framework.Core.Pagination
{
    public class PagedRequest : IPagedRequest
    {
        public int Page { get; set; } = PageValues.PageStartIndex;
        public int PageSize { get; set; } = PageValues.PageSize;
    }
}
