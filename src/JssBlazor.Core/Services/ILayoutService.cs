using System.Threading.Tasks;
using JssBlazor.Core.Models.LayoutService;

namespace JssBlazor.Core.Services
{
    public interface ILayoutService
    {
        LayoutServiceResult Current { get; set; }
        Task<LayoutServiceResult> GetRouteAsync(string path);
    }
}
