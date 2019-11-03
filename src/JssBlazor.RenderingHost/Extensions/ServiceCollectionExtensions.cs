using System;
using System.Net.Http;
using JssBlazor.Components.Models;
using JssBlazor.Components.Services;
using JssBlazor.Core.Models;
using JssBlazor.Core.Services;
using JssBlazor.RenderingHost.Controllers;
using JssBlazor.RenderingHost.Models;
using JssBlazor.RenderingHost.Services;
using JssBlazor.Tracking;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JssBlazor.RenderingHost.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJssBlazorRenderingHost(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<JssBlazorRenderingHostOptions> setupAction)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));
            if (setupAction is null) throw new ArgumentNullException(nameof(setupAction));

            services.AddFrameworkServices();
            services.OverrideBlazorServices();
            services.AddJssBlazorServices(configuration, setupAction);
        }

        private static void AddFrameworkServices(this IServiceCollection services)
        {
            services.AddMvc()
                .AddApplicationPart(typeof(RenderingHostController).Assembly)
                .AddControllersAsServices()
                .AddNewtonsoftJson();
            services.AddHttpContextAccessor();
        }

        private static void OverrideBlazorServices(this IServiceCollection services)
        {
            // Replace Blazor's out-of-the-box NavigationManager with one that correctly resolves URLs server side.
            services.AddScoped<NavigationManager, HardcodedRemoteNavigationManager>();

            // An HttpClient is registered out-of-the-box with Blazor WebAssembly.
            services.AddTransient(serviceProvider =>
            {
                var navigationManager = serviceProvider.GetRequiredService<NavigationManager>();
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(navigationManager.BaseUri)
                };
                return httpClient;
            });
        }

        private static void AddJssBlazorServices(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<JssBlazorRenderingHostOptions> setupAction)
        {
            services.Configure(setupAction);
            services.AddSingleton(serviceProvider => serviceProvider.GetService<IOptions<JssBlazorRenderingHostOptions>>().Value);
            services.AddSingleton(_ => configuration.GetSection("ComponentFactory").Get<ComponentFactoryOptions>());
            services.AddSingleton(_ => configuration.GetSection("SitecoreConfiguration").Get<SitecoreConfiguration>());

            services.AddSingleton<IComponentFactory, DefaultComponentFactory>();
            services.AddSingleton<ILayoutServiceResultProvider, StaticLayoutServiceResultProvider>();
            services.AddSingleton<ILayoutService, StaticLayoutService>();
            services.AddSingleton<IInitialStateLoader, ServerInitialStateLoader>();
            services.AddScoped<IHeadService, ServerHeadService>();

            services.AddScoped<IPreRenderer, DefaultPreRenderer>();

            services.AddJssBlazorTracking();
        }
    }
}
