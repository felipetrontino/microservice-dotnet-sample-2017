using Framework.Core.Bus;
using System;

namespace Library.Models.Message
{
    public class ReservationReturnMessage : BusMessage
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
    }
}
