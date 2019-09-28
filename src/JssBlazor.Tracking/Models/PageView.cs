namespace JssBlazor.Tracking.Models
{
    public class PageView : ITrackingModel
    {
        public string PageId { get; set; }
        public string Url { get; set; }
    }
}
