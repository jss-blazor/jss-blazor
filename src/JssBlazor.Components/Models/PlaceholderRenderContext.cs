using System.Collections.Generic;
using JssBlazor.Core.Models.LayoutService;
using JssBlazor.Core.Services;

namespace JssBlazor.Components.Models
{
    public class PlaceholderRenderContext
    {
        public IEnumerable<ComponentDefinition> Components { get; set; }
        public string Name { get; set; }
        public IRendering Rendering { get; set; }
        public IComponentFactory ComponentFactory { get; set; }
    }
}
