using System;
using System.Collections.Generic;
using JssBlazor.Shared.Models.LayoutService.Fields;

namespace JssBlazor.Shared.Models.LayoutService
{
    public class ComponentDefinition
    {
        public Guid Uid { get; set; }
        public string ComponentName { get; set; }
        public Guid? DataSource { get; set; }
        public IDictionary<string, Field> Fields { get; set; }
        public IDictionary<string, IEnumerable<ComponentDefinition>> Placeholders { get; set; }

        // Properties used by "raw components"
        public string Name { get; set; }
        public IDictionary<string, string> Attributes { get; set; }
        public string Contents { get; set; }
    }
}
