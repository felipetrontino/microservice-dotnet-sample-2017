using System;
using Book.Core.Enums;
using Framework.Core.Bus;

namespace Book.Models.Message
{
    public class BookUpdateMessage : BusMessage, IBusPublishMessage
    {
        public Guid Id { get;  set; }
        public string Title { get; set; }
        public string Author { get;  set; }
        public Language Language { get;  set; }
    }
}
