using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.Logging;

namespace JssBlazor.RenderingHost.Services
{
    public class HardcodedRemoteUriHelper : RemoteUriHelper
    {
        public HardcodedRemoteUriHelper(ILogger<RemoteUriHelper> logger)
         : base(logger)
        {
        }

        public override void InitializeState(string uriAbsolute, string baseUriAbsolute)
        {
            // This overrides the URL requested by the Rendering Host (e.g., https://localhost/api/renderinghost)
            // with the base path of the URL (e.g., https://localhost/) for the client-side app, which almost
            // certainly won't have a page found at /api/renderinghost.
            base.InitializeState(baseUriAbsolute, baseUriAbsolute);
        }
    }
}
