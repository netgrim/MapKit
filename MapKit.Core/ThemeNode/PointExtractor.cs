using System.Diagnostics;
using System.Xml;

namespace MapKit.Core
{
    [DebuggerDisplay("PointExtractor")]
    //[DebuggerDisplay("PointExtractor: Label = {Label}, Path={NodePath}")]
    public class PointExtractor : ContainerNode
    {
        private const string ElementName = "pointExtractor";
        internal const string StartField = "start";
        internal const string EndField = "end";
        internal const string IncField = "increment";
        internal const string OffsetField = "offset";
        internal const string XColumnField = "xColumn";
        internal const string YColumnField = "yColumn";
        internal const string ZColumnField = "zColumn";
        internal const string MColumnField = "mColumn";
        internal const string AngleColumnField = "angleColumn";

        internal const string XColumnDefaultValue = "coord_x";
        internal const string YColumnDefaultValue = "coord_y";
        internal const string ZColumnDefaultValue = "";
        internal const string MColumnDefaultValue = "coord_m";
        internal const string AngleColumnDefaultValue = "";

        internal const string StartPropertyName = "Start";
        internal const string EndPropertyName = "End";
        internal const string OffsetPropertyName = "Offset";

        private static FeatureProcessorType _nodeType;
        
        private string _start;
        private string _end;
        private string _offset;
        private string _increment;
        private string _angleColumn;
        private string _mColumn;
        private string _zColumn;
        private string _yColumn;
        private string _xColumn;

        public PointExtractor()
        {
            XColumn = XColumnDefaultValue;
            YColumn = YColumnDefaultValue;
            ZColumn = ZColumnDefaultValue;
            MColumn = MColumnDefaultValue;
            AngleColumn = AngleColumnDefaultValue;
        }

        static PointExtractor()
        {
            _nodeType = new FeatureProcessorType("Point Extractor", ElementName, typeof(PointExtractor));
            _nodeType.NodeTypes.Add(Stroke.NodeType);
            _nodeType.NodeTypes.Add(Marker.NodeType);
            _nodeType.NodeTypes.Add(SolidFill.NodeType);
            _nodeType.NodeTypes.Add(Text.NodeType);
            _nodeType.NodeTypes.Add(PointExtractor.NodeType);
            _nodeType.NodeTypes.Add(VerticesEnumerator.NodeType);
            _nodeType.NodeTypes.Add(LinearCalibration.NodeType);
            _nodeType.NodeTypes.Add(LineOffset.NodeType);
            _nodeType.NodeTypes.Add(Case.NodeType);
        }

        public string Start
        {

            get { return _start; }
            set
            {
                _start = value;
                OnFieldChanged(StartPropertyName);
            }
        }

        public string End
        {

            get { return _end; }
            set
            {
                _end = value;
                OnFieldChanged(EndPropertyName);
            }
        }

        public string Increment
        {

            get { return _increment; }
            set
            {
                _increment = value;
                OnFieldChanged("Increment");
            }
        }

        public string Offset
        {

            get { return _offset; }
            set
            {
                _offset = value;
                OnFieldChanged(OffsetPropertyName);
            }
        }

        public string XColumn
        {

            get { return _xColumn; }
            set
            {
                _xColumn = value;
                OnFieldChanged("XColumn");
            }
        }

        public string YColumn
        {

            get { return _yColumn; }
            set
            {
                _yColumn = value;
                OnFieldChanged("YColumn");
            }
        }

        public string ZColumn
        {

            get { return _zColumn; }
            set
            {
                _zColumn = value;
                OnFieldChanged("ZColumn");
            }
        }

        public string MColumn
        {

            get { return _mColumn; }
            set
            {
                _mColumn = value;
                OnFieldChanged("MColumn");
            }
        }

        public string AngleColumn
        {

            get { return _angleColumn; }
            set
            {
                _angleColumn = value;
                OnFieldChanged("AngleColumn");
            }
        }

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }

        public override object Clone()
        {
            var clone = (PointExtractor)MemberwiseClone();

            clone.Nodes = new ThemeNodeCollection<ThemeNode>(clone);
            foreach (var node in Nodes)
                clone.Nodes.Add((ThemeNode)node.Clone());

            return clone;
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(Start))
                writer.WriteAttributeString(StartField, Start);
            if (!string.IsNullOrEmpty(End))
                writer.WriteAttributeString(EndField, End);
            if (!string.IsNullOrEmpty(Increment))
                writer.WriteAttributeString(IncField, Increment);
            if (!string.IsNullOrEmpty(Offset))
                writer.WriteAttributeString(OffsetField, Offset);
            if (XColumn != XColumnDefaultValue)
                writer.WriteAttributeString(XColumnField, XColumn);
            if (YColumn != YColumnDefaultValue)
                writer.WriteAttributeString(YColumnField, YColumn);
            if (ZColumn != ZColumnDefaultValue)
                writer.WriteAttributeString(ZColumnField, ZColumn);
            if (MColumn != MColumnDefaultValue)
                writer.WriteAttributeString(MColumnField, MColumn);
            if (AngleColumn != AngleColumnDefaultValue)
                writer.WriteAttributeString(AngleColumnField, AngleColumn);

            base.WriteXmlAttributes(writer);
        }
        
        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == PointExtractor.StartField) Start = reader.Value;
            else if (reader.LocalName == PointExtractor.EndField) End = reader.Value;
            else if (reader.LocalName == PointExtractor.IncField) Increment = reader.Value;
            else if (reader.LocalName == PointExtractor.OffsetField) Offset = reader.Value;
            else if (reader.LocalName == PointExtractor.XColumnField) XColumn = reader.Value;
            else if (reader.LocalName == PointExtractor.YColumnField) YColumn = reader.Value;
            else if (reader.LocalName == PointExtractor.ZColumnField) ZColumn = reader.Value;
            else if (reader.LocalName == PointExtractor.MColumnField) MColumn = reader.Value;
            else if (reader.LocalName == PointExtractor.AngleColumnField) AngleColumn = reader.Value;
            else return base.TryReadXmlAttribute(reader);
            return true;
        }
        public override void ReadXml(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
                if(!TryReadXmlAttribute(reader))
                    MapXmlReader.HandleUnexpectedAttribute(reader.LocalName);

            reader.MoveToElement();
            if (!reader.IsEmptyElement)
            {
                reader.Read();
                base.ReadXmlContent(reader);
            }
            reader.Read();
        }
    }
}
