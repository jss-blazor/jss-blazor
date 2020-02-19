using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.AspNetCore.Blazor.Http;

namespace JssBlazor.Tracking.Extensions
{
    public static class ComponentsApplicationBuilderExtensions
    {
        public static WebAssemblyHost UseJssBlazorTracking(this WebAssemblyHost host)
        {
            WebAssemblyHttpMessageHandlerOptions.DefaultCredentials = FetchCredentialsOption.Include;
            return host;
        }
    }
}
