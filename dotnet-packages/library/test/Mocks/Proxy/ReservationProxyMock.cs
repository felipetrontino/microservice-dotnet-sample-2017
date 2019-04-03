using Framework.Test.Common;
using Library.Models.Proxy;
using Library.Tests.Utils;

namespace Library.Tests.Mocks.Proxy
{
    public class ReservationProxyMock : MockBuilder<ReservationProxyMock, ReservationProxy>
    {
        public static ReservationProxy Get(string key)
        {
            return Create(key).Default().Build();
        }

        public ReservationProxyMock Default()
        {
            Value.Number = Fake.GetReservationNumber(Key);
            Value.Items.Add(GetItem(Key));

            return this;
        }

        public ReservationProxy.Item GetItem(string key)
        {
            return new ReservationProxy.Item
            {
                Name = Fake.GetTitle(key),
                Number = Fake.GetCopyNumber(key)
            };
        }
    }
}
