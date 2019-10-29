using System.Threading.Tasks;
using JssBlazor.Core.Models.LayoutService;
using JssBlazor.Core.Services;

namespace JssBlazor.RenderingHost.Services
{
    public class ServerInitialStateLoader : IInitialStateLoader
    {
        public Task<LayoutServiceResult> GetInitialStateAsync()
        {
            return Task.FromResult<LayoutServiceResult>(null);
        }
    }
}
