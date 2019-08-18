using System.Collections.Generic;

namespace JssBlazor.Core.Models.Disconnected
{
    public class DisconnectedRoute
    {
        public string Id { get; set; }
        public IDictionary<string, string> Fields { get; set; }
        public IDictionary<string, IEnumerable<DisconnectedComponentDefinition>> Placeholders { get; set; }
    }
}
