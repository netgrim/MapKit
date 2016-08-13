using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace MapKit.Core
{
    [DebuggerDisplay("Svg: Label = {Label}, Path={NodePath}")]
    public abstract class SvgNode : ContainerNode
    {
        public const string IdField = "id";

        public string Id { get; set; }

        public List<Tuple<string, string>> Attributes { get; private set; }

        public SvgNode()
        {
            Attributes = new List<Tuple<string, string>>();
        }

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
            else if (!base.TryReadXmlAttribute(reader))
                Attributes.Add(Tuple.Create(reader.LocalName, reader.Value));

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
