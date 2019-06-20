using System;
using JssBlazor.Shared.Models.LayoutService;
using Microsoft.AspNetCore.Http;

namespace JssBlazor.RenderingHost.Services
{
    public class StaticLayoutServiceResultProvider : ILayoutServiceResultProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StaticLayoutServiceResultProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public LayoutServiceResult Result
        {
            get => _httpContextAccessor.HttpContext.Items[typeof(StaticLayoutServiceResultProvider)] as LayoutServiceResult;
            set => _httpContextAccessor.HttpContext.Items[typeof(StaticLayoutServiceResultProvider)] = value;
        }
    }
}
