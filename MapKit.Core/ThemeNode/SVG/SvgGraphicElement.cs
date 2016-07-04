using System;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;

namespace MapKit.Core
{
    [DebuggerDisplay("SvgGraphicElement: Label = {Label}, Path={NodePath}")]
    public class SvgGraphicElement : SvgNode
    {
        public const string ClassField = "class";
        public const string StyleField = "style";
        public const string TransformField = "transform";

        public string Class { get; set; }

        public string Transform { get; set; }

        public string Style { get; private set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            base.WriteXmlAttributes(writer);
            if (!string.IsNullOrEmpty(Class))
                writer.WriteAttributeString(ClassField, Class);
            if (!string.IsNullOrEmpty(Style))
                writer.WriteAttributeString(IdField, Style);
            if (!string.IsNullOrEmpty(Transform))
                writer.WriteAttributeString(TransformField, Transform);
        }

        public override NodeType GetNodeType()
        {
            return null;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == ClassField) Class = reader.Value;
            else if (reader.LocalName == StyleField) Style = reader.Value;
            else if (reader.LocalName == TransformField) Transform = reader.Value;
            else return base.TryReadXmlAttribute(reader);
            return true;
        }
    }
}
