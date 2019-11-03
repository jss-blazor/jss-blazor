using System.Collections.Generic;

namespace JssBlazor.Core.Models.LayoutService.Fields
{
    public class MultilistFieldValue : FieldValue
    {
        public IEnumerable<MultilistItem> Items { get; set; }
    }
}
