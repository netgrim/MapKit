using System;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;

namespace MapKit.Core
{
    [DebuggerDisplay("Marker: Label = {Label}, Path={NodePath}")]
    public class Marker : ThemeNode
    {
        private const string ElementName = "marker";

        public const string ColorPropertyName = "Color";
        public const string AnglePropertyName = "Angle";
        public const string FilePropertyName = "File";
        public const string ScaleYPropertyName = "ScaleY";
        public const string ScaleXPropertyName = "ScaleX";
        public const string OpacityPropertyName = "Opacity";
        public const string OverlappablePropertyName = "Overlappable";
        public const string AllowOverlapPropertyName = "AllowOverlap";
        public const string AlignmentPropertyName = "Alignment";

        private static StyleNodeType _nodeType;

        static Marker()
        {
            _nodeType = new StyleNodeType(Marker.Text, Marker.ElementName, typeof(Marker));
        }

        public Marker()
        {
            Style = new PointStyle();
            Style.PropertyChanged += new PropertyChangedEventHandler(Style_PropertyChanged);
        }

        public PointStyle Style { get; private set; }

        public override Map Map
        {
            get { return base.Map; }
            internal set
            {
                Style.Map = value;
                base.Map = value;
            }
        }

        public string Angle
        {
            get { return Style.Angle; }
            set { Style.Angle = value; }
        }

        public string ScaleX
        {
            get { return Style.ScaleX; }
            set { Style.ScaleX = value; }
        }

        public string ScaleY
        {
            get { return Style.ScaleY; }
            set { Style.ScaleY = value; }
        }

        public string Opacity
        {
            get { return Style.Opacity; }
            set { Style.Opacity = value; }
        }

        public string Overlappable
        {
            get { return Style.Overlappable; }
            set { Style.Overlappable = value; }
        }

        public string AllowOverlap
        {
            get { return Style.AllowOverlap; }
            set { Style.AllowOverlap = value; }
        }

        public string File
        {
            get { return Style.File; }
            set { Style.File = value; }
        }

        public string Color
        {
            get { return Style.Color; }
            set { Style.Color = value; }
        }

        public string Alignment
        {
            get { return Style.Alignment; }
            set { Style.Alignment = value; }
        }

        public static string Text
        {
            get { return "Marker"; }
        }

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        internal override bool Cascade(Style style)
        {
            return Style.Cascade(style);
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            base.WriteXmlAttributes(writer);
            Style.WriteXmlAttributes(writer);
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        public override void ReadXml(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
                if (!Style.TryReadXmlAttribute(reader))
                    MapXmlReader.HandleUnexpectedAttribute(reader.LocalName);

            reader.MoveToElement();
            if (!reader.IsEmptyElement && reader.Read() && reader.MoveToContent() != XmlNodeType.EndElement)
                throw new Exception("Marker element should be empty");
            reader.Read();

        }

        void Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnNotifyPropertyChanged(e);
        }

    }
}
