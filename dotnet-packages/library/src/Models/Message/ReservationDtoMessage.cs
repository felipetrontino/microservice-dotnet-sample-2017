using Framework.Core.Bus;
using System;

namespace Library.Models.Message
{
    public class ReservationDtoMessage : BusMessage
    {
        public Guid ReservationId { get; set; }
    }
}
