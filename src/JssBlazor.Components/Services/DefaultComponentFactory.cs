using System;
using JssBlazor.Components.Models;
using JssBlazor.Core.Models.LayoutService;
using JssBlazor.Core.Services;
using Microsoft.AspNetCore.Components;

namespace JssBlazor.Components.Services
{
    public class DefaultComponentFactory : IComponentFactory
    {
        private readonly string _componentAssemblyFormat;
        private readonly Type _missingComponentType;
        private readonly Type _rawComponentType;

        public DefaultComponentFactory(ComponentFactoryOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _componentAssemblyFormat = options.ComponentAssemblyFormat;
            _missingComponentType = Type.GetType(options.MissingComponentType);
            _rawComponentType = Type.GetType(options.RawComponentType);
        }

        public RenderFragment RenderComponent(ComponentDefinition component) => builder =>
        {
            RenderComponent(component, null)(builder);
        };

        public RenderFragment RenderComponent(ComponentDefinition component, object componentKey) => builder =>
        {
            var componentType = GetComponentType(component);
            builder.OpenComponent(0, componentType);
            builder.SetKey(componentKey);
            builder.AddAttribute(1, "Component", component);
            builder.CloseComponent();
        };

        public Type GetComponentType(ComponentDefinition componentDefinition)
        {
            var componentType = GetComponentTypeName(componentDefinition);
            if (componentType == null)
            {
                return !string.IsNullOrWhiteSpace(componentDefinition.Name) ? _rawComponentType : _missingComponentType;
            }

            try
            {
                return Type.GetType(componentType) ?? _missingComponentType;
            }
            catch
            {
                return _missingComponentType;
            }
        }

        public object GetComponentKey(ComponentDefinition componentDefinition, string identifier)
        {
            if (componentDefinition.Uid == Guid.Empty)
            {
                return $"component-{identifier}";
            }
            return componentDefinition.Uid;
        }

        private string GetComponentTypeName(ComponentDefinition componentDefinition)
        {
            var componentName = componentDefinition.ComponentName;
            if (string.IsNullOrWhiteSpace(componentName)) return null;

            componentName = SanitizeComponentName(componentName);
            if (componentName.Contains(",")) return componentName;

            componentName = AddAssemblyToComponentName(componentName);
            return componentName;
        }

        private static string SanitizeComponentName(string componentName)
        {
            return componentName.Replace('-', '_');
        }

        private string AddAssemblyToComponentName(string componentName)
        {
            return string.Format(_componentAssemblyFormat, componentName);
        }
    }
}
