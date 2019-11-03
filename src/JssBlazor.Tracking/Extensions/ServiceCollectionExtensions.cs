using System;
using JssBlazor.Tracking.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JssBlazor.Tracking.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJssBlazorTracking(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            services.AddTransient<ITrackingApi, TrackingApi>();
        }
    }
}
