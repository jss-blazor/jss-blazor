using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace JssBlazor.Components.Routing
{
    public class DynamicRouter : IComponent, IHandleAfterRender, IDisposable
    {
        private static readonly ReadOnlyDictionary<string, object> EmptyParametersDictionary =
            new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

        private RenderHandle _renderHandle;
        private bool _navigationInterceptionEnabled;
        private bool _disposed;

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private INavigationInterception NavigationInterception { get; set; }

        [Parameter]
        public Type Layout { get; set; }

        /// <summary>
        /// Gets or sets the content to display when no match is found for the requested route.
        /// </summary>
        [Parameter]
        public RenderFragment NotFound { get; set; }

        /// <summary>
        /// Gets or sets the content to display when a match is found for the requested route.
        /// </summary>
        [Parameter]
        public RenderFragment<RouteData> Found { get; set; }

        /// <inheritdoc />
        public void Attach(RenderHandle renderHandle)
        {
            _renderHandle = renderHandle;
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        /// <inheritdoc />
        public Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);

            if (Layout == null)
            {
                throw new InvalidOperationException($"The {nameof(DynamicRouter)} component requires a value for the parameter {nameof(Layout)}.");
            }

            // Found content is mandatory, because even though we could use something like <RouteView ...> as a
            // reasonable default, if it's not declared explicitly in the template then people will have no way
            // to discover how to customize this (e.g., to add authorization).
            if (Found == null)
            {
                throw new InvalidOperationException($"The {nameof(DynamicRouter)} component requires a value for the parameter {nameof(Found)}.");
            }

            // NotFound content is mandatory, because even though we could display a default message like "Not found",
            // it has to be specified explicitly so that it can also be wrapped in a specific layout
            if (NotFound == null)
            {
                throw new InvalidOperationException($"The {nameof(DynamicRouter)} component requires a value for the parameter {nameof(NotFound)}.");
            }

            Refresh();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                NavigationManager.LocationChanged -= OnLocationChanged;
            }

            _disposed = true;
        }

        private void Refresh()
        {
            var routeData = new RouteData(Layout, EmptyParametersDictionary);
            _renderHandle.Render(Found(routeData));
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs args)
        {
            if (_renderHandle.IsInitialized)
            {
                Refresh();
            }
        }

        Task IHandleAfterRender.OnAfterRenderAsync()
        {
            if (!_navigationInterceptionEnabled)
            {
                _navigationInterceptionEnabled = true;
                return NavigationInterception.EnableNavigationInterceptionAsync();
            }

            return Task.CompletedTask;
        }
    }
}
