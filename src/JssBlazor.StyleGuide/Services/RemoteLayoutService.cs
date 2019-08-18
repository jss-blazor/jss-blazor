using System;
using System.Net.Http;
using System.Threading.Tasks;
using JssBlazor.Shared.Models.LayoutService;
using JssBlazor.Shared.Services;
using Newtonsoft.Json;

namespace JssBlazor.StyleGuide.Services
{
    public class RemoteLayoutService : ILayoutService
    {
        private readonly HttpClient _httpClient;

        public RemoteLayoutService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<LayoutServiceResult> GetRouteAsync(string path)
        {
            // _httpClient.GetJsonAsync<LayoutServiceResult>(path) has issues deserializing the
            // Layout Service Response. Newtonsoft.Json does not.
            var response = await _httpClient.GetStringAsync(path);
            return JsonConvert.DeserializeObject<LayoutServiceResult>(response);
        }
    }
}
