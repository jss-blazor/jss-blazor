using System;
using System.Threading.Tasks;
using System.Web;
using JssBlazor.Core.Models.LayoutService;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace JssBlazor.Core.Services
{
    public class ClientInitialStateLoader : IInitialStateLoader
    {
        private readonly IJSRuntime _jsRuntime;

        public ClientInitialStateLoader(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        }

        public async Task<LayoutServiceResult> GetInitialStateAsync()
        {
            var initialState = await _jsRuntime.InvokeAsync<string>("jssBlazor.getInitialState");
            var htmlDecodedState = HttpUtility.HtmlDecode(initialState);
            try
            {
                return JsonConvert.DeserializeObject<LayoutServiceResult>(htmlDecodedState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
