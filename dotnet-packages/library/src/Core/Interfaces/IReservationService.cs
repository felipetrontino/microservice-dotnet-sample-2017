using Library.Models.Message;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface IReservationService
    {
        Task RequestAsync(ReservationMessage message);

        Task ReturnAsync(ReservationReturnMessage message);

        Task CheckDueAsync();

        Task ExpireAsync(ReservationExpiredMessage message);
    }
}