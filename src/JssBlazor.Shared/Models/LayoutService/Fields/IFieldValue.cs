using Newtonsoft.Json.Linq;

namespace JssBlazor.Shared.Models.LayoutService.Fields
{
    public interface IFieldValue
    {
        string Rendered { get; set; }
        JToken RawValue { get; set; }
    }
}
