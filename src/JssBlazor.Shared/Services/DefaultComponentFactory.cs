using System;
using JssBlazor.Shared.Models;
using JssBlazor.Shared.Models.LayoutService;

namespace JssBlazor.Shared.Services
{
    public class DefaultComponentFactory : IComponentFactory
    {
        private readonly Type _missingComponentType;

        public DefaultComponentFactory(ComponentFactoryOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _missingComponentType = Type.GetType(options.MissingComponentType);
        }

        public Type GetComponentType(ComponentDefinition componentDefinition)
        {
            var componentType = componentDefinition.ComponentName;

            try
            {
                return Type.GetType(componentType) ?? _missingComponentType;
            }
            catch
            {
                return _missingComponentType;
            }
        }
    }
}
