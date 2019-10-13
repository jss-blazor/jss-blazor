using System;

namespace JssBlazor.RenderingHost.Models
{
    public class BlazorAppConfiguration
    {
        public Type AppComponentType { get; set; }
        public string AppDomElementSelector { get; set; }
    }
}
