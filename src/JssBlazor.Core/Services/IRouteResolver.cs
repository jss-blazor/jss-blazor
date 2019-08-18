using System.Threading.Tasks;
using JssBlazor.Core.Models.Disconnected;

namespace JssBlazor.Core.Services
{
    public interface IRouteResolver
    {
        Task<string> GetRouteJsonAsync(string item);
        Task<DisconnectedRoute> GetRouteAsync(string item);
    }
}
