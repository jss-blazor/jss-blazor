using Microsoft.AspNetCore.Components.Builder;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace JssBlazor.Components.Extensions
{
    public static class ComponentsApplicationBuilderExtensions
    {
        public static void UseJssBlazorComponents(this IComponentsApplicationBuilder app)
        {
            // Blazor WebAssembly doesn't currently include any time zones so conversion to
            // local DateTimes from UTC does not work. This extension method fixes that.
            // https://github.com/jsakamoto/Toolbelt.Blazor.TimeZoneKit/
            app.UseLocalTimeZone();
        }
    }
}
