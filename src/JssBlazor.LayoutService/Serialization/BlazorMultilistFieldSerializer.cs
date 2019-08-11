using System.Collections.Generic;
using Newtonsoft.Json;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.LayoutService.Serialization;
using Sitecore.LayoutService.Serialization.FieldSerializers;
using Sitecore.LayoutService.Serialization.ItemSerializers;

namespace JssBlazor.LayoutService.Serialization
{
    public class BlazorMultilistFieldSerializer : MultilistFieldSerializer
    {
        private const string ItemsPropertyName = "value";

        public BlazorMultilistFieldSerializer(
            IItemSerializer itemSerializer,
            IFieldRenderer fieldRenderer)
            : base(itemSerializer, fieldRenderer)
        {
        }

        protected override void WriteEmptyValue(MultilistField field, JsonTextWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(ItemsPropertyName);
            base.WriteEmptyValue(field, writer);
            writer.WriteEndObject();
        }

        protected override void WriteValueObject(IEnumerable<Item> items, MultilistField field, JsonTextWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(ItemsPropertyName);
            base.WriteValueObject(items, field, writer);
            writer.WriteEndObject();
        }
    }
}
