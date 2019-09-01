namespace JssBlazor.Core.Models.LayoutService
{
    public class LayoutServiceResult
    {
        public RenderingData Sitecore { get; set; }
        public string Route { get; set; }
        public string RawContext { get; set; }
        public string RawRouteContext { get; set; }
    }
}
