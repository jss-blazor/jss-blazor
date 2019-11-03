using System;
using JssBlazor.Core.Models;

namespace JssBlazor.Components.Models
{
    public class JssBlazorOptions
    {
        public ComponentFactoryOptions ComponentFactoryOptions { get; }
        public SitecoreConfiguration SitecoreConfiguration { get; }

        public string ComponentAssemblyFormat
        {
            get => ComponentFactoryOptions.ComponentAssemblyFormat;
            set => ComponentFactoryOptions.ComponentAssemblyFormat = value;
        }

        public string DefaultLanguage
        {
            get => SitecoreConfiguration.DefaultLanguage;
            set => SitecoreConfiguration.DefaultLanguage = value;
        }

        public Guid SitecoreApiKey
        {
            get => SitecoreConfiguration.SitecoreApiKey;
            set => SitecoreConfiguration.SitecoreApiKey = value;
        }

        public string SitecoreApiHost {
            get => SitecoreConfiguration.SitecoreApiHost;
            set => SitecoreConfiguration.SitecoreApiHost = value;
        }

        public JssBlazorOptions()
        {
            ComponentFactoryOptions = new ComponentFactoryOptions();
            SitecoreConfiguration = new SitecoreConfiguration();
        }
    }
}
