using Framework.Test.Common;
using Library.Models.Dto;
using Library.Tests.Utils;
using System.Collections.Generic;

namespace Library.Tests.Mocks.Dto
{
    public class ReservationDtoMock : MockBuilder<ReservationDtoMock, ReservationDto>
    {
        public static ReservationDto Get(string key)
        {
            return Create(key).Default().Build();
        }

        public ReservationDtoMock Default()
        {
            Value.Number = Fake.GetReservationNumber(Key);
            Value.Status = Fake.GetStatusReservation(Key);
            Value.RequestDate = Fake.GetRequestDate(Key);
            Value.Member = GetMemberDetail(Key);
            Value.Loans = new List<ReservationDto.LoanDetail>
            {
                GetLoanDetail(Key)
            };

            return this;
        }

        private ReservationDto.MemberDetail GetMemberDetail(string key)
        {
            var ret = MockHelper.CreateModel<ReservationDto.MemberDetail>(key);
            ret.Name = Fake.GetMemberName(key);
            ret.DocumentId = FakeHelper.GetId(key).ToString();

            return ret;
        }

        private ReservationDto.LoanDetail GetLoanDetail(string key)
        {
            var ret = MockHelper.CreateModel<ReservationDto.LoanDetail>(key);
            ret.Title = Fake.GetTitle(key);
            ret.CopyNumber = Fake.GetCopyNumber(key);
            ret.DueDate = Fake.GetDueDate(key);
            ret.ReturnDate = null;

            return ret;
        }
    }
}