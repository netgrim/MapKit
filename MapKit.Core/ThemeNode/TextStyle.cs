using System;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;

namespace MapKit.Core
{
    [DebuggerDisplay("TextStyle: Label = {Label}, Path={NodePath}")]
    public class TextStyle : Style
	{
        internal const string ElementName = "textStyle";
        internal const string AngleField = "angle";
        internal const string FontField = "font";
        internal const string AlignmentField = "alignment";
        internal const string ColorField = "color";
        internal const string OpacityField = "opacity";
        internal const string OverlapableField = "overlapable";
        internal const string AllowOverlapField = "allowOverlap";
        internal const string ScaleXField = "scaleX";
        internal const string ScaleYField = "scaleY";
        internal const string SizeField = "size";
        internal const string ContentField = "content";
        internal const string KeepUpwardField = "keepUpward";

        private static StyleNodeType _nodeType;

   		private string _content;
        private string _angle;
        private string _font;
        private string _scaleX;
        private string _scaleY;
        private bool _keepUpward;
        private string _opacity;
        private string _overlappable;
        private string _allowOverlap;
        private string _alignment;
        private string _color;
        private string _size;

		public TextStyle()
		{
            KeepUpward = true;
		}

        static TextStyle()
        {
            _nodeType = new StyleNodeType("TextStyle", ElementName, typeof(TextStyle));
            _nodeType.NodeTypes.Add(LabelBox.NodeType);
        }

		public string Font
		{
			get { return _font; }
			set
			{
				_font = value;
				Fields |= TextStyleFields.Font;
                OnFieldChanged("Font");
            }
		}

        public string Angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                Fields |= TextStyleFields.Angle;
                OnFieldChanged("Angle");
            }
        }

		public string ScaleX
		{
			get { return _scaleX; }
			set
			{
				_scaleX = value;
                Fields |= TextStyleFields.ScaleX;
                OnFieldChanged("ScaleX");
            }
		}

		public string ScaleY
		{
			get { return _scaleY; }
			set
			{
				_scaleY = value;
                Fields |= TextStyleFields.ScaleY;
                OnFieldChanged("ScaleY");
            }
		}

        public bool KeepUpward
        {
            get { return _keepUpward; }
            set
            {
                _keepUpward = value;
                Fields |= TextStyleFields.KeepUpward;
                OnFieldChanged("KeepUpward");
            }
        }

        public string Opacity
		{
			get { return _opacity; }
			set
			{
				_opacity = value;
                Fields |= TextStyleFields.Opacity;
                OnFieldChanged("Opacity");
            }
		}

		public string Overlappable
		{
			get { return _overlappable; }
			set
			{
				_overlappable = value;
                Fields |= TextStyleFields.Overlappable;
                OnFieldChanged("Overlappable");
            }
		}

		public string AllowOverlap
		{
			get { return _allowOverlap; }
			set
			{
				_allowOverlap = value;
                Fields |= TextStyleFields.AllowOverlap;
                OnFieldChanged("AllowOverlap");
            }
		}

		public string Alignment
		{
			get { return _alignment; }
			set
			{
				_alignment = value;
                Fields |= TextStyleFields.Alignment;
                OnFieldChanged("Alignment");
            }
		}

		public String Color
		{
			get { return _color; }
			set
			{
				_color = value;
                Fields |= TextStyleFields.Color;
                OnFieldChanged("Color");
            }
		}

        public string Size 
		{
			get { return _size; }
			set
			{
				_size = value;
                Fields |= TextStyleFields.Size;
                OnFieldChanged("Size");
            }
		}

		public string Content
		{
			get { return _content; }
			set
			{
				_content = value;
                Fields |= TextStyleFields.Content;
                OnFieldChanged("Content");
            }
		}

        [Browsable(false)]
        internal TextStyleFields Fields { get; set; }

        [Browsable(false)]
        internal TextStyleFields InheritedFields { get; set; }

        public override Map Map
        {
            get { return base.Map; }
            internal set { base.Map = value; }
        }

        public static new NodeType NodeType
        {
            get { return _nodeType; }
        }

        internal override bool Cascade(Style style)
        {
            //var textStyle = style as TextStyle;
            //if (textStyle == null || textStyle.Id != Id) return false;

            //Cascade(textStyle);
            return true;
        }

        private void Cascade(TextStyle text)
		{
            var missing = (text.Fields | text.InheritedFields) & ~Fields;
            if (missing == TextStyleFields.None) return;

            foreach (TextStyleFields fields in Enum.GetValues(typeof(TextStyleFields)))
				if (missing.HasFlag(fields))
					switch (fields)
					{
                        case TextStyleFields.Font: 
                            _font = text.Font;
                            OnNotifyPropertyChanged("Font");
                            break;
                        case TextStyleFields.Angle: 
                            _angle = text.Angle;
                            OnNotifyPropertyChanged("Angle");
                            break;
                        case TextStyleFields.ScaleX: 
                            _scaleX = text.ScaleX;
                            OnNotifyPropertyChanged("ScaleX");
                            break;
                        case TextStyleFields.ScaleY: 
                            _scaleY = text.ScaleY;
                            OnNotifyPropertyChanged("ScaleY");
                            break;
                        case TextStyleFields.Opacity: 
                            _opacity = text.Opacity;
                            OnNotifyPropertyChanged("Opacity");
                            break;
                        case TextStyleFields.Overlappable: 
                            _overlappable = text.Overlappable;
                            OnNotifyPropertyChanged("Overlappable");
                            break;
                        case TextStyleFields.AllowOverlap: 
                            _allowOverlap = text.AllowOverlap;
                            OnNotifyPropertyChanged("AllowOverlap");
                            break;
                        case TextStyleFields.Alignment: 
                            _alignment = text.Alignment;
                            OnNotifyPropertyChanged("Alignment");
                            break;
                        case TextStyleFields.Color: 
                            _color = text.Color;
                            OnNotifyPropertyChanged("Color");
                            break;
                        case TextStyleFields.Size: 
                            _size = text.Size;
                            OnNotifyPropertyChanged("Size");
                            break;
                        case TextStyleFields.Content: 
                            _content = text.Content;
                            OnNotifyPropertyChanged("Content");
                            break;
                        case TextStyleFields.KeepUpward: 
                            _keepUpward = text.KeepUpward;
                            OnNotifyPropertyChanged("KeepUpward");
                            break;
					}
            InheritedFields = missing;
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            if (Fields.HasFlag(TextStyleFields.Angle)) writer.WriteAttributeString(AngleField, Angle);
            if (Fields.HasFlag(TextStyleFields.Font)) writer.WriteAttributeString(FontField, Font);
            if (Fields.HasFlag(TextStyleFields.Color)) writer.WriteAttributeString(ColorField, Color);
            if (Fields.HasFlag(TextStyleFields.Alignment)) writer.WriteAttributeString(AlignmentField, Alignment);
            if (Fields.HasFlag(TextStyleFields.Opacity)) writer.WriteAttributeString(OpacityField, Opacity);
            if (Fields.HasFlag(TextStyleFields.Overlappable)) writer.WriteAttributeString(OverlapableField, Overlappable);
            if (Fields.HasFlag(TextStyleFields.AllowOverlap)) writer.WriteAttributeString(AllowOverlapField, AllowOverlap);
            if (Fields.HasFlag(TextStyleFields.ScaleX)) writer.WriteAttributeString(ScaleXField, ScaleX);
            if (Fields.HasFlag(TextStyleFields.ScaleY)) writer.WriteAttributeString(ScaleYField, ScaleY);
            if (Fields.HasFlag(TextStyleFields.Size)) writer.WriteAttributeString(SizeField, Size);

            if (!KeepUpward)
            {
                writer.WriteStartAttribute(KeepUpwardField);
                writer.WriteValue(KeepUpward);
                writer.WriteEndAttribute();
            }

            base.WriteXmlAttributes(writer);
        }

        protected internal override void WriteXmlContent(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(Content) && (Fields & TextStyleFields.Content) != TextStyleFields.None)
                writer.WriteElementString(ContentField, Content);
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.NodeType != XmlNodeType.Attribute)
                return false;

            if (reader.LocalName == TextStyle.FontField) Font = reader.Value;
            else if (reader.LocalName == TextStyle.SizeField) Size = reader.Value;
            else if (reader.LocalName == TextStyle.ColorField) Color = reader.Value;
            else if (reader.LocalName == TextStyle.AlignmentField) Alignment = reader.Value;
            else if (reader.LocalName == TextStyle.AllowOverlapField) AllowOverlap = reader.Value;
            else if (reader.LocalName == TextStyle.OverlapableField) Overlappable = reader.Value;
            else if (reader.LocalName == TextStyle.OpacityField) Opacity = reader.Value;
            else if (reader.LocalName == TextStyle.ScaleXField) ScaleX = reader.Value;
            else if (reader.LocalName == TextStyle.ScaleYField) ScaleY = reader.Value;
            else if (reader.LocalName == TextStyle.AngleField) Angle = reader.Value;
            else if (reader.LocalName == TextStyle.KeepUpwardField) KeepUpward = bool.Parse(reader.Value);
            else return base.TryReadXmlAttribute(reader);
            return true;
        }

        internal new bool ReadXmlContent(XmlReader _reader)
        {
            if (!_reader.IsStartElement(TextStyle.ContentField)) return false;
            Content = _reader.ReadElementContentAsString();
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
                reader.Read();
                while (reader.MoveToContent() != XmlNodeType.EndElement)
                    if (ReadXmlContent(reader)) continue;
                    else MapXmlReader.HandleUnexpectedElement(reader.LocalName);
            }
            reader.Read();
        }
    }

	[Flags]
    public enum TextStyleFields
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
