using Framework.Test.Common;
using Library.Models.Message;
using Library.Tests.Utils;
using System.Collections.Generic;

namespace Library.Tests.Mocks.Message
{
    public class ReservationMessageMock : MockBuilder<ReservationMessageMock, ReservationMessage>
    {
        public static ReservationMessage Get(string key)
        {
            return Create(key).Default().Build();
        }

        public ReservationMessageMock Default()
        {
            Value.Number = Fake.GetReservationNumber(Key);
            Value.MemberId = FakeHelper.GetId(Key).ToString();
            Value.MemberName = Fake.GetMemberName(Key);

            Value.Items = new List<ReservationMessage.Item>();
            Value.Items.Add(GetItem(Key));

            return this;
        }

        private ReservationMessage.Item GetItem(string key)
        {
            var ret = MockHelper.CreateModel<ReservationMessage.Item>(key);
            ret.Name = Fake.GetTitle(key);
            ret.Number = Fake.GetCopyNumber(key);

            return ret;
        }
    }
}
