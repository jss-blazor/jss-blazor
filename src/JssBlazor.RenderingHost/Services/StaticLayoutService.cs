using System;
using System.Threading.Tasks;
using JssBlazor.Core.Models.LayoutService;
using JssBlazor.Core.Services;

namespace JssBlazor.RenderingHost.Services
{
    public class StaticLayoutService : ILayoutService
    {
        private readonly ILayoutServiceResultProvider _layoutServiceResultProvider;

        public StaticLayoutService(ILayoutServiceResultProvider layoutServiceResultProvider)
        {
            _layoutServiceResultProvider = layoutServiceResultProvider ?? throw new ArgumentNullException(nameof(layoutServiceResultProvider));
        }

        public Task<LayoutServiceResult> GetRouteDataAsync(string route, string language)
        {
            return Task.FromResult(_layoutServiceResultProvider.Result);
        }
    }
}
