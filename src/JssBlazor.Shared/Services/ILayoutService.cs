using System.Threading.Tasks;
using JssBlazor.Shared.Models;

namespace JssBlazor.Shared.Services
{
    public interface ILayoutService
    {
        Task<LayoutServiceResponse> GetRouteAsync(string path);
    }
}
