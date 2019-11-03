using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace JssBlazor.RenderingHost.Services
{
    public class EmptyView : IView
    {
        public static IView Default => new EmptyView();

        public string Path { get; } = string.Empty;

        public Task RenderAsync(ViewContext context)
        {
            return Task.CompletedTask;
        }
    }
}
