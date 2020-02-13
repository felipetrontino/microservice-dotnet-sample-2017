namespace Framework.Core.Pagination
{
    public class PagedRequest : IPagedRequest
    {
        public int Page { get; set; } = PageValues.PageStart;
        public int PageSize { get; set; } = PageValues.PageSize;
    }
}
