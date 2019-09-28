using System;
using JssBlazor.Components;
using JssBlazor.Core.Models;
using JssBlazor.Core.Services;
using JssBlazor.StyleGuide.Services;
using JssBlazor.Tracking.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JssBlazor.StyleGuide
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new ComponentFactoryOptions
            {
                ComponentAssemblyFormat = "JssBlazor.StyleGuide.Components.{0}, JssBlazor.StyleGuide",
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
            services.AddSingleton<ITrackingApi, LoggerTrackingApi>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
