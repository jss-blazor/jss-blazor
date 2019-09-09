using System;

namespace JssBlazor.Core.Models
{
    public class SitecoreConfiguration
    {
        public string DefaultLanguage { get; set; }
        public Guid SitecoreApiKey { get; set; }
        public string SitecoreApiHost { get; set; }
    }
}
