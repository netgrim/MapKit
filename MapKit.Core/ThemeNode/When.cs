using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;

namespace MapKit.Core
{
    [DebuggerDisplay("When: Label = {Label}, Path={NodePath}")]
    public class When : ExpressionNode
    {
        public const string ElementName = "when";
        private static FeatureProcessorType _nodeType;

        static When()
        {
            _nodeType = new FeatureProcessorType("When", ElementName, typeof(When));
            _nodeType.NodeTypes.Add(LinearCalibration.NodeType);
            _nodeType.NodeTypes.Add(PointExtractor.NodeType);
            _nodeType.NodeTypes.Add(LineOffset.NodeType);
            _nodeType.NodeTypes.Add(Stroke.NodeType);
            _nodeType.NodeTypes.Add(SolidFill.NodeType);
            _nodeType.NodeTypes.Add(Text.NodeType);
            _nodeType.NodeTypes.Add(Marker.NodeType);
            _nodeType.NodeTypes.Add(Case.NodeType);
            _nodeType.NodeTypes.Add(SolidFillStyle.NodeType);
            _nodeType.NodeTypes.Add(TextStyle.NodeType);
            _nodeType.NodeTypes.Add(LineStyle.NodeType);
            _nodeType.NodeTypes.Add(PointStyle.NodeType);
        }

        public override string GenerateNodeName()
        {
            return "When: " + Expression;
        }

        protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnNotifyPropertyChanged(e);
            if (e.PropertyName == ThemeNode.LabelProperty 
                || (e.PropertyName == ExpressionNode.ExpressionProperty
                    && string.IsNullOrWhiteSpace(Label)))
                OnNotifyPropertyChanged(ThemeNode.LabelOrDefaultProperty);
        }

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }

        public override object Clone()
        {
            var when = new When();
            when.Expression = Expression;

            foreach (var node in Nodes)
                when.Nodes.Add((ThemeNode)node.Clone());

            return when;
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == When.ExpressionField) Expression = reader.Value;
            else return base.TryReadXmlAttribute(reader);
            return true;
        }
    }
}
