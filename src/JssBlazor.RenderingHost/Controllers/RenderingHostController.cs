using System;
using System.Net;
using System.Threading.Tasks;
using JssBlazor.RenderingHost.Models;
using JssBlazor.RenderingHost.Services;
using Microsoft.AspNetCore.Mvc;

namespace JssBlazor.RenderingHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RenderingHostController : Controller
    {
        private readonly ILayoutServiceResultProvider _layoutServiceResultProvider;
        private readonly BlazorAppConfiguration _blazorAppConfiguration;
        private readonly IPreRenderer _preRenderer;

        public RenderingHostController(
            ILayoutServiceResultProvider layoutServiceResultProvider,
            BlazorAppConfiguration blazorAppConfiguration,
            IPreRenderer preRenderer)
        {
            _layoutServiceResultProvider = layoutServiceResultProvider ?? throw new ArgumentNullException(nameof(layoutServiceResultProvider));
            _blazorAppConfiguration = blazorAppConfiguration ?? throw new ArgumentNullException(nameof(blazorAppConfiguration));
            _preRenderer = preRenderer ?? throw new ArgumentNullException(nameof(preRenderer));
        }

        [HttpPost]
        public async Task<RenderResult> Post([FromBody]RenderRequest renderRequest)
        {
            try
            {
                var layoutServiceResult = renderRequest.Args.LayoutServiceResult;
                _layoutServiceResultProvider.Result = layoutServiceResult;

                var appType = _blazorAppConfiguration.AppComponentType;
                var appDomElementSelector = _blazorAppConfiguration.AppDomElementSelector;
                var pageEditing = layoutServiceResult.Sitecore.Context.PageEditing;
                var appHtml = await _preRenderer.RenderAppAsync(appType, appDomElementSelector, ControllerContext, ViewData, TempData, pageEditing);
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
