using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JssBlazor.Core.Models.LayoutService.Fields
{
    public class ArrayFieldValue : FieldValue
    {
        public IEnumerable<JToken> FieldValue { get; set; }
    }
}
