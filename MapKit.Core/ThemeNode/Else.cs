using System.Diagnostics;

namespace MapKit.Core
{
    [DebuggerDisplay("Else: Label = {Label}, Path={NodePath}")]
    public class Else : ContainerNode
	{
        internal const string ElementName = "else";
        
        private static FeatureProcessorType _nodeType;

        static Else()
        {
            _nodeType = new FeatureProcessorType("Else", ElementName, typeof(Else));
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

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }

		public override object Clone()
		{
			var newElse = new Else();
			newElse.Label = Label;

			foreach (var node in Nodes)
				newElse.Nodes.Add((ThemeNode)node.Clone());

			return newElse;
		}

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

    }
}
