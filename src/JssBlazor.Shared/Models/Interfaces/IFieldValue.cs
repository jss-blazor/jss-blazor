using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace JssBlazor.Shared.Models.Interfaces
{
    public interface IFieldValue
    {
        string Rendered { get; set; }
        JToken RawValue { get; set; }
    }
}
