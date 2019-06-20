using System;
using System.Collections.Generic;

namespace JssBlazor.Shared.Models.LayoutService
{
    public class RenderedItem
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IDictionary<string, Field> Fields { get; set; }
        public string DatabaseName { get; set; }
        public Guid DeviceId { get; set; }
        public Guid ItemId { get; set; }
        public string ItemLanguage { get; set; }
        public int ItemVersion { get; set; }
        public Guid LayoutId { get; set; }
        public Guid TemplateId { get; set; }
        public string TemplateName { get; set; }
        public IDictionary<string, IEnumerable<ComponentDefinition>> Placeholders { get; set; }
    }
}
