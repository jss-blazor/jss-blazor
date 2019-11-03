using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JssBlazor.Core.Models.LayoutService.Fields
{
    public class FieldValueJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jToken = JToken.Load(reader);
            if (jToken.Type == JTokenType.Array)
            {
                var field = new MultilistFieldValue
                {
                    Items = jToken.ToObject<IEnumerable<MultilistItem>>(),
                    Rendered = jToken.ToString(),
                    RawValue = jToken
                };
                return field;
            }

            var fieldValue = jToken.Type switch
            {
                JTokenType.String => new FieldValue
                {
                    Rendered = jToken.Value<string>()
                },
                _ => new FieldValue
                {
                    Rendered = jToken.ToString()
                },
            };
            fieldValue.RawValue = jToken;
            return fieldValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is FieldValue fieldValue)) return;
            if (fieldValue.RawValue == null) return;
            serializer.Serialize(writer, fieldValue.RawValue);
        }
    }
}
