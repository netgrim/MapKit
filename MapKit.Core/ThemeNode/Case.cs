using System.Diagnostics;
using System.Xml;
using System.ComponentModel;

namespace MapKit.Core
{
    [DebuggerDisplay("Case: Label = {Label}, Path={NodePath}")]
	public class Case : ExpressionNode 
	{
        private const string ElementName = "case";
        private static FeatureProcessorType _nodeType;

        static Case()
        {
            _nodeType = new FeatureProcessorType("Switch Case", Case.ElementName, typeof(Case));
            _nodeType.NodeTypes.Add(When.NodeType);
            _nodeType.NodeTypes.Add(Else.NodeType);
        }

        public Case()
        {
            Label = null;
        }
                
        public static NodeType NodeType
        {
            get { return _nodeType; }
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        public override string GenerateNodeName()
        {
            return "Case: " + Expression;
        }

        protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnNotifyPropertyChanged(e);
            if (e.PropertyName == ThemeNode.LabelProperty
                || (e.PropertyName == ExpressionNode.ExpressionProperty
                    && string.IsNullOrWhiteSpace(Label)))
                OnNotifyPropertyChanged(ThemeNode.LabelOrDefaultProperty);
        }

                
		public override object Clone()
		{
			var caseNode = (Case)MemberwiseClone();

            caseNode.Nodes = new ThemeNodeCollection<ThemeNode>(caseNode);
			foreach (var node in Nodes)
				caseNode.Nodes.Add((ThemeNode)node.Clone());

			return caseNode;
		}

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(ElementName);
            WriteXmlAttributes(writer);
            WriteXmlContent(writer);
            writer.WriteEndElement();
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == Case.ExpressionField) Expression = reader.Value;
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
                var isElsePresent = false;
                reader.Read();
                while (reader.MoveToContent() != XmlNodeType.EndElement)
                    if (reader.IsStartElement(When.ElementName) && !isElsePresent)
                    {
                        var when = new When();
                        Nodes.Add(when);
                        when.ReadXml(reader);
                    }
                    else if (reader.IsStartElement(Else.ElementName))
                    {
                        var elseNode = new Else();
                        Nodes.Add(elseNode);
                        elseNode.ReadXml(reader);
                        isElsePresent = true;
                    }
                    else MapXmlReader.HandleUnexpectedElement(reader.LocalName);
            }
            reader.Read();
        }

    }
}
