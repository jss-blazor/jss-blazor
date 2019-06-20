using JssBlazor.Shared.Models.LayoutService;
using Newtonsoft.Json;

namespace JssBlazor.RenderingHost.Models
{
    public class RenderRequest
    {
        public string Id { get; set; }
        public string[] Args { get; set; }
        public string FunctionName { get; set; }
        public string ModuleName { get; set; }
        
        private FunctionArgs _functionArgs;
        public FunctionArgs FunctionArgs
        {
            // Move into a custom JSON Serializer
            get
            {
                if (_functionArgs != null) return _functionArgs;
                if (Args == null || Args.Length == 0) return null;

                var functionArgs = new FunctionArgs
                {
                    RequestPath = Args[0]
                };
                if (Args.Length > 1)
                {
                    functionArgs.LayoutServiceResult = JsonConvert.DeserializeObject<LayoutServiceResult>(Args[1]);
                }
                if (Args.Length > 2)
                {
                    functionArgs.ViewBag = JsonConvert.DeserializeObject<ViewBag>(Args[2]);
                }

                return _functionArgs = functionArgs;
            }
        }
    }
}
