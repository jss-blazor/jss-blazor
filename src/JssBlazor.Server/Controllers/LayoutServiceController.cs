using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace JssBlazor.Server.Controllers
{
    [Route("sitecore/api/layout")]
    public class LayoutServiceController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LayoutServiceController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        [HttpGet("[action]")]
        public async Task<string> Render(string item)
        {
            var routePath = ResolveRoute(item);
            var routeYml = await GetRouteYmlAsync(routePath);
            var routeJson = ConvertYmlToJson(routeYml);
            return routeJson;
        }

        private static string ResolveRoute(string item)
        {
            return "/routes/en.yml";
        }

        private async Task<string> GetRouteYmlAsync(string routePath)
        {
            var routeYml = _webHostEnvironment.WebRootFileProvider.GetFileInfo(routePath);
            using (var stream = routeYml.CreateReadStream())
            using (var reader = new StreamReader(stream))
            {
                var routeString = await reader.ReadToEndAsync();
                return routeString;
            }
        }

        private static string ConvertYmlToJson(string yml)
        {
            var deserializer = new Deserializer();
            var ymlObject = deserializer.Deserialize<object>(yml);
            using (var stringWriter = new StringWriter())
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(stringWriter, ymlObject);
                var json = stringWriter.ToString();
                return json;
            }
        }
    }
}
