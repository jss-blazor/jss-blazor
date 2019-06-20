using System.Collections.Generic;

namespace JssBlazor.RenderingHost.Models
{
    public class ViewBag
    {
        public string ItemLanguage { get; set; }
        public IDictionary<string, string> AppDictionary { get; set; }
        public ViewBagHttpContext HttpContext { get; set; }
    }
}
