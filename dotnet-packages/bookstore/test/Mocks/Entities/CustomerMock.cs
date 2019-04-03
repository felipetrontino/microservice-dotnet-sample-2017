using Bookstore.Entities;
using Bookstore.Tests.Utils;
using Framework.Test.Common;

namespace Bookstore.Tests.Mocks.Entities
{
    public class CustomerMock : MockBuilder<CustomerMock, Customer>
    {
        public static Customer Get(string key)
        {
            return Create(key).Default().Build();
        }

        public CustomerMock Default()
        {
            Value.Name = Fake.GetCustomerName(Key);
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
