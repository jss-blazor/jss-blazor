using Newtonsoft.Json.Linq;

namespace JssBlazor.Core.Models.LayoutService.Fields
{
    public class FieldValue
    {
        public string Rendered { get; set; }
        public JToken RawValue { get; set; }
    }
}
