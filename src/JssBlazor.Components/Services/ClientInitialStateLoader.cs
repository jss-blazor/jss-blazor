using System;
using System.Threading.Tasks;
using JssBlazor.Core;
using JssBlazor.Core.Models.LayoutService;
using JssBlazor.Core.Services;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace JssBlazor.Components.Services
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
            try
            {
                var initialState = await _jsRuntime.InvokeAsync<string>(Constants.GetInitialStateMethodName);
                return JsonConvert.DeserializeObject<LayoutServiceResult>(initialState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
