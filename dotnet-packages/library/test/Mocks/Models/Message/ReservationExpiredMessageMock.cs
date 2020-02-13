using Framework.Test.Common;
using Library.Models.Message;

namespace Library.Tests.Mocks.Models.Message
{
    public class ReservationExpiredMessageMock : MockBuilder<ReservationExpiredMessageMock, ReservationExpiredMessage>
    {
        public static ReservationExpiredMessage Get(string key)
        {
            return Create(key).Default().Build();
        }

        public ReservationExpiredMessageMock Default()
        {
            Value.ReservationId = FakeHelper.GetId(Key);

            return this;
        }
    }
}