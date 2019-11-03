using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace JssBlazor.Components.Extensions
{
    public static class ComponentsApplicationBuilderExtensions
    {
        public static void UseJssBlazorComponents<TComponent>(
            this IComponentsApplicationBuilder app,
            string domElementSelector)
            where TComponent : IComponent
        {
            app.AddComponent<TComponent>(domElementSelector);

            // Blazor WebAssembly doesn't currently include any time zones so conversion to
            // local DateTimes from UTC does not work. This extension method fixes that.
            // https://github.com/jsakamoto/Toolbelt.Blazor.TimeZoneKit/
            app.UseLocalTimeZone();
        }
    }
}
