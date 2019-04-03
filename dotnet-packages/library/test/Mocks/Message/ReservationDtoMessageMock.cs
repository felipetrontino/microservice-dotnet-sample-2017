using Framework.Test.Common;
using Library.Models.Message;

namespace Library.Tests.Mocks.Message
{
    public class ReservationDtoMessageMock : MockBuilder<ReservationDtoMessageMock, ReservationDtoMessage>
    {
        public static ReservationDtoMessage Get(string key)
        {
            return Create(key).Default().Build();
        }

        public ReservationDtoMessageMock Default()
        {
            Value.ReservationId = FakeHelper.GetId(Key);

            return this;
        }
    }
}
