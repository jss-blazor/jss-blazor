using JssBlazor.Core.Models.LayoutService;

namespace JssBlazor.RenderingHost.Services
{
    public interface ILayoutServiceResultProvider
    {
        LayoutServiceResult Result { get; set; }
    }
}
