using System;
using JssBlazor.Core.Models.LayoutService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JssBlazor.RenderingHost.Models
{
    public class FunctionArgsJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!(JToken.Load(reader) is JArray jArray)) return null;
            if (jArray.Count == 0) return null;

            var functionArgs = new FunctionArgs
            {
                RequestPath = jArray[0].ToString()
            };
            if (jArray.Count > 1)
            {
                var rawLayoutServiceResult = jArray[1].ToString();
                functionArgs.LayoutServiceResult = JsonConvert.DeserializeObject<LayoutServiceResult>(rawLayoutServiceResult);
            }
            if (jArray.Count > 2)
            {
                var rawViewBag = jArray[2].ToString();
                functionArgs.ViewBag = JsonConvert.DeserializeObject<ViewBag>(rawViewBag);
            }
            return functionArgs;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
