using System.Threading.Tasks;
using JssBlazor.Core.Models.LayoutService;

namespace JssBlazor.Core.Services
{
    public interface IInitialStateLoader
    {
        Task<LayoutServiceResult> GetInitialStateAsync();
    }
}
