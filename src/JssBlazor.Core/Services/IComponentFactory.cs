using System;
using JssBlazor.Core.Models.LayoutService;

namespace JssBlazor.Core.Services
{
    public interface IComponentFactory
    {
        Type GetComponentType(ComponentDefinition componentDefinition);
    }
}
