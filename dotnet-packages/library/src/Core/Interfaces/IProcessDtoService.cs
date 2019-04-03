using Library.Models.Message;
using System;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface IProcessDtoService
    {
        Task CreateReservationAsync(ReservationDtoMessage message);
    }
}
