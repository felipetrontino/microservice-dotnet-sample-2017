using Book.Core.Enums;
using Framework.Core.Pagination;

namespace Book.Models.Payload
{
    public class BookFilterPayload : PagedRequest
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public Language? Language { get; set; }
    }
}