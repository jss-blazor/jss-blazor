using System;
using System.Collections.Generic;
using System.Linq;
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
            JToken jToken = JToken.ReadFrom(reader);
            var jTokenType = jToken.Type.ToString().ToLower();

            IFieldValue fieldValue;
            switch (jTokenType)
            {
                case "array":
                    fieldValue = new ArrayFieldValue
                    {
                        FieldValue = jToken.Values<JToken>().ToList().AsReadOnly()
                    };
                    break;
                case "boolean":
                    fieldValue = new BooleanFieldValue
                    {
                        FieldValue = jToken.Value<bool>()
                    };
                    break;
                case "date":
                    fieldValue = new DateFieldValue
                    {
                        FieldValue = jToken.Value<DateTime>()
                    };
                    break;
                case "string":
                    fieldValue = new FieldValue
                    {
                        Rendered = jToken.Value<string>()
                    };
                    break;
                default:
                    fieldValue = new FieldValue
                    {
                        Rendered = jToken.ToString()
                    };
                    break;
            }
            fieldValue.RawValue = jToken;

            return fieldValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            base.ToString();
        }
    }
}
