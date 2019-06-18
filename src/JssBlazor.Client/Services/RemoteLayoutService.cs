using System;
using System.Net.Http;
using System.Threading.Tasks;
using JssBlazor.Shared.Models;
using JssBlazor.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace JssBlazor.Client.Services
{
    public class RemoteLayoutService : ILayoutService
    {
        private readonly HttpClient _httpClient;

        public RemoteLayoutService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<LayoutServiceResponse> GetRouteAsync(string path)
        {
            return await _httpClient.GetJsonAsync<LayoutServiceResponse>(path);
        }
    }
}
