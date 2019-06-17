using System.Collections.Generic;

namespace JssBlazor.Shared.Models
{
    public class LayoutServiceResponse
    {
        public string Id { get; set; }
        public IDictionary<string, string> Fields { get; set; }
        public IDictionary<string, IList<ComponentDefinition>> Placeholders { get; set; }
    }
}
