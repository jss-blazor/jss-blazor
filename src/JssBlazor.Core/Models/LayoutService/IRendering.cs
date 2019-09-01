using System;
using System.Collections.Generic;
using JssBlazor.Core.Models.LayoutService.Fields;

namespace JssBlazor.Core.Models.LayoutService
{
    public interface IRendering
    {
        Guid Uid { get; set; }
        string Name { get; set; }
        IDictionary<string, Field> Fields { get; set; }
        IDictionary<string, IEnumerable<ComponentDefinition>> Placeholders { get; set; }
        IDictionary<string, object> Params { get; set; }
    }
}
