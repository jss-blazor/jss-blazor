using System;
using JssBlazor.Core.Models;
using JssBlazor.Core.Models.LayoutService;

namespace JssBlazor.Core.Services
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

            componentName = SanitizeComponentName(componentName);
            if (componentName.Contains(",")) return componentName;

            componentName = AddAssemblyToComponentName(componentName);
            return componentName;
        }

        private string SanitizeComponentName(string componentName)
        {
            return componentName.Replace('-', '_');
        }

        private string AddAssemblyToComponentName(string componentName)
        {
            return string.Format(_componentAssemblyFormat, componentName);
        }
    }
}
