using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JssBlazor.Core.Models.LayoutService.Fields
{
    public class MultilistItem
    {
        public Guid Id { get; set; }

        [JsonProperty(ItemConverterType = typeof(FieldJsonConverter))]
        public IDictionary<string, Field> Fields { get; set; }
    }
}
