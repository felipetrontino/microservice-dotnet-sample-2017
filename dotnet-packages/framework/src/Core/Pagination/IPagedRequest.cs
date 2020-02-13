namespace Framework.Core.Pagination
{
    public interface IPagedRequest
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
