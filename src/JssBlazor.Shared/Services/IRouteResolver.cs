using System.Threading.Tasks;
using JssBlazor.Shared.Models.Disconnected;

namespace JssBlazor.Shared.Services
{
    public interface IRouteResolver
    {
        Task<string> GetRouteJsonAsync(string item);
        Task<DisconnectedRoute> GetRouteAsync(string item);
    }
}
