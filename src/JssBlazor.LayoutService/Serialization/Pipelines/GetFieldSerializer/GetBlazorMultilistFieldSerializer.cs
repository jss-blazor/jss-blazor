using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Common;
using Sitecore.LayoutService.Presentation;
using Sitecore.LayoutService.Serialization;
using Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer;

namespace JssBlazor.LayoutService.Serialization.Pipelines.GetFieldSerializer
{
    public class GetBlazorMultilistFieldSerializer : BaseGetFieldSerializer
    {
        public List<string> AllowedConfigurations { get; set; } = new List<string>();

        public GetBlazorMultilistFieldSerializer(
            IFieldRenderer fieldRenderer)
            : base(fieldRenderer)
        {
        }

        public override void Process(GetFieldSerializerPipelineArgs args)
        {
            if (!IsConfigurationAllowed()) return;
            base.Process(args);
        }

        protected virtual bool IsConfigurationAllowed()
        {
            var jsonRenderingContext = Switcher<JsonRenderingContext, JsonRenderingContext>.CurrentValue;
            return !AllowedConfigurations.Any() || AllowedConfigurations.Contains(jsonRenderingContext.RenderingConfiguration.Name);
        }

        protected override void SetResult(GetFieldSerializerPipelineArgs args)
        {
            if (args is null) throw new ArgumentNullException(nameof(args));
            args.Result = new BlazorMultilistFieldSerializer(args.ItemSerializer, FieldRenderer);
        }
    }
}
