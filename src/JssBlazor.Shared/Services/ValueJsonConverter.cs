using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JssBlazor.Shared.Models.LayoutService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JssBlazor.RenderingHost.Services
{
    public class ValueJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jObject = JToken.ReadFrom(reader);
            var fieldValue = new FieldValue();
            fieldValue.RawValue = jObject;

            switch (jObject.Type.ToString().ToLower())
            {
                case "string":
                    {
                        fieldValue.Rendered = jObject.Value<string>();
                        break;
                    }
                default:
                    {
                        fieldValue.Rendered = "Gods";
                        break;
                    }

            }

            return fieldValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            base.ToString();
        }
    }
}
