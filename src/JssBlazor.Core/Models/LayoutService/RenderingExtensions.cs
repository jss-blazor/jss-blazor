namespace JssBlazor.Core.Models.LayoutService
{
    public static class RenderingExtensions
    {
        public static string GetFieldValue(
            this IRendering rendering,
            string fieldName,
            string defaultValue = null)
        {
            if (rendering == null) return defaultValue;
            string renderedValue = null;

            var fields = rendering.Fields;
            if (fields == null) return defaultValue;

            if (fields[fieldName] != null)
            {
                var value = fields[fieldName]?.Value;
                switch (value.GetType().Name)
                {
                    case "BooleanFieldValue":
                    case "DateFieldValue":
                        renderedValue = fields[fieldName]?.Value.RawValue.ToString();
                        break;
                    default:
                        renderedValue = fields[fieldName]?.Value?.Rendered;
                        break;
                }
            }
            return string.IsNullOrWhiteSpace(renderedValue) ? defaultValue : renderedValue;
        }

        public static T GetFieldValue<T>(this IRendering rendering, string fieldName)
        {
            var field = rendering?.Fields?[fieldName];
            if (field == null) return default;
            return field.GetFieldValue<T>();
        }
    }
}
