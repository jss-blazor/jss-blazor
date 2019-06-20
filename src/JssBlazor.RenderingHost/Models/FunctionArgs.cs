using JssBlazor.Shared.Models.LayoutService;

namespace JssBlazor.RenderingHost.Models
{
    public class FunctionArgs
    {
        public string RequestPath { get; set; }
        public LayoutServiceResult LayoutServiceResult { get; set; }
        public ViewBag ViewBag { get; set; }
    }
}
