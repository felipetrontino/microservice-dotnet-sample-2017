using Bookstore.Models.Message;
using System.Threading.Tasks;

namespace Bookstore.Core.Interfaces
{
    public interface IPurchaseService
    {
        Task CreateAsync(PurchaseMessage message);
    }
}
