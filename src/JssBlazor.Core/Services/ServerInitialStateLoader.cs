using System.Threading.Tasks;
using JssBlazor.Core.Models.LayoutService;

namespace JssBlazor.Core.Services
{
    public class ServerInitialStateLoader : IInitialStateLoader
    {
        public Task<LayoutServiceResult> GetInitialStateAsync()
        {
            return Task.FromResult<LayoutServiceResult>(null); ;
        }
    }
}
