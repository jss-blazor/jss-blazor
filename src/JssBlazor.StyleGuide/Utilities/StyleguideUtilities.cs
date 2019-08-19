using JssBlazor.Core.Models.LayoutService;

namespace JssBlazor.StyleGuide.Utilities
{
    public static class StyleguideUtilities
    {
        public static string GetMarkupId(IRendering rendering)
        {
            return $"i{rendering.Uid.ToString("D")}";
        }
    }
}
