using JssBlazor.Shared.Models.LayoutService;

namespace JssBlazor.RenderingHost.Services
{
    public interface ILayoutServiceResultProvider
    {
        LayoutServiceResult Result { get; set; }
    }
}
