using System.Diagnostics;
using System.Xml;

namespace MapKit.Core
{
    [DebuggerDisplay("LabelBox: Count = {Nodes.Count}")]
	public class LabelBox : ContainerNode
	{
        private const string ElementName = "labelBox";
        private static FeatureProcessorType _nodeType;

		public LabelBox()
		{
		}

        static LabelBox()
        {
            _nodeType = new FeatureProcessorType("Label Box", ElementName, typeof(LabelBox));
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
			var labelBox = (LabelBox)MemberwiseClone();

            labelBox.Nodes = new ThemeNodeCollection<ThemeNode>(labelBox);
			foreach (var node in Nodes)
				labelBox.Nodes.Add((ThemeNode)node.Clone());

			return labelBox;
		}

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        public static LabelBox FromXml(XmlReader reader, Map map)
        {
            var labelBox = new LabelBox();
            labelBox.Map = map;
            labelBox.ReadXml(reader);

            return labelBox;
        }
    }
}
