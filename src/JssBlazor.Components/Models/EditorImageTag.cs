using System.Collections;
using System.Collections.Generic;

namespace JssBlazor.Components.Models
{
    public class EditorImageTag
    {
        public string ImgTag { get; set; }
        public IDictionary<string, string> Attrs { get; set; } = new Dictionary<string, string>();
    }
}
