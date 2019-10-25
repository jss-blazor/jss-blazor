namespace JssBlazor.Core.Models.LayoutService.Fields
{
    public class LinkField : Field
    {
        public new LinkFieldValue Value { get; set; }
        public string EditableFirstPart { get; set; }
        public string EditableLastPart { get; set; }
    }
}
