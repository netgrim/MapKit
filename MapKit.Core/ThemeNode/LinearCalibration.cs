using System.Diagnostics;
using System.Xml;

namespace MapKit.Core
{
    [DebuggerDisplay("LinearCalibration: Label = {Label}, Path={NodePath}")]
    public class LinearCalibration : ContainerNode
	{
        private const string ElementName = "linearCalibration";
        internal const string StartMeasureField = "startMeasure";
        internal const string EndMeasureField = "endMeasure";

        private static FeatureProcessorType _nodeType;

        private string _startMeasure;
        private string _endMeasure;

		public LinearCalibration()
		{
		}
        
        static LinearCalibration()
        {
            _nodeType = new FeatureProcessorType("Linear Calibration", LinearCalibration.ElementName, typeof(LinearCalibration));
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

        public string StartMeasure
        {

            get { return _startMeasure; }
            set
            {
                _startMeasure = value;
                OnFieldChanged("StartMeasure");
            }
        }

        public string EndMeasure
        {

            get { return _endMeasure; }
            set
            {
                _endMeasure = value;
                OnFieldChanged("EndMeasure");
            }
        }

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(StartMeasure))
            {
                writer.WriteStartAttribute(StartMeasureField);
                writer.WriteValue(StartMeasure);
                writer.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(EndMeasure))
            {
                writer.WriteStartAttribute(EndMeasureField);
                writer.WriteValue(EndMeasure);
                writer.WriteEndAttribute();
            }

            base.WriteXmlAttributes(writer);
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == LinearCalibration.StartMeasureField) StartMeasure = reader.Value;
            else if (reader.LocalName == LinearCalibration.EndMeasureField) EndMeasure = reader.Value;
            else return base.TryReadXmlAttribute(reader);
            return true;
        }    }
}
