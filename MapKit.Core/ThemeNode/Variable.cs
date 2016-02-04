using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace MapKit.Core
{
    public class Variable : ThemeNode
    {
        public const string ElementName = "var";

        public const string NameProperty = "Name";
        public const string NameAttribute = "name";
        public const string ValueProperty = "Value";
        public const string ValueAttribute = "value";
        public const string TypeAttribute = "type";

        private static NodeType _nodeType;

        private string _name;
        private string _value;
        private TypeCode _typeCode;

        static Variable()
        {
            _nodeType = new StyleNodeType(Text, Variable.ElementName, typeof(Variable));
        }

        public Variable()
        {
            Type = TypeCode.String;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnFieldChanged(NameAttribute);
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnFieldChanged(ValueAttribute);
            }
        }

        public TypeCode Type
        {
            get { return _typeCode; }
            set
            {
                _typeCode = value;
                OnFieldChanged(ValueAttribute);
            }
        }

        public static string Text
        {
            get { return typeof(Variable).Name; }
        }

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }

        public override string GenerateNodeName()
        {
            return "Variable: " + Name;
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }
        
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(ElementName);
            writer.WriteValue(_value);
            writer.WriteEndElement();
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == NameAttribute) Name = reader.Value;
            else if (reader.LocalName == ValueAttribute) Value = reader.Value;
            else if (reader.LocalName == TypeAttribute) Type = (TypeCode)Enum.Parse(typeof(TypeCode), reader.Value, true);
            else return base.TryReadXmlAttribute(reader);
            return true;
        }
    }
}
