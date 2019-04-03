using Book.Core.Enums;

namespace Book.Models.Payload
{
    public class BookFilterPayload
    {
        public string Title { get; set; }
        public string Category { get; set; }        
        public Language? Language { get; set; }
    }
}
