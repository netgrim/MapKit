using System;
using System.Collections.Generic;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;

namespace MapKit.Core
{
    [DebuggerDisplay("Text: Label = {Label}, Path={NodePath}")]
    public class Text : ContainerNode
	{
        internal const string ElementName = "text";

        private static StyleNodeType _nodeType;

        private LabelBox _labelBox;
        private TextStyle _style;
        
        static Text()
        {
            _nodeType = new StyleNodeType("Text", ElementName, typeof(Text));
            _nodeType.NodeTypes.Add(LabelBox.NodeType);
        }

		public Text()
		{
            LabelBox = new LabelBox();
            _style = new TextStyle();
            _style.PropertyChanged += _style_PropertyChanged;
		}
               
		public string Font
		{
			get { return _style.Font; }
            set { _style.Font = value; }
		}

        public string Angle
        {
            get { return _style.Angle; }
            set { _style.Angle = value; }
        }

		public string ScaleX
        {
            get { return _style.ScaleX; }
            set { _style.ScaleX = value; }
        }

		public string ScaleY
        {
            get { return _style.ScaleY; }
            set { _style.ScaleY = value; }
        }

        public bool KeepUpward
        {
            get { return _style.KeepUpward; }
            set { _style.KeepUpward = value; }
        }

        public string Opacity
        {
            get { return _style.Opacity; }
            set { _style.Opacity = value; }
        }

		public string Overlappable
        {
            get { return _style.Overlappable; }
            set { _style.Overlappable = value; }
        }

		public string AllowOverlap
        {
            get { return _style.AllowOverlap; }
            set { _style.AllowOverlap = value; }
        }

		public string Alignment
        {
            get { return _style.Alignment; }
            set { _style.Alignment = value; }
        }

		public String Color
        {
            get { return _style.Color; }
            set { _style.Color = value; }
        }

        public string Size
        {
            get { return _style.Size; }
            set { _style.Size = value; }
        }

        [Browsable(false)]
        public LabelBox LabelBox
        {
            get { return _labelBox; }
            private set
            {
                if (value != null && value.Map != null && value.Map != Map)
                    throw new InvalidOperationException("Already in another map");

                if (_labelBox != null)
                    _labelBox.Map = null;
                
                _labelBox = value;

                if (_labelBox != null)
                    _labelBox.Map = Map;
            }
        }

		public string Content
		{
			get { return _style.Content; }
            set { _style.Content = value; }
		}

        [Browsable(false)]
        internal TextFields Fields { get; set; }

        [Browsable(false)]
        internal TextFields InheritedFields { get; set; }

        public override Map Map
        {
            get { return base.Map; }
            internal set
            {
                base.Map = value;
                if (LabelBox != null)
                    LabelBox.Map = value;
                _style.Map = value;
            }
        }

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }

        internal override bool Cascade(Style style)
        {
            if (LabelBox != null)
                LabelBox.Cascade(style);

            return _style.Cascade(style);
        }

		public override object Clone()
		{
			return MemberwiseClone();
		}

        public override void ReadXml(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
                if (!_style.TryReadXmlAttribute(reader))
                    MapXmlReader.HandleUnexpectedAttribute(reader.LocalName);

            reader.MoveToElement();
            if (!reader.IsEmptyElement)
            {
                reader.Read();
                while (reader.MoveToContent() != XmlNodeType.EndElement)
                {
                    if (_style.ReadXmlContent(reader)) continue;
                    else if (reader.IsStartElement(LabelBox.NodeType.ElementName)) LabelBox = LabelBox.FromXml(reader, Map);
                    else MapXmlReader.HandleUnexpectedElement(reader.LocalName);

                }
            }
            reader.Read();
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            _style.WriteXmlAttributes(writer);

            base.WriteXmlAttributes(writer);
        }

        protected internal override void WriteXmlContent(XmlWriter writer)
        {
            _style.WriteXmlContent(writer);
            if(LabelBox != null && LabelBox.Nodes.Count > 0)
                LabelBox.WriteXml(writer);
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        public override IEnumerator<ThemeNode> GetEnumerator()
        {
            return LabelBox != null
                ? LabelBox.Nodes.GetEnumerator()
                : ((IEnumerable<ThemeNode>)new ThemeNode[0]).GetEnumerator();
        }

        void _style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnNotifyPropertyChanged(e.PropertyName);
        }
    }

	[Flags]
	public enum TextFields
	{
		None = 0,
		Font = 0x1,
		Angle = 0x2,
		ScaleX = 0x4,
		ScaleY = 0x8,
		//OffsetY = 0x10,
		//OffsetX = 0x20,
		Opacity = 0x40,
		Overlappable = 0x80,
		AllowOverlap = 0x100,
		Alignment = 0x200,
		Color = 0x400,
		Size = 0x800,
        Content = 0x1000,
        KeepUpward = 0x2000,
	}
}
