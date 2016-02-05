using System.Xml;
using System.Globalization;
using System.ComponentModel;

namespace MapKit.Core
{
    class Window : ContainerNode
    {
        public const string ElementName = "window";
        internal const string AngleField = "angle";
        internal const string BgColorField = "bgColor";
        internal const string CenterXField = "centerX";
        internal const string CenterYField = "centerY";
        internal const string ZoomField = "zoom";
        internal const string MaxRecursionField = "maxRecursion";
        internal const string ClipField = "clip";
        internal const int DefaultMaxRecursion = 0;
        
        private bool _clip = true;
        private string _centerX;
        private  string _centerY;
        private  string _zoom;
        private  string _angle;
        private int _maxRecursion;

        public Window()
        {
            MaxRecursion = DefaultMaxRecursion;
        }

        public string BackgroundColor { get; set; }

        public string CenterX
        {
            get { return _centerX; }
            set
            {
                _centerX = value;
                OnFieldChanged(CenterXField);
            }
        }

        public string CenterY
        {
            get { return _centerY; }
            set
            {
                _centerY = value;
                OnFieldChanged(CenterYField);
            }
        }

        public string Zoom 
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                OnFieldChanged(ZoomField);
            }
        }

        public string Angle 
        {
            get { return _angle; }
            set
            {
                _angle = value;
                OnFieldChanged(AngleField);
            }
        }

        public int MaxRecursion 
        {
            get { return _maxRecursion; }
            set
            {
                _maxRecursion = value;
                OnNotifyPropertyChanged(MaxRecursionField);
            }
        }

        [DefaultValue(true)]
        public bool Clip
        {
            get { return _clip; }
            set
            {
                _clip = value;
                //Fields |= SolidFillStyleFields.Opacity;
                OnFieldChanged(ClipField);
            }
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override NodeType GetNodeType()
        {
            return new NodeType(ElementName, typeof(Window));
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            if(!string.IsNullOrEmpty(BackgroundColor))
                writer.WriteAttributeString(BgColorField, BackgroundColor);
            if(!string.IsNullOrEmpty(CenterX))
                writer.WriteAttributeString(CenterXField, CenterX);
            if (!string.IsNullOrEmpty(CenterY))
                writer.WriteAttributeString(CenterYField, CenterY);
            if (!string.IsNullOrEmpty(Zoom))
                writer.WriteAttributeString(ZoomField, Zoom);
            if (!string.IsNullOrEmpty(Angle))
                writer.WriteAttributeString(AngleField, Angle);
            if (MaxRecursion > DefaultMaxRecursion)
                writer.WriteAttributeString(MaxRecursionField, MaxRecursion.ToString());

            base.WriteXmlAttributes(writer);
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            switch (reader.LocalName)
            {
                case Window.BgColorField: BackgroundColor = reader.Value; break;
                case Window.CenterXField: CenterX = reader.Value; break;
                case Window.CenterYField: CenterY = reader.Value; break;
                case Window.ZoomField: Zoom = reader.Value; break;
                case Window.AngleField: Angle = reader.Value; break;
                case Window.MaxRecursionField: MaxRecursion = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo); break;
                case Window.ClipField: Clip = bool.Parse(reader.Value); break;
                default: return base.TryReadXmlAttribute(reader);
            }
            return true;
        }

        public override void ReadXml(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
                if (!TryReadXmlAttribute(reader))
                    MapXmlReader.HandleUnexpectedAttribute(reader.LocalName);

            if (!reader.IsEmptyElement)
            {
                reader.Read();
                base.ReadXmlContent(reader);
            }
            reader.Read();

            //if (!reader.IsEmptyElement && reader.Read() && reader.MoveToContent() != XmlNodeType.EndElement)
            //    throw new Exception("LineStyle element should be empty");

            reader.Read();
        }

        protected override bool TryReadXmlElement(XmlReader reader)
        {
            ThemeNode node = null;
            if (reader.LocalName == WindowTarget.ElementName)
                node = new WindowTarget();
            else if (reader.LocalName == WindowQuery.ElementName)
                node = new WindowQuery();
            else
                return base.TryReadXmlElement(reader);

            Nodes.Add(node);
            node.ReadXml(reader);
            
            return true;
        }


    }
}
