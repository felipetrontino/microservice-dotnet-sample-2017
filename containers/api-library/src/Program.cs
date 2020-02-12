using Framework.Web.Common;
using System.Threading.Tasks;

namespace Library.Api
{
    public class Program
    {
        protected Program()
        {
        }

        public static async Task<int> Main(string[] args)
        {
            return await WebHostBootstrap.RunAsync<Startup>();
        }
    }
}