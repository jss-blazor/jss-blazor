using System;
using JssBlazor.Components.Extensions;
using JssBlazor.Tracking;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JssBlazor.Project.Styleguide.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJssBlazorComponents(options =>
            {
                options.ComponentAssemblyFormat = "JssBlazor.Project.Styleguide.Client.Components.{0}, JssBlazor.Project.Styleguide.Client";
                options.DefaultLanguage = "en";
                options.SitecoreApiKey = new Guid("a3ff4713-af3b-4faa-a471-4780c19a4dd8");
                options.SitecoreApiHost = "http://styleguide.sitecore";
            });
            services.AddJssBlazorTracking();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.UseJssBlazorComponents<App>("app");
        }
    }
}
