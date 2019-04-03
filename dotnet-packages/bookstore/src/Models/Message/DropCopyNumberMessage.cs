using Framework.Core.Bus;

namespace Bookstore.Models.Message
{
    public class DropCopyNumberMessage : BusMessage
    {
        public string Number { get; set; }
    }
}
