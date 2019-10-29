using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JssBlazor.RenderingHost.Services
{
    public class DefaultPreRenderer : IPreRenderer
    {
        private const string AppStateId = "__JSS_STATE__";

        private readonly IHtmlHelper _htmlHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILayoutServiceResultProvider _layoutServiceResultProvider;

        public DefaultPreRenderer(
            IHtmlHelper htmlHelper,
            IWebHostEnvironment webHostEnvironment,
            ILayoutServiceResultProvider layoutServiceResultProvider)
        {
            _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _layoutServiceResultProvider = layoutServiceResultProvider ?? throw new ArgumentNullException(nameof(layoutServiceResultProvider));
        }

        public async Task<string> RenderAppAsync<T>(
            string domElementSelector,
            ActionContext actionContext,
            ViewDataDictionary viewData,
            ITempDataDictionary tempData,
            bool pageEditing)
            where T : IComponent
        {
            return await RenderAppAsync(typeof(T), domElementSelector, actionContext, viewData, tempData, pageEditing);
        }

        public async Task<string> RenderAppAsync(
            Type appType,
            string domElementSelector,
            ActionContext actionContext,
            ViewDataDictionary viewData,
            ITempDataDictionary tempData,
            bool pageEditing)
        {
            var appHtml = await GetAppHtmlAsync(appType, actionContext, viewData, tempData);
            var indexHtml = await GetIndexHtmlAsync(appType);

            var preRenderedApp = await InsertAppHtml(indexHtml, appHtml, domElementSelector, pageEditing);
            if (pageEditing) return preRenderedApp;

            var statefulPreRenderedApp = await InsertInitialState(preRenderedApp);
            return statefulPreRenderedApp;
        }

        private async Task<string> GetAppHtmlAsync(
            Type appType,
            ActionContext actionContext,
            ViewDataDictionary viewData,
            ITempDataDictionary tempData)
        {
            await using var viewWriter = new StringWriter();
            var viewContext = new ViewContext(actionContext, EmptyView.Default, viewData, tempData, viewWriter, new HtmlHelperOptions());

            var helper = (HtmlHelper)_htmlHelper;
            helper.Contextualize(viewContext);
            var appHtmlRenderer = await RenderComponentAsync(appType);

            await using var appHtmlWriter = new StringWriter();
            appHtmlRenderer.WriteTo(appHtmlWriter, HtmlEncoder.Default);

            var appHtml = appHtmlWriter.ToString();
            return appHtml;
        }

        private async Task<IHtmlContent> RenderComponentAsync(Type appType)
        {
            var renderComponentAsyncMethod = typeof(HtmlHelperComponentExtensions).GetMethod(
                nameof(HtmlHelperComponentExtensions.RenderComponentAsync),
                new[] { typeof(IHtmlHelper), typeof(RenderMode) });
            var renderComponentAsyncOfAppTypeMethod = renderComponentAsyncMethod.MakeGenericMethod(new[] { appType });

            var appHtmlRenderer = await (Task<IHtmlContent>)renderComponentAsyncOfAppTypeMethod.Invoke(
                null,
                new object[] { _htmlHelper, RenderMode.ServerPrerendered });
            return appHtmlRenderer;
        }

        private async Task<string> GetIndexHtmlAsync(Type appType)
        {
            var appName = appType.Assembly.GetName().Name;
            var indexHtmlPath = Path.Combine(appName, "dist", "index.html");
            var indexHtml = _webHostEnvironment.ContentRootFileProvider.GetFileInfo(indexHtmlPath);

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

        private async Task<string> InsertInitialState(
            string indexHtml)
        {
            var htmlDocument = new HtmlDocument
            {
                OptionCheckSyntax = false
            };
            htmlDocument.LoadHtml(indexHtml);

            var stateNode = HtmlNode.CreateNode("<script></script>");
            stateNode.Attributes.Add("type", "application/json");
            stateNode.Attributes.Add("id", AppStateId);

            var fetcherScript = HtmlNode.CreateNode("<script></script>");
            fetcherScript.InnerHtml = $"window.jssBlazor = window.jssBlazor || {{}}; window.jssBlazor.getInitialState = () => {{ return document.getElementById(\"{AppStateId}\").innerHTML; }}";

            var body = htmlDocument.DocumentNode.SelectSingleNode("//body");
            body.AppendChild(stateNode);
            body.AppendChild(fetcherScript);

            await using var stringWriter = new StringWriter();
            htmlDocument.Save(stringWriter);

            // HTML Agility Pack modifies the state object when setting it on the stateNode and there
            // doesn't appear to be a way to stop that behavior. Using string replace to bypass it for now.
            var layoutServiceResult = _layoutServiceResultProvider.Result;
            var initialState = JsonConvert.SerializeObject(layoutServiceResult, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });

            var initialStateDocument = stringWriter.ToString();
            initialStateDocument = initialStateDocument.Replace(
                $"<script type=\"application/json\" id=\"{AppStateId}\"></script>",
                $"<script type=\"application/json\" id=\"{AppStateId}\">{initialState}</script>");
            return initialStateDocument;
        }
    }
}
