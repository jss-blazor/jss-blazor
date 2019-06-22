using JssBlazor.Shared.Models.LayoutService;
using Microsoft.AspNetCore.Components;

namespace JssBlazor.Client.Models
{
    public class JssComponentBase : ComponentBase
    {
        [Parameter]
        protected ComponentDefinition Component { get; set; }

        protected string GetFieldValue(string fieldName)
        {
            var field = Component?.Fields?[fieldName];
            var value = field?.Value;
            var editable = field?.Editable;
            return string.IsNullOrWhiteSpace(editable) ? value : editable;
        }

        protected MarkupString GetMarkupFieldValue(string fieldName)
        {
            return (MarkupString)GetFieldValue(fieldName);
        }
    }
}
