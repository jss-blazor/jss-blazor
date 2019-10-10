using JssBlazor.Core.Models.LayoutService;
using JssBlazor.Core.Services;

namespace JssBlazor.Components.Models
{
    public class PlaceholderRenderEachContext
    {
        public ComponentDefinition Component { get; set; }
        public int Index { get; set; }
        public IComponentFactory ComponentFactory { get; set; }
    }
}
