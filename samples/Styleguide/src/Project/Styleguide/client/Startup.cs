using System;
using JssBlazor.Components;
using JssBlazor.Components.Extensions;
using JssBlazor.Core.Extensions;
using JssBlazor.Tracking;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JssBlazor.Project.Styleguide.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJssBlazor(options =>
            {
                options.ComponentFactoryOptions.ComponentAssemblyFormat = "JssBlazor.Project.Styleguide.Client.Components.{0}, JssBlazor.Project.Styleguide.Client";
                options.ComponentFactoryOptions.MissingComponentType = typeof(MissingComponent).AssemblyQualifiedName;
                options.ComponentFactoryOptions.RawComponentType = typeof(RawComponent).AssemblyQualifiedName;

                options.SitecoreConfiguration.DefaultLanguage = "en";
                options.SitecoreConfiguration.SitecoreApiKey = new Guid("a3ff4713-af3b-4faa-a471-4780c19a4dd8");
                options.SitecoreConfiguration.SitecoreApiHost = "http://styleguide.sitecore";
            });
            services.AddJssBlazorTracking();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            app.UseJssBlazorComponents();
        }
    }
}
