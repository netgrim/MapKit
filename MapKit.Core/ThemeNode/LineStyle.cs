using System;
using System.Drawing;
using System.Xml;
using System.ComponentModel;
using System.Diagnostics;

namespace MapKit.Core
{
    [DebuggerDisplay("LineStyle: Label = {Label}, Path={NodePath}")]
    public class LineStyle : Style, INotifyPropertyChanged
	{
        private static string ElementName = "lineStyle";
        internal static string WidthField = "width";
        internal static string ColorField = "color";
        internal static string DashStyleField = "dashStyle";
        internal static string DashArrayField = "dashArray";
        internal static string DashOffsetField = "dashOffset";
        internal static string OpacityField = "opacity";
        internal static string StartCapField = "startCap";
        internal static string EndCapField = "endCap";
        internal static string JoinField = "join";
        internal static string MiterLimitField = "miterlimit";
        internal static string PatternFileField = "patternFile";
        internal static string PatternFilePropertyName = "PatternFile";
        
        //default property values
        private const string DefPatternFile = "";
        private const string DefWidth = "1";
        private const string DefDashArray = "";
        private const string DefColor = "Black";
        private const string DefDashOffset = "";
        private const string DefDashStyle = "";
        private const string DefOpacity = "100";
        private const string DefMiterLimit = "";
        private const string DefStartCap = "";
        private const string DefEndCap = "";
        private const string DefJoin = "";        

        private static StyleNodeType _nodeType;

        private string _join = DefJoin;
        private string _dashStyle = DefDashStyle;
        private string _patternFile = DefPatternFile;
        private string _width = DefWidth;
        private string _color = DefColor;
        private string _dashOffset = DefDashOffset;
        private string _opacity = DefOpacity;
        private string _miterlimit = DefMiterLimit;
        private string _dashArray = DefDashArray;
        private string _startCap = DefStartCap;
        private string _endCap = DefEndCap;
        private LineStyle _lineStyle;


        static LineStyle()
        {
            _nodeType = new StyleNodeType(LineStyle.Text, LineStyle.ElementName, typeof(LineStyle));
        }

		public LineStyle()
		{
            SuperColor = new InheritableColor();
		}

        protected override void OnNodePathChanged(EventArgs e)
        {
            var parent = Parent;
            while (parent != null)
            {
                var parentStyle = parent as LineStyle;
                if (parentStyle != null)
                {
                    Cascade(parentStyle);
                    SuperColor.Parent = parentStyle.SuperColor;

                    base.OnNodePathChanged(e);
                    return;
                }
                else
                    parent = parent.Parent;
            }

            SuperColor.Parent = null;
            Cascade(null);

            base.OnNodePathChanged(e);
        }

        public override ContainerNode Parent
        {
            get { return base.Parent; }
            internal set
            {
                base.Parent = value;
                var parent = Parent;
                while (parent != null)
                {
                    var parentStyle = parent as LineStyle;
                    if (parentStyle != null)
                    {
                        Cascade(parentStyle);
                        SuperColor.Parent = parentStyle.SuperColor;
                        return;
                    }
                    else
                        parent = parent.Parent;
                }
                
                Cascade(null);
                SuperColor.Parent = null;
            }
        }

		public string DashStyle
		{
			get { return _dashStyle; }
			set
			{
				_dashStyle = value;
                Fields |= LineStyleFields.DashStyle;
                OnFieldChanged("DashStyle");
            }
		}

        private string InheritedDashStyle
        {
            set
            {
                if (_dashStyle == value) return;
                _dashStyle = value;
                OnNotifyPropertyChanged("DashStyle");
            }
        }

		public string PatternFile
		{
			get { return _patternFile; }
			set
			{
				_patternFile = value;
                Fields |= LineStyleFields.PatternFile;
                OnFieldChanged(PatternFilePropertyName);
            }
		}

        private string InheritedPatternFile
        {
            set
            {
                if (_patternFile == value) return;
                _patternFile = value;
                OnNotifyPropertyChanged("PatternFile");
            }
        }

		public string Width
		{
			get { return _width; }
            set
            {
                _width = value;

                if (string.IsNullOrEmpty(value))
                    Fields &= ~LineStyleFields.Width;
                else
                    Fields |= LineStyleFields.Width;

                OnFieldChanged("Width");
            }
		}

        private string InheritedWidth
        {
            set
            {
                if (_width == value) return;
                _width = value;
                OnNotifyPropertyChanged("Width");
            }
        }

        public string Color
		{
			get { return _color; }
			set
			{
				_color = value;

                ColorInherited = string.IsNullOrEmpty(value) || value == (_lineStyle != null ? _lineStyle.Color : KnownColor.Black.ToString());

                OnFieldChanged("Color");
			}
		}

        public InheritableColor SuperColor { get; set; }


        private string InheritedColor
        {
            set
            {
                if (_color == value) return;
                _color = value;
                OnNotifyPropertyChanged("Color");
            }
        }

        public bool ColorInherited
        {
            get { return (Fields & LineStyleFields.Color) != LineStyleFields.Color; }
            set
            {
                if (value && _lineStyle != null)
                {
                    Fields &= ~LineStyleFields.Color;
                    InheritedColor = _lineStyle.Color;
                }
                else
                    Fields |= LineStyleFields.Color;
            }
        }

		public string DashOffset
		{
			get { return _dashOffset; }
			set
			{
				_dashOffset = value;
                Fields |= LineStyleFields.DashOffset;
                OnFieldChanged("DashOffset");
            }
		}

        private string InheritedDashOffset
        {
            set
            {
                if (_dashOffset == value) return;
                _dashOffset = value;
                OnNotifyPropertyChanged("DashOffset");
            }
        }

        public string Opacity
		{
			get { return _opacity; }
			set
			{
				_opacity = value;
                Fields |= LineStyleFields.Opacity;
                OnFieldChanged("Opacity");
            }
		}

        private string InheritedOpacity
        {
            set
            {
                if (_opacity == value) return;
                _opacity = value;
                OnNotifyPropertyChanged("Opacity");
            }
        }

		public string Miterlimit
		{
			get { return _miterlimit; }
			set
			{
				_miterlimit = value;
                Fields |= LineStyleFields.Miterlimit;
                OnFieldChanged("Miterlimit");
            }
		}

        private string InheritedMiterlimit
        {
            set
            {
                if (_miterlimit == value) return;
                _miterlimit = value;
                OnNotifyPropertyChanged("Miterlimit");
            }
        }

		public string DashArray
		{
			get { return _dashArray; }
			set
			{
				_dashArray = value;
                Fields |= LineStyleFields.DashArray;
                OnFieldChanged("DashArray");
            }
		}

        private string InheritedDashArray
        {
            set
            {
                if (_dashArray == value) return;
                _dashArray = value;
                OnNotifyPropertyChanged("DashArray");
            }
        }

		public string StartCap
		{
			get { return _startCap; }
			set
			{
				_startCap = value;
                Fields |= LineStyleFields.StartCap;
                OnFieldChanged("StartCap");
            }
		}

        private string InheritedStartCap
        {
            set
            {
                if (_startCap == value) return;
                _startCap = value;
                OnNotifyPropertyChanged("StartCap");
            }
        }

		public string EndCap
		{
			get { return _endCap; }
			set
			{
				_endCap = value;
                Fields |= LineStyleFields.EndCap;
                OnFieldChanged("EndCap");
            }
		}

        private string InheritedEndCap
        {
            set
            {
                if (_endCap == value) return;
                _endCap = value;
                OnNotifyPropertyChanged("EndCap");
            }
        }

		public string Join
		{
			get { return _join; }
			set
			{
				_join = value;
                Fields |= LineStyleFields.Join;
                OnFieldChanged("Join");
            }
		}

        private string InheritedJoin
        {
            set
            {
                if (_join == value) return;
                _join = value;
                OnNotifyPropertyChanged("Join");
            }
        }

        [Browsable(false)]
        internal LineStyleFields Fields { get; set; }

        //[Browsable(false)]
        //internal LineStyleFields InheritedFields { get; set; }

        public static string Text
        {
            get { return "LineStyle"; }
        }

        public static new NodeType NodeType
        {
            get { return _nodeType; }
        }

        internal override bool Cascade(Style style)
        {
            var lineStyle = style as LineStyle;
            if (lineStyle == null /*|| lineStyle.Id != Id*/) return false;
            
            Cascade(lineStyle);
            return true;
        }

        public void Cascade(LineStyle lineStyle)
		{
            if (_lineStyle != lineStyle)
            {
                if (_lineStyle != null)
                    _lineStyle.PropertyChanged += new PropertyChangedEventHandler(ParentLineStyle_PropertyChanged);
                _lineStyle = lineStyle;
                if (_lineStyle != null)
                    _lineStyle.PropertyChanged += new PropertyChangedEventHandler(ParentLineStyle_PropertyChanged);
            }

            var missing = /*lineStyle != null ? (lineStyle.Fields | lineStyle.InheritedFields) & ~Fields : */~Fields;
			if (missing == LineStyleFields.None) return;

            foreach (LineStyleFields fields in Enum.GetValues(typeof(LineStyleFields)))
				if (missing.HasFlag(fields))
					switch (fields)
					{
                        case LineStyleFields.PatternFile:
                            InheritedPatternFile = lineStyle != null ? lineStyle.PatternFile : DefPatternFile;
                            break;
                        case LineStyleFields.Width: 
                            InheritedWidth = lineStyle != null ? lineStyle.Width : DefWidth; 
                            break;
                        case LineStyleFields.DashArray:
                            InheritedDashArray = lineStyle != null ? lineStyle.DashArray : DefDashArray;
                            break;
                        case LineStyleFields.Color:
                            InheritedColor = lineStyle != null ? lineStyle.Color : DefColor;
                            break;
                        case LineStyleFields.DashOffset:
                            InheritedDashOffset = lineStyle != null ? lineStyle.DashOffset : DefDashOffset;
                            break;
                        case LineStyleFields.DashStyle:
                            InheritedDashStyle = lineStyle != null ? lineStyle.DashStyle : DefDashStyle;
                            break;
                        case LineStyleFields.Opacity:
                            InheritedOpacity = lineStyle != null ? lineStyle.Opacity : DefOpacity;
                            break;
                        case LineStyleFields.Miterlimit:
                            InheritedMiterlimit = lineStyle != null ? lineStyle.Miterlimit : DefMiterLimit;
                            break;
                        case LineStyleFields.StartCap:
                            InheritedStartCap = lineStyle != null ? lineStyle.StartCap : DefStartCap;
                            break;
                        case LineStyleFields.EndCap:
                            InheritedEndCap = lineStyle != null ? lineStyle.EndCap : DefEndCap;
                            break;
                        case LineStyleFields.Join:
                            InheritedJoin = lineStyle != null ? lineStyle.Join : DefJoin;
                            break;
					}
            //InheritedFields = missing;
        }

        void ParentLineStyle_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cascade(_lineStyle);
        }

		public override object Clone()
		{
			return MemberwiseClone();
		}

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            base.WriteXmlAttributes(writer);

            if (!string.IsNullOrEmpty(Color) && Fields.HasFlag(LineStyleFields.Color))
                writer.WriteAttributeString(ColorField, Color);
            if (!string.IsNullOrEmpty(Opacity) && Fields.HasFlag(LineStyleFields.Opacity))
                writer.WriteAttributeString(OpacityField, Opacity);
            if (!string.IsNullOrEmpty(Width) && Fields.HasFlag(LineStyleFields.Width))
                writer.WriteAttributeString(WidthField, Width);
            if (!string.IsNullOrEmpty(DashStyle) && Fields.HasFlag(LineStyleFields.DashStyle))
                writer.WriteAttributeString(DashStyleField, DashStyle);
            if (!string.IsNullOrEmpty(DashArray) && Fields.HasFlag(LineStyleFields.DashArray))
                writer.WriteAttributeString(DashArrayField, DashArray);
            if (!string.IsNullOrEmpty(DashOffset) && Fields.HasFlag(LineStyleFields.DashOffset))
                writer.WriteAttributeString(DashOffsetField, DashOffset);
            if (!string.IsNullOrEmpty(StartCap) && Fields.HasFlag(LineStyleFields.StartCap))
                writer.WriteAttributeString(StartCapField, StartCap);
            if (!string.IsNullOrEmpty(EndCap) && Fields.HasFlag(LineStyleFields.EndCap))
                writer.WriteAttributeString(EndCapField, EndCap);
            if (!string.IsNullOrEmpty(Join) && Fields.HasFlag(LineStyleFields.Join))
                writer.WriteAttributeString(JoinField, Join);
            if (!string.IsNullOrEmpty(Miterlimit) && Fields.HasFlag(LineStyleFields.Miterlimit))
                writer.WriteAttributeString(MiterLimitField, Miterlimit);
            if (!string.IsNullOrEmpty(PatternFile) && Fields.HasFlag(LineStyleFields.PatternFile))
                writer.WriteAttributeString(PatternFileField, PatternFile);
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.NodeType != XmlNodeType.Attribute)
                return false;

            if (reader.LocalName == LineStyle.PatternFileField) PatternFile = reader.Value;
            else if (reader.LocalName == LineStyle.WidthField) Width = reader.Value;
            else if (reader.LocalName == LineStyle.ColorField) SuperColor.Expression = Color = reader.Value;
            else if (reader.LocalName == LineStyle.DashStyleField) DashStyle = reader.Value;
            else if (reader.LocalName == LineStyle.DashArrayField) DashArray = reader.Value;
            else if (reader.LocalName == LineStyle.DashOffsetField) DashOffset = reader.Value;
            else if (reader.LocalName == LineStyle.OpacityField) Opacity = reader.Value;
            else if (reader.LocalName == LineStyle.StartCapField) StartCap = reader.Value;
            else if (reader.LocalName == LineStyle.EndCapField) EndCap = reader.Value;
            else if (reader.LocalName == LineStyle.JoinField) Join = reader.Value;
            else if (reader.LocalName == LineStyle.MiterLimitField) Miterlimit = reader.Value;
            else if (reader.LocalName == LineStyle.VisibleField) Visible = Convert.ToBoolean(reader.Value);
            else return base.TryReadXmlAttribute(reader);
            return true;
        }

        public override string GenerateNodeName()
        {
            return "LineStyle: " + string.Join(", ", string.Join("\t", _color, _opacity, _width, _dashStyle).Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries));
        }
    }

	[Flags]
    public enum LineStyleFields
	{
		None = 0,
		PatternFile = 0x1,
		Width = 0x2,
		//Scale = 0x4,
		DashArray = 0x8,
		Color = 0x10,
		//OffsetX = 0x20,
		//OffsetY = 0x40,
		DashOffset = 0x80,
		Opacity = 0x100,
		Miterlimit = 0x200,
		StartCap = 0x800,
		EndCap = 0x1000,
		Join = 0x2000,
		DashStyle = 0x4000,
	}

    //public enum LineCap
    //{
    //    butt,
    //    round,
    //    square,
    //}

}
