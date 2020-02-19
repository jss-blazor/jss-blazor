using JssBlazor.Components.Extensions;
using JssBlazor.Tracking.Extensions;
using Microsoft.AspNetCore.Blazor.Hosting;
using System.Threading.Tasks;

namespace JssBlazor.Project.Styleguide.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            await builder.Build()
                .UseJssBlazorComponents()
                .UseJssBlazorTracking()
                .RunAsync();
        }
    }
}
