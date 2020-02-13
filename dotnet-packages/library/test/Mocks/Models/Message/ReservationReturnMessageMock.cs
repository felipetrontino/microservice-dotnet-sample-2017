using Framework.Test.Common;
using Library.Models.Message;
using Library.Tests.Utils;

namespace Library.Tests.Mocks.Models.Message
{
    public class ReservationReturnMessageMock : MockBuilder<ReservationReturnMessageMock, ReservationReturnMessage>
    {
        public static ReservationReturnMessage Get(string key)
        {
            return Create(key).Default().Build();
        }

        public ReservationReturnMessageMock Default()
        {
            Value.Number = Fake.GetReservationNumber(Key);
            Value.Date = Fake.GetReturnDate(Key);

            return this;
        }
    }
}