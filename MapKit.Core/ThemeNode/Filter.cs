using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;
using System.Globalization;

namespace MapKit.Core
{
    [DebuggerDisplay("Label = {Label}")]
    public class Filter : ContainerNode
    {
        private const string ElementName = "if";
        private const string ConditionField = "condition";
       
        private string _condition;

        static Filter()
        {
            NodeType = new NodeType("Conditional", ElementName, typeof(Filter));
        }

        public static NodeType NodeType { get; private set; }


        public override string GenerateNodeName()
        {
            return "If: " + Condition;
        }

        public string Condition
        {

            get { return _condition; }
            set
            {
                _condition = value;
                OnFieldChanged("Condition");
            }
        }

        public override object Clone()
        {
            var filter = (Filter)MemberwiseClone();

            filter.Nodes = new ThemeNodeCollection<ThemeNode>(filter);
            foreach (var node in Nodes)
                filter.Nodes.Add((ThemeNode)node.Clone());

			return filter;
		}

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            writer.WriteAttributeString(ConditionField, Condition);
            base.WriteXmlAttributes(writer);
     	}

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == Filter.ConditionField) Condition = reader.Value;
            else return base.TryReadXmlAttribute(reader);
            return true;
        }
    }
}
