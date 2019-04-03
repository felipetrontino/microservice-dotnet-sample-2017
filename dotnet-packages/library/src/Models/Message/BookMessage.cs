using Framework.Core.Bus;
using Library.Core.Enums;

namespace Library.Models.Message
{
    public class BookMessage : BusMessage
    {
        public string Title { get; set; }
        public Language Language { get; set; }

        public string Author { get; set; }
    }
}
