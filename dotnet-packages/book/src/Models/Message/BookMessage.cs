using Book.Core.Enums;
using Framework.Core.Bus;

namespace Book.Models.Message
{
    public class BookMessage : BusMessage
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Catalog { get; set; }
        public Language Language { get; set; }
    }
}