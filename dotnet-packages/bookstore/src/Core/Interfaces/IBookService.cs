using Bookstore.Models.Message;
using System.Threading.Tasks;

namespace Bookstore.Core.Interfaces
{
    public interface IBookService
    {
        Task UpdateAsync(BookMessage message);
    }
}
