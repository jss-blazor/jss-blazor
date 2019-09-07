using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.Logging;

namespace JssBlazor.RenderingHost.Services
{
    public class HardcodedRemoteUriHelper : IHostEnvironmentNavigationManager
    {

        public HardcodedRemoteUriHelper(ILogger<NavigationManager> logger)     
        {
        }


        public void Initialize(string baseUri, string uri)
        {
            // This overrides the URL requested by the Rendering Host (e.g., https://localhost/api/renderinghost)
            // with the base path of the URL (e.g., https://localhost/) for the client-side app, which almost
            // certainly won't have a page found at /api/renderinghost.
            //base.(baseUriAbsolute, baseUriAbsolute);
        }
    }
}
