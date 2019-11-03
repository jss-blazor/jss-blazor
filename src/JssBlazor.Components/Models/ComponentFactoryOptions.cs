namespace JssBlazor.Components.Models
{
    public class ComponentFactoryOptions
    {
        public string ComponentAssemblyFormat { get; set; }
        public string MissingComponentType { get; set; }
        public string RawComponentType { get; set; }

        public ComponentFactoryOptions()
        {
            MissingComponentType = typeof(MissingComponent).AssemblyQualifiedName;
            RawComponentType = typeof(RawComponent).AssemblyQualifiedName;
        }
    }
}
