using System.Threading.Tasks;
using JssBlazor.Components.Services;

namespace JssBlazor.RenderingHost.Services
{
    public class ServerHeadService : IHeadService
    {
        public string Title { get; private set; }

        public Task SetTitleAsync(string title)
        {
            Title = title;
            return Task.CompletedTask;
        }
    }
}
