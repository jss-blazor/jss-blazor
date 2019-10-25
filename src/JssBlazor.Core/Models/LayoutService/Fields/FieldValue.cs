using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JssBlazor.Core.Models.LayoutService.Fields
{
    public class FieldValue
    {
        public string Rendered { get; set; }
        public JToken RawValue { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalData { get; set; }
    }
}
