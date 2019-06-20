using System;
using System.Threading.Tasks;
using JssBlazor.Shared.Models.LayoutService;
using JssBlazor.Shared.Services;

namespace JssBlazor.RenderingHost.Services
{
    public class StaticLayoutService : ILayoutService
    {
        private readonly ILayoutServiceResultProvider _layoutServiceResultProvider;

        public StaticLayoutService(ILayoutServiceResultProvider layoutServiceResultProvider)
        {
            _layoutServiceResultProvider = layoutServiceResultProvider ?? throw new ArgumentNullException(nameof(layoutServiceResultProvider));
        }

        public Task<LayoutServiceResult> GetRouteAsync(string path)
        {
            return Task.FromResult(_layoutServiceResultProvider.Result);
        }
    }
}
