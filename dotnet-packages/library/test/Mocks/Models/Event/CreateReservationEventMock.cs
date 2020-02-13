using Framework.Test.Common;
using Library.Models.Dto;
using Library.Tests.Utils;
using System.Collections.Generic;

namespace Library.Tests.Mocks.Models.Event
{
    public class CreateReservationEventMock : MockBuilder<CreateReservationEventMock, CreateReservationEvent>
    {
        public static CreateReservationEvent Get(string key)
        {
            return Create(key).Default().Build();
        }

        public CreateReservationEventMock Default()
        {
            Value.Number = Fake.GetReservationNumber(Key);
            Value.Status = Fake.GetStatusReservation(Key);
            Value.RequestDate = Fake.GetRequestDate(Key);
            Value.Member = GetMemberDetail(Key);
            Value.Loans = new List<CreateReservationEvent.LoanDetail>
            {
                GetLoanDetail(Key)
            };

            return this;
        }

        private CreateReservationEvent.MemberDetail GetMemberDetail(string key)
        {
            var ret = MockHelper.CreateModel<CreateReservationEvent.MemberDetail>(key);
            ret.Name = Fake.GetMemberName(key);
            ret.DocumentId = FakeHelper.GetId(key).ToString();

            return ret;
        }

        private CreateReservationEvent.LoanDetail GetLoanDetail(string key)
        {
            var ret = MockHelper.CreateModel<CreateReservationEvent.LoanDetail>(key);
            ret.Title = Fake.GetTitle(key);
            ret.CopyNumber = Fake.GetCopyNumber(key);
            ret.DueDate = Fake.GetDueDate(key);
            ret.ReturnDate = null;

            return ret;
        }
    }
}