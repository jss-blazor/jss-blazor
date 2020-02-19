using Microsoft.AspNetCore.Blazor.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace JssBlazor.Components.Extensions
{
    public static class ComponentsApplicationBuilderExtensions
    {
        public static WebAssemblyHost UseJssBlazorComponents(this WebAssemblyHost host)
        {
            // Blazor WebAssembly doesn't currently include any time zones so conversion to
            // local DateTimes from UTC does not work. This extension method fixes that.
            // https://github.com/jsakamoto/Toolbelt.Blazor.TimeZoneKit/
            host.UseLocalTimeZone();

            return host;
        }
    }
}
