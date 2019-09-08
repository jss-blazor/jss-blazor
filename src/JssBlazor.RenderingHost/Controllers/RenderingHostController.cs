using System;
using System.Net;
using System.Threading.Tasks;
using JssBlazor.Core.Services;
using JssBlazor.RenderingHost.Models;
using JssBlazor.RenderingHost.Services;
using JssBlazor.StyleGuide;
using Microsoft.AspNetCore.Mvc;

namespace JssBlazor.RenderingHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RenderingHostController : Controller
    {
        private readonly ILayoutServiceResultProvider _layoutServiceResultProvider;
        private readonly ILayoutService _layoutService;
        private readonly IPreRenderer _preRenderer;

        public RenderingHostController(
            ILayoutServiceResultProvider layoutServiceResultProvider,
            ILayoutService layoutService,
            IPreRenderer preRenderer)
        {
            _layoutServiceResultProvider = layoutServiceResultProvider ?? throw new ArgumentNullException(nameof(layoutServiceResultProvider));
            _layoutService = layoutService ?? throw new ArgumentNullException(nameof(layoutService));
            _preRenderer = preRenderer ?? throw new ArgumentNullException(nameof(preRenderer));
        }

        [HttpPost]
        public async Task<RenderResult> Post([FromBody]RenderRequest renderRequest)
        {
            try
            {
                _layoutServiceResultProvider.Result = renderRequest.FunctionArgs.LayoutServiceResult;
                _layoutServiceResultProvider.Result.Route = renderRequest.Args[0];
                _layoutServiceResultProvider.Result.RawContext = renderRequest.Args[1];
                _layoutServiceResultProvider.Result.RawRouteContext = renderRequest.Args[2];

                _layoutService.Current = _layoutServiceResultProvider.Result;
                ViewData.Add("layoutServiceResult", _layoutServiceResultProvider.Result);

                var appHtml = await _preRenderer.RenderAppAsync<App>("app", ControllerContext, ViewData, TempData);
                var resultModel = new RenderResult
                {
                    Html = appHtml,
                    Status = (int)HttpStatusCode.OK
                };
                return resultModel;
            }
            catch (Exception ex)
            {
                return new RenderResult
                {
                    Html = $"<html><body><h1>An error occurred during server-side rendering.</h1><div>{ex.Message}</div></body></html>",
                    Status = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
