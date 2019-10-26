using JssBlazor.Core.Models.LayoutService;
using Microsoft.AspNetCore.Components;

namespace JssBlazor.Components.Models
{
    public class JssComponentBase : ComponentBase
    {
        [Parameter]
        public ComponentDefinition Component { get; set; }
    }
}
