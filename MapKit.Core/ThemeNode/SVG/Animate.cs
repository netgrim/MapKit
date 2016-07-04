using System;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;

namespace MapKit.Core
{
    [DebuggerDisplay("Animate: Label = {Label}, Path={NodePath}")]
    public class Animate : ContainerNode
    {
        public const string ElementName = "animate";
        public const string IdField = "id";
        public const string ClassField = "class";
        public const string OpacityField = "opacity";
        public const string AttributeNameField = "attributeName";
        public const string FromField = "from";
        public const string ToField = "to";
        public const string ValuesField = "values";
        public const string DurField = "dur";
        public const string BeginField = "begin";
        public const string RepeatCountField = "repeatCount";
        public const string FillField = "fill";
        

        static Animate()
        {
            //_nodeType = new StyleNodeType(Marker.Text, Marker.ElementName, typeof(Marker));
        }

        public Animate()
        {
        }

        public string Id { get; set; }

        public string Class { get; set; }

        public string Opacity { get; set; }

        public static string Text
        {
            get { return "Animate"; }
        }

        public static NodeType NodeType
        {
            get { return null; }
        }

        public string AttributeName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Values { get; set; }
        public string Duration { get; set; }
        public string Begin { get; set; }
        public string RepeatCount { get; set; }
        public string Fill { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            base.WriteXmlAttributes(writer);
            if (!string.IsNullOrEmpty(Id)) writer.WriteAttributeString(IdField, Id);
            if (!string.IsNullOrEmpty(Class)) writer.WriteAttributeString(ClassField, Class);
            if (!string.IsNullOrEmpty(Opacity)) writer.WriteAttributeString(OpacityField, Opacity);
            if (!string.IsNullOrEmpty(AttributeName)) writer.WriteAttributeString(AttributeNameField, AttributeName);
            if (!string.IsNullOrEmpty(From)) writer.WriteAttributeString(FromField, Opacity);
            if (!string.IsNullOrEmpty(To)) writer.WriteAttributeString(ToField, Opacity);
            if (!string.IsNullOrEmpty(Values)) writer.WriteAttributeString(ValuesField, Opacity);
            if (!string.IsNullOrEmpty(Duration)) writer.WriteAttributeString(DurField, Opacity);
            if (!string.IsNullOrEmpty(Begin)) writer.WriteAttributeString(BeginField, Opacity);
            if (!string.IsNullOrEmpty(RepeatCount)) writer.WriteAttributeString(RepeatCountField, Opacity);
            if (!string.IsNullOrEmpty(Fill)) writer.WriteAttributeString(FillField, Fill);
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == IdField) Id = reader.Value;
            else if (reader.LocalName == ClassField) Class = reader.Value;
            else if (reader.LocalName == OpacityField) Opacity = reader.Value;
            else if (reader.LocalName == AttributeNameField) AttributeName = reader.Value;
            else if (reader.LocalName == FromField) From = reader.Value;
            else if (reader.LocalName == ToField) To = reader.Value;
            else if (reader.LocalName == ValuesField) Values = reader.Value;
            else if (reader.LocalName == DurField) Duration = reader.Value;
            else if (reader.LocalName == BeginField) Begin = reader.Value;
            else if (reader.LocalName == RepeatCountField) RepeatCount = reader.Value;
            else if (reader.LocalName == FillField) Fill = reader.Value;
            else return base.TryReadXmlAttribute(reader);
            return true;
        }

        public override void ReadXml(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
                if (!TryReadXmlAttribute(reader))
                    MapXmlReader.HandleUnexpectedAttribute(reader.LocalName);

            reader.MoveToElement();
            if (!reader.IsEmptyElement && reader.Read() && reader.MoveToContent() != XmlNodeType.EndElement)
                throw new Exception("Animate element should be empty");
            reader.Read();
        }
    }
}
