using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JssBlazor.Shared.Models.LayoutService.Fields
{
    public class ArrayFieldValue : FieldValue
    {
        public IReadOnlyCollection<JToken> FieldValue { get; set; }
    }
}
