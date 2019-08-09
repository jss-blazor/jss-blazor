using System;
using JssBlazor.Shared.Models;
using JssBlazor.Shared.Models.LayoutService;

namespace JssBlazor.Shared.Services
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

        private string GetComponentTypeName(ComponentDefinition componentDefinition)
        {
            var componentName = componentDefinition.ComponentName;
            if (string.IsNullOrWhiteSpace(componentName)) return null;

            if (componentName.Contains(",")) return componentName;

            var fullyQualifiedComponentName = string.Format(_componentAssemblyFormat, componentName);
            return fullyQualifiedComponentName;
        }
    }
}
