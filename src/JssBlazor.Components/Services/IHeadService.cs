using System.Threading.Tasks;

namespace JssBlazor.Components.Services
{
    public interface IHeadService
    {
        string Title { get; }

        Task SetTitleAsync(string title);
    }
}
