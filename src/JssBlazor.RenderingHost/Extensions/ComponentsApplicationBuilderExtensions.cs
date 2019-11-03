using Microsoft.AspNetCore.Builder;

namespace JssBlazor.RenderingHost.Extensions
{
    public static class ComponentsApplicationBuilderExtensions
    {
        public static void UseJssBlazorRenderingHost(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
