using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JssBlazor.Core.Models.Disconnected;
using JssBlazor.Core.Models.LayoutService;
using JssBlazor.Core.Models.LayoutService.Fields;

namespace JssBlazor.Core.Services
{
    public class DisconnectedLayoutService : ILayoutService
    {
        private const string AvailableInConnectedMode = "available-in-connected-mode";
        private static readonly Guid AvailableInConnectedModeId = Guid.Empty;

        private readonly IRouteResolver _routeResolver;

        public DisconnectedLayoutService(IRouteResolver routeResolver)
        {
            _routeResolver = routeResolver ?? throw new ArgumentNullException(nameof(routeResolver));
        }

        public async Task<LayoutServiceResult> GetRouteDataAsync(string route, string language)
        {
            var disconnectedRoute = await _routeResolver.GetRouteAsync(route);
            var result = new LayoutServiceResult
            {
                Sitecore = new RenderingData
                {
                    Context = new SitecoreContext
                    {
                        Language = language,
                        PageEditing = false,
                        PageState = "normal",
                        Site = new Site
                        {
                            Name = "JssDisconnectedLayoutService"
                        }
                    },
                    Route = new RenderedItem
                    {
                        DatabaseName = AvailableInConnectedMode,
                        DeviceId = AvailableInConnectedModeId,
                        DisplayName = AvailableInConnectedMode,
                        ItemId = AvailableInConnectedModeId,
                        ItemLanguage = language,
                        ItemVersion = 1,
                        LayoutId = AvailableInConnectedModeId,
                        TemplateId = AvailableInConnectedModeId,
                        TemplateName = AvailableInConnectedMode,
                        Name = disconnectedRoute.Id,
                        Fields = MapFields(disconnectedRoute.Fields),
                        Placeholders = MapPlaceholders(disconnectedRoute.Placeholders)
                    }

                }
            };
            return result;
        }

        protected virtual IDictionary<string, Field> MapFields(IDictionary<string, string> disconnectedFields)
        {
            return disconnectedFields.ToDictionary(kvp => kvp.Key, kvp => new Field {});
        }

        protected virtual IDictionary<string, IEnumerable<ComponentDefinition>> MapPlaceholders(
            IDictionary<string, IEnumerable<DisconnectedComponentDefinition>> disconnectedPlaceholders)
        {
            var connectedPlaceholders = new Dictionary<string, IEnumerable<ComponentDefinition>>();
            foreach (var disconnectedPlaceholder in disconnectedPlaceholders)
            {
                var connectedComponents = disconnectedPlaceholder.Value.Select(MapComponentDefinition).ToList();
                connectedPlaceholders.Add(disconnectedPlaceholder.Key, connectedComponents);
            }
            return connectedPlaceholders;
        }

        protected virtual ComponentDefinition MapComponentDefinition(
            DisconnectedComponentDefinition disconnectedComponentDefinition)
        {
            var connectedComponentDefinition = new ComponentDefinition
            {
                Uid = AvailableInConnectedModeId,
                ComponentName = disconnectedComponentDefinition.ComponentName,
                DataSource = AvailableInConnectedModeId,
                Fields = MapFields(disconnectedComponentDefinition.Fields)
            };
            return connectedComponentDefinition;
        }
    }
}
