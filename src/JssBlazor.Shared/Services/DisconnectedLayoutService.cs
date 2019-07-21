using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JssBlazor.Shared.Models.Disconnected;
using JssBlazor.Shared.Models.LayoutService;

namespace JssBlazor.Shared.Services
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

        public async Task<LayoutServiceResult> GetRouteAsync(string path)
        {
            var route = await _routeResolver.GetRouteAsync(path);
            var result = new LayoutServiceResult
            {
                Sitecore = new RenderingData
                {
                    Context = new SitecoreContext
                    {
                        Language = "en",
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
                        ItemLanguage = "en",
                        ItemVersion = 1,
                        LayoutId = AvailableInConnectedModeId,
                        TemplateId = AvailableInConnectedModeId,
                        TemplateName = AvailableInConnectedMode,
                        Name = route.Id,
                        Fields = MapFields(route.Fields),
                        Placeholders = MapPlaceholders(route.Placeholders)
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
