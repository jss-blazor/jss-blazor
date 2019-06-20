using System.Collections.Generic;

namespace JssBlazor.RenderingHost.Models
{
    public class ViewBagRequest
    {
        public string Url { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> QueryString { get; set; }
        public string UserAgent { get; set; }
    }
}
