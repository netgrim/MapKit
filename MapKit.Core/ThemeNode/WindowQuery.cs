namespace MapKit.Core
{
    class WindowQuery : ContainerNode
    {
        public const string ElementName = "window-query";

        public override NodeType GetNodeType()
        {
            return new NodeType(ElementName, typeof(WindowQuery));
        }
    }
}
