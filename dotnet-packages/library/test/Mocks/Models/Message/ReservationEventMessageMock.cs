using Framework.Test.Common;
using Library.Models.Message;

namespace Library.Tests.Mocks.Models.Message
{
    public class ReservationEventMessageMock : MockBuilder<ReservationEventMessageMock, ReservationEventMessage>
    {
        public static ReservationEventMessage Get(string key)
        {
            return Create(key).Default().Build();
        }

        public ReservationEventMessageMock Default()
        {
            Value.ReservationId = FakeHelper.GetId(Key);

            return this;
        }
    }
}