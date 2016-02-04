using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace MapKit.Core
{
    class LineOffset : ThemeNode
    {
        private const string ElementName = "offset";

        private static FeatureProcessorType _nodeType;

        static LineOffset()
        {
            _nodeType = new FeatureProcessorType("Line Offset", LineOffset.ElementName, typeof(LineOffset));
            _nodeType.NodeTypes.Add(LinearCalibration.NodeType);
            _nodeType.NodeTypes.Add(PointExtractor.NodeType);
            _nodeType.NodeTypes.Add(LineOffset.NodeType);
            _nodeType.NodeTypes.Add(Stroke.NodeType);
            _nodeType.NodeTypes.Add(SolidFill.NodeType);
            _nodeType.NodeTypes.Add(Text.NodeType);
            _nodeType.NodeTypes.Add(Marker.NodeType);
            _nodeType.NodeTypes.Add(Case.NodeType);
        }

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }
    }
}
