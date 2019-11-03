using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using JssBlazor.Tracking.Models;
using Microsoft.AspNetCore.Blazor.Http;
using Newtonsoft.Json;

namespace JssBlazor.Tracking.Services
{
    public class TrackingApi : ITrackingApi
    {
        private static readonly IDictionary<string, string> EmptyDictionary = new Dictionary<string, string>();

        private readonly HttpClient _httpClient;

        public TrackingApi(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task TrackEventAsync(ITrackingModel @event, TrackingRequestOptions options)
        {
            await TrackEventsAsync(new[] { @event }, options);
        }

        public async Task TrackEventsAsync(IEnumerable<ITrackingModel> events, TrackingRequestOptions options)
        {
            var fetchUrl = ResolveTrackingUrl(options);
            await FetchData(fetchUrl, events, options?.QueryStringParams ?? EmptyDictionary);
        }

        private static string ResolveTrackingUrl(TrackingRequestOptions options)
        {
            return $"{options.Host}{options.ServiceUrl}/{options.Action}";
        }

        private async Task FetchData<T>(string url,
            T data,
            IDictionary<string, string> parameters)
        {
            var request = BuildRequest(url, data, parameters);
            var response = await _httpClient.SendAsync(request);
            await response.Content.ReadAsStringAsync();
        }

        private static HttpRequestMessage BuildRequest<T>(
            string url,
            T data,
            IDictionary<string, string> parameters)
        {
            var queryString = GetQueryString(parameters);
            var fetchUrl = url.IndexOf("?", StringComparison.Ordinal) > -1 ? $"{url}&{queryString}" : $"{url}?{queryString}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(fetchUrl),
                Content = new StringContent(JsonConvert.SerializeObject(data))
            };

            // include browser cookies when making request
            request.Properties[WebAssemblyHttpMessageHandler.FetchArgs] = new
            {
                // the FetchCredentialsOption.Include enum value throws an exception in the browser
                credentials = "include"
            };

            return request;
        }

        private static string GetQueryString(IDictionary<string, string> parameters)
        {
            return HttpUtility.UrlEncode(string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}")));
        }
    }
}
