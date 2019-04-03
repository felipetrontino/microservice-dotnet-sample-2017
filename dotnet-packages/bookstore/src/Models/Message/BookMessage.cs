using Bookstore.Core.Enums;
using Framework.Core.Bus;

namespace Bookstore.Models.Message
{
    public class BookMessage : BusMessage
    {
        public string Title { get; set; }
        public Language Language { get; set; }

        public string Author { get; set; }
    }
}
