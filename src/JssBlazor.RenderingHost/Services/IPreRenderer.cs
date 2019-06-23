using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace JssBlazor.RenderingHost.Services
{
    public interface IPreRenderer
    {
        Task<string> RenderAppAsync<T>(
            string domElementSelector,
            ActionContext actionContext,
            ViewDataDictionary viewData,
            ITempDataDictionary tempData)
            where T : IComponent;
    }
}
