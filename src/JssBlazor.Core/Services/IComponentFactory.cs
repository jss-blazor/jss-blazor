using System;
using JssBlazor.Core.Models.LayoutService;
using Microsoft.AspNetCore.Components;

namespace JssBlazor.Core.Services
{
    public interface IComponentFactory
    {
        RenderFragment RenderComponent(ComponentDefinition componentDefinition);
        RenderFragment RenderComponent(ComponentDefinition componentDefinition, object key);
        Type GetComponentType(ComponentDefinition componentDefinition);
        object GetComponentKey(ComponentDefinition componentDefinition, string identifier);
    }
}
