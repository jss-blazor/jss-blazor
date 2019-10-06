using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.FileProviders;

namespace JssBlazor.RenderingHost.Services
{
    public class DefaultPreRenderer : IPreRenderer
    {
        private readonly IHtmlHelper _htmlHelper;
        private readonly Func<string, IFileInfo> _fileInfoFactory;

        public DefaultPreRenderer(
            IHtmlHelper htmlHelper,
            Func<string, IFileInfo> fileInfoFactory)
        {
            _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
            _fileInfoFactory = fileInfoFactory ?? throw new ArgumentNullException(nameof(fileInfoFactory));
        }

        public async Task<string> RenderAppAsync<T>(
            string domElementSelector,
            ActionContext actionContext,
            ViewDataDictionary viewData,
            ITempDataDictionary tempData,
            bool pageEditing)
            where T : IComponent
        {
            var appHtml = await GetAppHtmlAsync<T>(actionContext, viewData, tempData);
            var indexHtml = await GetIndexHtmlAsync();

            var preRenderedApp = await InsertAppHtml(indexHtml, appHtml, domElementSelector, pageEditing);
            return preRenderedApp;
        }

        private async Task<string> GetAppHtmlAsync<T>(
            ActionContext actionContext,
            ViewDataDictionary viewData,
            ITempDataDictionary tempData)
            where T : IComponent
        {
            await using var viewWriter = new StringWriter();
            var viewContext = new ViewContext(actionContext, EmptyView.Default, viewData, tempData, viewWriter, new HtmlHelperOptions());

            var helper = (HtmlHelper)_htmlHelper;
            helper.Contextualize(viewContext);
            var appHtmlRenderer = await _htmlHelper.RenderComponentAsync<T>(RenderMode.ServerPrerendered);

            await using var appHtmlWriter = new StringWriter();
            appHtmlRenderer.WriteTo(appHtmlWriter, HtmlEncoder.Default);

            var appHtml = appHtmlWriter.ToString();
            return appHtml;
        }

        private async Task<string> GetIndexHtmlAsync()
        {
            var indexHtml = _fileInfoFactory("index.html");
            await using var stream = indexHtml.CreateReadStream();
            using var reader = new StreamReader(stream);
            var routeString = await reader.ReadToEndAsync();
            return routeString;
        }

        private static async Task<string> InsertAppHtml(
            string indexHtml,
            string appHtml,
            string domElementSelector,
            bool pageEditing)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(indexHtml);

            var appNode = htmlDocument.DocumentNode.SelectSingleNode($"//{domElementSelector}");
            appNode.RemoveAllChildren();
            appNode.InnerHtml = appHtml;

            if (pageEditing)
            {
                // Blazor breaks Experience Editor functionality, so remove the Blazor bundle in the Experience Editor.
                var blazorScript = htmlDocument.DocumentNode.SelectSingleNode($"//script[contains(@src, 'blazor.webassembly.js')]");
                blazorScript.Remove();
            }

            await using var stringWriter = new StringWriter();
            htmlDocument.Save(stringWriter);

            return stringWriter.ToString();
        }
    }
}
