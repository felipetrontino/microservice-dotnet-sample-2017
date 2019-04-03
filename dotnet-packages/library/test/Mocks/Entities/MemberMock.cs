using Framework.Test.Common;
using Library.Entities;
using Library.Tests.Utils;

namespace Library.Tests.Mocks.Entities
{
    public class MemberMock : MockBuilder<MemberMock, Member>
    {
        public static Member Get(string key)
        {
            return Create(key).Default().Build();
        }

        public MemberMock Default()
        {
            Value.Name = Fake.GetMemberName(Key);
            Value.Address = Fake.GetAddress(Key);
            Value.City = Fake.GetCity(Key);
            Value.State = Fake.GetState(Key);
            Value.DocumentId = FakeHelper.GetId(Key).ToString();
            Value.Email = Fake.GetEmail(Key);
            Value.Phone = Fake.GetPhone(Key);
            Value.UserId = FakeHelper.GetId(Key).ToString();

            return this;
        }
    }
}
