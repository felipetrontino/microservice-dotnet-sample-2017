using Bookstore.Models.Message;
using System.Threading.Tasks;

namespace Bookstore.Core.Interfaces
{
    public interface IProcessDtoService
    {
        Task CreateShippingAsync(ShippingDtoMessage message);
    }
}
