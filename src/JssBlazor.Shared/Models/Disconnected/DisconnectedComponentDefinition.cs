using System.Collections.Generic;

namespace JssBlazor.Shared.Models.Disconnected
{
    public class DisconnectedComponentDefinition
    {
        public string ComponentName { get; set; }
        public IDictionary<string, string> Fields { get; set; }
        // The following property causes a "Uncaught (in promise) RangeError: Maximum call stack size exceeded"
        // exception in the browser.
        //public IDictionary<string, IList<DisconnectedComponentDefinition>> Placeholders { get; set; }
    }
}
