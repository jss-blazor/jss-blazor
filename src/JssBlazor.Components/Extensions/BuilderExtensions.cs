using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace JssBlazor.Components.Extensions
{
    public static class BuilderExtensions
    {
        public static void UseJssBlazorComponents<TComponent>(
            this WebAssemblyHostBuilder builder,
            string domElementSelector)
            where TComponent : IComponent
        {
            builder.RootComponents.Add<TComponent>(domElementSelector);
        }

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
