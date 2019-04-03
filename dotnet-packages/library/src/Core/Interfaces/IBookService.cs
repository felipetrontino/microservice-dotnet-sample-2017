using Library.Models.Message;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface IBookService
    {
        Task UpdateAsync(BookMessage message);
    }
}
