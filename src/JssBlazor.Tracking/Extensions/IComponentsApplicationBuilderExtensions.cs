using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.AspNetCore.Components.Builder;

namespace JssBlazor.Tracking.Extensions
{
    public static class ComponentsApplicationBuilderExtensions
    {
        public static void UseJssBlazorTracking(this IComponentsApplicationBuilder app)
        {
            WebAssemblyHttpMessageHandlerOptions.DefaultCredentials = FetchCredentialsOption.Include;
        }
    }
}
