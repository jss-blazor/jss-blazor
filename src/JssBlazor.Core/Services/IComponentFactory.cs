using System;
using JssBlazor.Core.Models.LayoutService;
using Microsoft.AspNetCore.Components;

namespace JssBlazor.Core.Services
{
    public interface IComponentFactory
    {
        RenderFragment RenderComponent(ComponentDefinition componentDefinition);
        Type GetComponentType(ComponentDefinition componentDefinition);
    }
}
