using System.Threading.Tasks;
using JssBlazor.Shared.Models.LayoutService;

namespace JssBlazor.Shared.Services
{
    public interface ILayoutService
    {
        Task<LayoutServiceResult> GetRouteAsync(string path);
    }
}
