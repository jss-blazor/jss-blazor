using System;
using System.Threading.Tasks;
using JssBlazor.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace JssBlazor.Server.Controllers
{
    [Route("sitecore/api/layout")]
    public class LayoutServiceController : Controller
    {
        private readonly IRouteResolver _routeResolver;

        public LayoutServiceController(IRouteResolver routeResolver)
        {
            _routeResolver = routeResolver ?? throw new ArgumentNullException(nameof(routeResolver));
        }

        [HttpGet("[action]")]
        public async Task<string> Render(string item)
        {
            var routeJson = await _routeResolver.GetRouteJsonAsync(item);
            return routeJson;
        }
    }
}
