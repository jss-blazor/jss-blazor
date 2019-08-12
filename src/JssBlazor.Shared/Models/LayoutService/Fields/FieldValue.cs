using Newtonsoft.Json.Linq;

namespace JssBlazor.Shared.Models.LayoutService.Fields
{
    public class FieldValue : IFieldValue
    {
        public string Rendered { get; set; }
        public JToken RawValue { get; set; }
    }
}
