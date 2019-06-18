using System;
using System.Threading.Tasks;
using JssBlazor.Shared.Models;
using JssBlazor.Shared.Services;
using Microsoft.AspNetCore.Http;

namespace JssBlazor.RenderingHost.Services
{
    public class LocalLayoutService : ILayoutService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LayoutServiceResponse Route
        {
            get => _httpContextAccessor.HttpContext.Items[typeof(LocalLayoutService)] as LayoutServiceResponse;
            set => _httpContextAccessor.HttpContext.Items[typeof(LocalLayoutService)] = value;
        }

        public LocalLayoutService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Task<LayoutServiceResponse> GetRouteAsync(string path)
        {
            return Task.FromResult(Route);
        }
    }
}
