using System;
using JssBlazor.RenderingHost.Services;
using JssBlazor.Core.Models;
using JssBlazor.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using JssBlazor.Tracking.Services;

namespace JssBlazor.RenderingHost.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJssBlazorRenderingHost(this IServiceCollection services, IConfiguration configuration)
        {
            // Required for Blazor server-side rendering.
            services.AddServerSideBlazor();

            // Replace Blazor's out-of-the-box NavigationManager with one that correctly resolves URLs server side.
            services.AddScoped<NavigationManager, HardcodedRemoteNavigationManager>();

            services.AddSingleton<ILayoutServiceResultProvider, StaticLayoutServiceResultProvider>();
            services.AddSingleton<ILayoutService, StaticLayoutService>();

            services.AddSingleton<Func<string, IFileInfo>>(serviceProvider => subpath =>
            {
                var webHostEnvironment = serviceProvider.GetService<IWebHostEnvironment>();
                return webHostEnvironment.WebRootFileProvider.GetFileInfo(subpath);
            });
            services.AddScoped<IPreRenderer, DefaultPreRenderer>();

            services.AddSingleton(_ => configuration.GetSection("ComponentFactory").Get<ComponentFactoryOptions>());
            services.AddSingleton<IComponentFactory, DefaultComponentFactory>();

            services.AddSingleton(_ => configuration.GetSection("SitecoreConfiguration").Get<SitecoreConfiguration>());
            services.AddSingleton<ITrackingApi, LoggerTrackingApi>();
        }
    }
}
