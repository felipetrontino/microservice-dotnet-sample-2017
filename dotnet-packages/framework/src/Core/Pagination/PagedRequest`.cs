namespace Framework.Core.Pagination
{
    public class PagedRequest<T> : PagedRequest
    {
        public PagedRequest() { }
        public PagedRequest(T filter)
        {
            Filter = filter;
        }
        public T Filter { get; set; }
    }
}
