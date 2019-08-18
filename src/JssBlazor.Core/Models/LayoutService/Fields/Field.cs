using Newtonsoft.Json;

namespace JssBlazor.Core.Models.LayoutService.Fields
{
    public class Field
    {
        [JsonConverter(typeof(FieldValueJsonConverter))]
        public IFieldValue Value { get; set; }

        public string Editable { get; set; }
    }
}
