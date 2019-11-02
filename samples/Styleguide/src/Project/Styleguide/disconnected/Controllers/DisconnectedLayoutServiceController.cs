using System;
using System.Threading.Tasks;
using JssBlazor.Core.Models.LayoutService;
using JssBlazor.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace JssBlazor.Project.Styleguide.DisconnectedServer
{
    [Route("sitecore/api/layout")]
    public class DisconnectedLayoutServiceController : Controller
    {
        private readonly ILayoutService _layoutService;

        public DisconnectedLayoutServiceController(ILayoutService layoutService)
        {
            _layoutService = layoutService ?? throw new ArgumentNullException(nameof(layoutService));
        }

        [HttpGet("[action]")]
        public async Task<LayoutServiceResult> Render(string item, string sc_lang)
        {
            var result = await _layoutService.GetRouteDataAsync(item, sc_lang);
            return result;
        }
    }
}
