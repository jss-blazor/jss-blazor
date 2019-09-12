using Microsoft.AspNetCore.Components;

namespace JssBlazor.Core.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static string GetSitecoreRoutePath(this NavigationManager navigationManager)
        {
            var sitecoreRoutePath = navigationManager.ToBaseRelativePath(navigationManager.Uri);
            sitecoreRoutePath = sitecoreRoutePath.Split('#')[0];
            if (!sitecoreRoutePath.StartsWith("/"))
            {
                sitecoreRoutePath = $"/{sitecoreRoutePath}";
            }
            return sitecoreRoutePath;
        }
    }
}
