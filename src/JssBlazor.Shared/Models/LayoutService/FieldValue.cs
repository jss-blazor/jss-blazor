using System;
using System.Collections.Generic;
using System.Text;
using JssBlazor.Shared.Models.Interfaces;
using Newtonsoft.Json.Linq;

namespace JssBlazor.Shared.Models.LayoutService
{
    public class FieldValue : IFieldValue
    {
        public string Rendered { get ; set; }
        public JToken RawValue { get; set; }
    }
}
