using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using JssBlazor.Core.Models;
using JssBlazor.Core.Models.LayoutService;
using Newtonsoft.Json;

namespace JssBlazor.Core.Services
{
    public class RemoteLayoutService : ILayoutService
    {
        private readonly SitecoreConfiguration _sitecoreConfiguration;
        private readonly HttpClient _httpClient;

        public RemoteLayoutService(
            SitecoreConfiguration sitecoreConfiguration,
            HttpClient httpClient)
        {
            _sitecoreConfiguration = sitecoreConfiguration ?? throw new ArgumentNullException(nameof(sitecoreConfiguration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<LayoutServiceResult> GetRouteDataAsync(string route, string language)
        {
            var layoutServiceUrl = GetLayoutServiceUrl(route, language);

            // _httpClient.GetJsonAsync<LayoutServiceResult>(path) has issues deserializing the
            // Layout Service Response. Newtonsoft.Json does not.
            var response = await _httpClient.GetStringAsync(layoutServiceUrl);
            return JsonConvert.DeserializeObject<LayoutServiceResult>(response);
        }

        private string GetLayoutServiceUrl(string route, string language)
        {
            var layoutServiceUrl = new UriBuilder(_sitecoreConfiguration.SitecoreApiHost)
            {
                Path = "/sitecore/api/layout/render/jss"
            };

            var queryString = HttpUtility.ParseQueryString(layoutServiceUrl.Query);
            queryString["item"] = route;
            queryString["sc_lang"] = !string.IsNullOrWhiteSpace(language) ? language : _sitecoreConfiguration.DefaultLanguage;
            queryString["sc_apikey"] = _sitecoreConfiguration.SitecoreApiKey.ToString("D");
            layoutServiceUrl.Query = queryString.ToString();

            return layoutServiceUrl.ToString();
        }
    }
}
