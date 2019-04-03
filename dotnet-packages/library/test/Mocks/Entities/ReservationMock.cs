using Framework.Test.Common;
using Library.Entities;
using Library.Tests.Utils;
using System;
using System.Collections.Generic;

namespace Library.Tests.Mocks.Entities
{
    public class ReservationMock : MockBuilder<ReservationMock, Reservation>
    {
        public static Reservation Get(string key)
        {
            return Create(key).Default().Build();
        }

        public static Reservation GetByReservation(string key)
        {
            return Create(key).ByReservation().Build();
        }

        public ReservationMock Default()
        {
            Value.Number = Fake.GetReservationNumber(Key);
            Value.Status = Fake.GetStatusReservation(Key);
            Value.RequestDate = Fake.GetRequestDate(Key);
            Value.Member = MemberMock.Get(Key);

            Value.Loans = new List<Loan>();
            Value.Loans.Add(LoanMock.Get(Key));

            return this;
        }

        public ReservationMock ByReservation()
        {
            Value.InsertedAt = DateTime.UtcNow;
            Value.Number = Fake.GetReservationNumber(Key);
            Value.Status = Fake.GetStatusReservation(Key);
            Value.RequestDate = DateTime.UtcNow;

            Value.Member = MockHelper.CreateModel<Member>(Key);
            Value.Member.InsertedAt = DateTime.UtcNow;
            Value.Member.Name = Fake.GetMemberName(Key);
            Value.Member.DocumentId = FakeHelper.GetId(Key).ToString();

            Value.Loans = new List<Loan>();

            var loan = LoanMock.Get(Key);
            loan.InsertedAt = DateTime.UtcNow;
            Value.Loans.Add(loan);

            return this;
        }
    }
}