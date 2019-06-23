using JssBlazor.Client.Services;
using JssBlazor.Client.Shared.Jss.Components;
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
            services.AddSingleton<ComponentFactoryOptions>(_ => new ComponentFactoryOptions
            {
                MissingComponentType = typeof(MissingComponent).AssemblyQualifiedName
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
