using Framework.Test.Common;
using Library.Models.Payload;
using Library.Tests.Mocks.Entities;
using Library.Tests.Utils;

namespace Library.Tests.Mocks.Models.Payload
{
    public class ReservationFilterPayloadMock : MockBuilder<ReservationFilterPayloadMock, ReservationFilterPayload>
    {
        public static ReservationFilterPayload Get(string key)
        {
            return Create(key).Default().Build();
        }

        public ReservationFilterPayloadMock Default()
        {
            Value.MemberId = MemberMock.Get(Key).UserId;
            Value.MemberName = Fake.GetMemberName(Key);
            Value.Items.Add(GetItem(Key));

            return this;
        }

        public ReservationFilterPayload.Item GetItem(string key)
        {
            return new ReservationFilterPayload.Item
            {
                Name = Fake.GetItemName(key),
                Number = Fake.GetItemNumber(key)
            };
        }
    }
}