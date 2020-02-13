using Book.Core.Enums;
using Framework.Core.Bus;

namespace Book.Models.Message
{
    public class UpdateBookEvent : BusMessage, IBusPublishMessage
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Language Language { get; set; }
    }
}