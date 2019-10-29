namespace JssBlazor.Core.Models
{
    public class JssBlazorOptions
    {
        public ComponentFactoryOptions ComponentFactoryOptions { get; }

        public SitecoreConfiguration SitecoreConfiguration { get; set; }

        public JssBlazorOptions()
        {
            ComponentFactoryOptions = new ComponentFactoryOptions();
            SitecoreConfiguration = new SitecoreConfiguration();
        }
    }
}
