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
            ITempDataDictionary tempData)
            where T : IComponent
        {
            var appHtml = await GetAppHtmlAsync<T>(actionContext, viewData, tempData);
            var indexHtml = await GetIndexHtmlAsync();

            var preRenderedApp = await ReplaceAppNodeWithPreRenderedAppAsync(indexHtml, appHtml, domElementSelector);
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
            var appHtmlRenderer = await _htmlHelper.RenderComponentAsync<T>();

            await using var appHtmlWriter = new StringWriter();
            appHtmlRenderer.WriteTo(appHtmlWriter, HtmlEncoder.Default);

            var appHtml = appHtmlWriter.ToString();
            return appHtml;
        }

        private async Task<string> GetIndexHtmlAsync()
        {
            var routeYml = _fileInfoFactory("index.html");
            await using var stream = routeYml.CreateReadStream();
            using var reader = new StreamReader(stream);
            var routeString = await reader.ReadToEndAsync();
            return routeString;
        }

        private static async Task<string> ReplaceAppNodeWithPreRenderedAppAsync(
            string indexHtml,
            string appHtml,
            string domElementSelector)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(indexHtml);

            var tempNode = htmlDocument.CreateElement("temp");
            tempNode.InnerHtml = appHtml;

            var appNode = htmlDocument.DocumentNode.SelectSingleNode($"//{domElementSelector}");
            var currentNode = appNode;
            foreach (var childNode in tempNode.ChildNodes)
            {
                appNode.ParentNode.InsertAfter(childNode, currentNode);
                currentNode = childNode;
            }
            appNode.Remove();

            await using var stringWriter = new StringWriter();
            htmlDocument.Save(stringWriter);

            return stringWriter.ToString();
        }
    }
}