using System.Threading.Tasks;

namespace JssBlazor.Shared.Services
{
    public interface IRouteResolver
    {
        Task<string> GetRouteJsonAsync(string item);
    }
}
