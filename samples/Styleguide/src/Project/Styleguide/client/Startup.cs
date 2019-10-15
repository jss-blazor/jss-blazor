using System;
using JssBlazor.Components;
using JssBlazor.Core.Models;
using JssBlazor.Core.Services;
using JssBlazor.Styleguide.Services;
using JssBlazor.Tracking.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JssBlazor.Styleguide
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new ComponentFactoryOptions
            {
                ComponentAssemblyFormat = "JssBlazor.Styleguide.Components.{0}, JssBlazor.Styleguide",
                MissingComponentType = typeof(MissingComponent).AssemblyQualifiedName,
                RawComponentType = typeof(RawComponent).AssemblyQualifiedName
            });
            services.AddSingleton<IComponentFactory, DefaultComponentFactory>();
            services.AddSingleton(new SitecoreConfiguration
            {
                DefaultLanguage = "en",
                SitecoreApiKey = new Guid("a3ff4713-af3b-4faa-a471-4780c19a4dd8"),
                SitecoreApiHost = "http://styleguide.sitecore"
            });
            services.AddSingleton<ILayoutService, RemoteLayoutService>();

            services.AddTransient<ITrackingApi, TrackingApi>();
            services.AddSingleton<IInitialStateLoader, ClientInitialStateLoader>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
