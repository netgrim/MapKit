namespace MapKit.Core
{
    class WindowTarget : ContainerNode
    {
        internal const string ElementName = "window-target";

        public override NodeType GetNodeType()
        {
            return new NodeType(ElementName, typeof(WindowTarget));
        }

    }
}
