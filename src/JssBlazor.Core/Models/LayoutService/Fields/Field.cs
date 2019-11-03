using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JssBlazor.Core.Models.LayoutService.Fields
{
    public class Field
    {
        [JsonConverter(typeof(FieldValueJsonConverter))]
        public FieldValue Value { get; set; }

        public string Editable { get; set; }

        public string Id { get; set; }

        public string Url { get; set; }

        public IDictionary<string, Field> Fields { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalData { get; set; }

        public T GetFieldValue<T>()
        {
            var rawValue = Value?.RawValue;
            return rawValue == null ? default : rawValue.Value<T>();
        }
    }
}
