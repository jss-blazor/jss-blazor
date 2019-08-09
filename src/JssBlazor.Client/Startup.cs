using JssBlazor.Client.Services;
using JssBlazor.Components;
using JssBlazor.Shared.Models;
using JssBlazor.Shared.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JssBlazor.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ => new ComponentFactoryOptions
            {
                ComponentAssemblyFormat = "JssBlazor.Client.Shared.StyleGuide.{0}, JssBlazor.Client",
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
