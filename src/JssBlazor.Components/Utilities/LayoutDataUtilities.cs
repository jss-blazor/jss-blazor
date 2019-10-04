using System.Collections.Generic;
using System.Linq;
using JssBlazor.Core.Models.LayoutService;

namespace JssBlazor.Components.Utilities
{
    public static class LayoutDataUtilities
    {
        public static IEnumerable<IRendering> GetChildPlaceholder(
            IRendering rendering,
            string placeholderName)
        {
            if (rendering == null ||
                string.IsNullOrWhiteSpace(placeholderName) ||
                rendering.Placeholders == null ||
                !rendering.Placeholders.ContainsKey(placeholderName))
            {
                return Enumerable.Empty<IRendering>();
            }

            return rendering.Placeholders[placeholderName];
        }
    }
}
