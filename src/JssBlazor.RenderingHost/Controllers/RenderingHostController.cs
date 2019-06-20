using System;
using System.IO;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using JssBlazor.Client;
using JssBlazor.RenderingHost.Models;
using JssBlazor.RenderingHost.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace JssBlazor.RenderingHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RenderingHostController : Controller
    {
        private readonly ILayoutServiceResultProvider _layoutServiceResultProvider;
        private readonly IHtmlHelper _htmlHelper;

        public RenderingHostController(
            ILayoutServiceResultProvider layoutServiceResultProvider,
            IHtmlHelper htmlHelper)
        {
            _layoutServiceResultProvider = layoutServiceResultProvider ?? throw new ArgumentNullException(nameof(layoutServiceResultProvider));
            _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
        }

        [HttpPost]
        public async Task<RenderResult> Post([FromBody]RenderRequest renderRequest)
        {
            _layoutServiceResultProvider.Result = renderRequest.FunctionArgs.LayoutServiceResult;
            var appHtml = await ServerSideRenderApp();
            var resultModel = new RenderResult
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
