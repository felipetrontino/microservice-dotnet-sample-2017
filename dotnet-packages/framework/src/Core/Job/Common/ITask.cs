using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Framework.Core.Job.Common
{
    public interface ITask
    {
        string Tenant { get; set; }
        Task RunAsync(IConfiguration config);
    }
}
