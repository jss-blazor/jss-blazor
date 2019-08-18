using System;
using System.IO;
using System.Threading.Tasks;
using JssBlazor.Core.Models.Disconnected;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace JssBlazor.Core.Services
{
    public class YamlRouteResolver : IRouteResolver
    {
        private readonly Func<string, IFileInfo> _fileInfoFactory;

        public YamlRouteResolver(Func<string, IFileInfo> fileInfoFactory)
        {
            _fileInfoFactory = fileInfoFactory ?? throw new ArgumentNullException(nameof(fileInfoFactory));
        }

        public async Task<string> GetRouteJsonAsync(string item)
        {
            var routePath = ResolveRoute(item);
            var routeYml = await GetRouteYmlAsync(routePath);
            var routeJson = ConvertYmlToJson(routeYml);
            return routeJson;
        }

        public async Task<DisconnectedRoute> GetRouteAsync(string item)
        {
            var routeJson = await GetRouteJsonAsync(item);
            var route = JsonConvert.DeserializeObject<DisconnectedRoute>(routeJson);
            return route;
        }

        private static string ResolveRoute(string item)
        {
            return "/routes/en.yml";
        }

        private async Task<string> GetRouteYmlAsync(string routePath)
        {
            var routeYml = _fileInfoFactory(routePath);
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
