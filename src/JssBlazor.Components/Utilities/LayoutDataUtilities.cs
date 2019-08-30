using System;
using System.Collections.Generic;
using System.Linq;
using JssBlazor.Core.Models.LayoutService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public static T GetFieldValue<T>(IRendering rendering, string fieldName)
        {
            var fields = rendering.Fields;
            if(fields[fieldName].Value != null && fields[fieldName].Value.RawValue != null)
            {
                var value = fields[fieldName].Value.RawValue.Value<T>();
                return value;
            }
            return default(T);
        }

        public static T GetFieldValue<T>(this Core.Models.LayoutService.Fields.Field field)
        {
            if (field.Value != null && field.Value.RawValue != null)
            {
                var value = field.Value.RawValue.Value<T>();
                return value;
            }
            return default(T);
        }

        public static T GetFieldValue<T>(this Core.Models.LayoutService.Fields.Field field, string key)
        {
            if (field.Value != null && field.Value.RawValue != null)
            {
                var value = field.Value.RawValue[key];
                if (value != null)
                    return value.Value<T>();
            }
            return default(T);
        }

        public static Core.Models.LayoutService.Fields.Field GetLinkedField<T>(this Core.Models.LayoutService.Fields.Field field, string key)
        {
            var value = field.Value.RawValue[key];
            return null;
        }
    }
}
