using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;

namespace JssBlazor.RenderingHost.Models
{
    public class RenderRequest
    {
        public string Id { get; set; }

        [JsonConverter(typeof(FunctionArgsJsonConverter))]
        public FunctionArgs Args { get; set; }

        public string FunctionName { get; set; }

        public string ModuleName { get; set; }
    }
}
