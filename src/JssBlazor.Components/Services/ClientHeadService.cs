using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace JssBlazor.Components.Services
{
    public class ClientHeadService : IHeadService
    {
        private readonly IJSRuntime _jsRuntime;

        public string Title { get; private set; }

        public ClientHeadService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime ?? throw new System.ArgumentNullException(nameof(jsRuntime));
        }

        public async Task SetTitleAsync(string title)
        {
            Title = title;
            await _jsRuntime.InvokeVoidAsync(Constants.RenderTitleMethodName, title);
        }
    }
}
