using System;
using System.IO;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using JssBlazor.Client;
using JssBlazor.RenderingHost.Models;
using JssBlazor.RenderingHost.Services;
using JssBlazor.Shared.Models;
using JssBlazor.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace JssBlazor.RenderingHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RenderingHostController : Controller
    {
        private readonly IRouteResolver _routeResolver;
        private readonly ILayoutService _layoutService;
        private readonly IHtmlHelper _htmlHelper;

        public RenderingHostController(
            IRouteResolver routeResolver,
            ILayoutService layoutService,
            IHtmlHelper htmlHelper)
        {
            _routeResolver = routeResolver ?? throw new ArgumentNullException(nameof(routeResolver));
            _layoutService = layoutService ?? throw new ArgumentNullException(nameof(layoutService));
            _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
        }

        [HttpPost]
        public async Task<ResultModel> Post([FromBody]RenderRequest renderRequest)
        {
            // Layout Service data will eventually come from Sitecore by reading renderRequest.Args.
            // For now, read it from the YAML routes hosted in the Rendering Host.
            var routeJson = await _routeResolver.GetRouteJsonAsync(null);
            if (_layoutService is LocalLayoutService localLayoutService)
            {
                localLayoutService.Route = JsonConvert.DeserializeObject<LayoutServiceResponse>(routeJson);
            }

            var appHtml = await ServerSideRenderApp();
            var resultModel = new ResultModel
            {
                Html = appHtml,
                Status = (int)HttpStatusCode.OK
            };
            return resultModel;
        }

        private async Task<string> ServerSideRenderApp()
        {
            await using var viewWriter = new StringWriter();
            var viewContext = new ViewContext(ControllerContext, EmptyView.Default, ViewData, TempData, viewWriter, new HtmlHelperOptions());

            var helper = (HtmlHelper)_htmlHelper;
            helper.Contextualize(viewContext);
            var appHtml = await _htmlHelper.RenderComponentAsync<App>();

            await using var appHtmlWriter = new StringWriter();
            appHtml.WriteTo(appHtmlWriter, HtmlEncoder.Default);

            return appHtmlWriter.ToString();
        }
    }
}
