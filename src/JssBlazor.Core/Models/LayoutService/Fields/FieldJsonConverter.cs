using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JssBlazor.Core.Models.LayoutService.Fields
{
    public class FieldJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jToken = JToken.Load(reader);
            if (jToken.Type == JTokenType.Object)
            {
                if (jToken.SelectToken("value.href") != null)
                {
                    return jToken.ToObject<LinkField>();
                }
            }
            return jToken.ToObject<Field>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
