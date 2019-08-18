using JssBlazor.Shared.Models.LayoutService;
using Microsoft.AspNetCore.Components;

namespace JssBlazor.Components.Models
{
    public class JssComponentBase : ComponentBase
    {
        [Parameter]
        public ComponentDefinition Component { get; set; }

        protected string GetFieldValue(string fieldName)
        {
            var field = Component?.Fields?[fieldName];
            var value = field?.Value;
            var editable = field?.Editable;
            return string.IsNullOrWhiteSpace(editable) ? value?.Rendered : editable;
        }

        protected MarkupString GetMarkupFieldValue(string fieldName)
        {
            return (MarkupString)GetFieldValue(fieldName);
        }
    }
}
