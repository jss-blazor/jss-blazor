using System;
using JssBlazor.Core.Models;
using JssBlazor.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JssBlazor.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJssBlazor(
            this IServiceCollection services,
            Action<JssBlazorOptions> setupAction)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (setupAction is null) throw new ArgumentNullException(nameof(setupAction));

            services.Configure(setupAction);
            services.AddSingleton(serviceProvider => serviceProvider.GetService<IOptions<JssBlazorOptions>>().Value.ComponentFactoryOptions);
            services.AddSingleton(serviceProvider => serviceProvider.GetService<IOptions<JssBlazorOptions>>().Value.SitecoreConfiguration);

            services.AddSingleton<IComponentFactory, DefaultComponentFactory>();
            services.AddSingleton<ILayoutService, RemoteLayoutService>();
            services.AddSingleton<IInitialStateLoader, ClientInitialStateLoader>();
        }
    }
}
