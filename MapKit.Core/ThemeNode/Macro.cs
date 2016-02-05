using System.Xml;

namespace MapKit.Core
{
    public class Macro : ContainerNode
    {
        private const string ElementName = "macro";
        
        private static NodeType _nodeType;

        static Macro()
        {
            _nodeType = new StyleNodeType("macro", Macro.ElementName, typeof(Macro));
        }

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }

        public override string GenerateNodeName()
        {
            return "Macro: " + Name;
        }

        public override NodeType GetNodeType()
        {
            return _nodeType;
        }

        public string InputFeatureVariable { get; set; }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == NameField) Name = reader.Value;
            else return base.TryReadXmlAttribute(reader);
            return true;
        }

    }
}
