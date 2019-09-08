using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace JssBlazor.RenderingHost.Services
{
    public class HardcodedRemoteNavigationManager : NavigationManager, IHostEnvironmentNavigationManager
    {
        private readonly ILogger<HardcodedRemoteNavigationManager> _logger;
        private IJSRuntime _jsRuntime;

        public HardcodedRemoteNavigationManager(ILogger<HardcodedRemoteNavigationManager> logger)
        {
            _logger = logger;
        }

        public bool HasAttachedJSRuntime => _jsRuntime != null;

        public new void Initialize(string baseUri, string uri)
        {
            // This overrides the URL requested by the Rendering Host (e.g., https://localhost/api/renderinghost)
            // with the base path of the URL (e.g., https://localhost/) for the client-side app, which almost
            // certainly won't have a page found at /api/renderinghost.
            base.Initialize(baseUri, baseUri);
            NotifyLocationChanged(isInterceptedLink: false);
        }

        public void AttachJsRuntime(IJSRuntime jsRuntime)
        {
            if (_jsRuntime != null)
            {
                throw new InvalidOperationException("JavaScript runtime already initialized.");
            }

            _jsRuntime = jsRuntime;
        }

        public void NotifyLocationChanged(string uri, bool intercepted)
        {
            Log.ReceivedLocationChangedNotification(_logger, uri, intercepted);

            Uri = uri;
            NotifyLocationChanged(intercepted);
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            Log.RequestingNavigation(_logger, uri, forceLoad);

            if (_jsRuntime == null)
            {
                var absoluteUriString = ToAbsoluteUri(uri).ToString();
                throw new NavigationException(absoluteUriString);
            }

            _jsRuntime.InvokeAsync<object>("Blazor._internal.navigationManager.navigateTo", uri, forceLoad);
        }

        private static class Log
        {
            private static readonly Action<ILogger, string, bool, Exception> _requestingNavigation =
                LoggerMessage.Define<string, bool>(LogLevel.Debug, new EventId(1, "RequestingNavigation"), "Requesting navigation to URI {Uri} with forceLoad={ForceLoad}");

            private static readonly Action<ILogger, string, bool, Exception> _receivedLocationChangedNotification =
                LoggerMessage.Define<string, bool>(LogLevel.Debug, new EventId(2, "ReceivedLocationChangedNotification"), "Received notification that the URI has changed to {Uri} with isIntercepted={IsIntercepted}");

            public static void RequestingNavigation(ILogger logger, string uri, bool forceLoad)
            {
                _requestingNavigation(logger, uri, forceLoad, null);
            }

            public static void ReceivedLocationChangedNotification(ILogger logger, string uri, bool isIntercepted)
            {
                _receivedLocationChangedNotification(logger, uri, isIntercepted, null);
            }
        }
    }
}
