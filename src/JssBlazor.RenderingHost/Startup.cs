using System;
using System.Linq;
using JssBlazor.RenderingHost.Services;
using JssBlazor.Core.Models;
using JssBlazor.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace JssBlazor.RenderingHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddHttpContextAccessor();

            services.AddSingleton<Func<string, IFileInfo>>(serviceProvider => subpath =>
            {
                var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
                return webHostEnvironment.WebRootFileProvider.GetFileInfo(subpath);
            });
            services.AddSingleton<IRouteResolver, YamlRouteResolver>();
            services.AddSingleton<ILayoutServiceResultProvider, StaticLayoutServiceResultProvider>();
            services.AddSingleton<ILayoutService, StaticLayoutService>();

            // Required to render JssBlazor.StyleGuide on the server.
            services.AddServerSideBlazor();
            // Replace Blazor's out-of-the-box IUriHelper with one that correctly resolves URLs server side.
            services.AddScoped<NavigationManager, HardcodedRemoteUriHelper>();

            services.AddSingleton<Func<string, IFileInfo>>(serviceProvider => (subpath) =>
            {
                var webHostEnvironment = serviceProvider.GetService<IWebHostEnvironment>();
                return webHostEnvironment.WebRootFileProvider.GetFileInfo(subpath);
            });
            services.AddScoped<IPreRenderer, DefaultPreRenderer>();

            services.AddSingleton(_ => Configuration.GetSection("ComponentFactory").Get<ComponentFactoryOptions>());
            services.AddSingleton<IComponentFactory, DefaultComponentFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
            });
        }
    }
}
