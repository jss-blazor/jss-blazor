using System.Collections.Generic;

namespace JssBlazor.Shared.Models
{
    public class ComponentDefinition
    {
        public string ComponentName { get; set; }
        public IDictionary<string, string> Fields { get; set; }

        // The following property causes a "Uncaught (in promise) RangeError: Maximum call stack size exceeded"
        // exception in the browser.
        //public IDictionary<string, IList<ComponentDefinition>> Placeholders { get; set; }
    }
}
