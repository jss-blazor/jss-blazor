using System;
using JssBlazor.Shared.Models.LayoutService;

namespace JssBlazor.Shared.Services
{
    public interface IComponentFactory
    {
        Type GetComponentType(ComponentDefinition componentDefinition);
    }
}
