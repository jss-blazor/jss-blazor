using System;
using JssBlazor.RenderingHost.Services;
using JssBlazor.Shared.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JssBlazor.Shared.Models.LayoutService
{    
    public class Field
    {
        [JsonConverter(typeof(ValueJsonConverter))]
        public IFieldValue Value { get; set; }
        public string Editable { get; set; }
    }
}
