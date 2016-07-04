using System.Xml;
using System.Diagnostics;

namespace MapKit.Core
{
    [DebuggerDisplay("Svg: Label = {Label}, Path={NodePath}")]
    public abstract class SvgNode : ContainerNode
    {
        public const string IdField = "id";

        public string Id { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            base.WriteXmlAttributes(writer);
            if (!string.IsNullOrEmpty(Id))
                writer.WriteAttributeString(IdField, Id);
        }

        public override NodeType GetNodeType()
        {
            return null;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == IdField) Id = reader.Value;
            else return base.TryReadXmlAttribute(reader);
            return true;
        }

        public override void ReadXml(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
                if (!TryReadXmlAttribute(reader))
                    MapXmlReader.HandleUnexpectedAttribute(reader.LocalName);

            reader.MoveToElement();
            if (!reader.IsEmptyElement)
            {
                reader.Read();
                ReadXmlContent(reader);
            }

            reader.Read();
        }
    }
}
