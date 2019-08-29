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

        public static string GetFieldValue(IRendering rendering, string fieldName, string defaultValue = null)
        {
            if (rendering == null) return defaultValue;
            string renderedValue = null;

            var fields = rendering.Fields;
            if (fields == null) return defaultValue;

            if(fields[fieldName] != null)
            {
                var value = fields[fieldName]?.Value;
                switch(value.GetType().Name)
                {
                    case "BooleanFieldValue":
                        renderedValue = fields[fieldName]?.Value.RawValue.ToString();
                        break;
                    default:
                        renderedValue = fields[fieldName]?.Value?.Rendered;
                        break;
                }
            }
            return string.IsNullOrWhiteSpace(renderedValue) ? defaultValue : renderedValue;
        }
    }
}
