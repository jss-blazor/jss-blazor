using JssBlazor.Components;
using JssBlazor.Shared.Models;
using JssBlazor.Shared.Services;
using JssBlazor.StyleGuide.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JssBlazor.StyleGuide
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ => new ComponentFactoryOptions
            {
                ComponentAssemblyFormat = "JssBlazor.StyleGuide.Components.{0}, JssBlazor.StyleGuide",
                MissingComponentType = typeof(MissingComponent).AssemblyQualifiedName,
                RawComponentType = typeof(RawComponent).AssemblyQualifiedName
            });
            services.AddSingleton<IComponentFactory, DefaultComponentFactory>();
            services.AddSingleton<ILayoutService, RemoteLayoutService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
