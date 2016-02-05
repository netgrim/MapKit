using System;
using System.Xml;
using System.ComponentModel;
using System.Diagnostics;

namespace MapKit.Core
{
    [DebuggerDisplay("Stroke: Label = {Label}, Path={NodePath}")]
    public class Stroke : ThemeNode, INotifyPropertyChanged
	{
        private const string ElementName = "stroke";
        private const string ReferenceField = "ref"; 
        
        private static StyleNodeType _nodeType;

        private LineStyle _style;

        static Stroke()
        {
            _nodeType = new StyleNodeType(Stroke.Text, Stroke.ElementName, typeof(Stroke));
        }

		public Stroke()
		{
            _style = new LineStyle();
            _style.PropertyChanged += new PropertyChangedEventHandler(style_PropertyChanged);
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
                    _style.Cascade(parentStyle);
                    SuperColor.Parent = parentStyle.SuperColor;

                    base.OnNodePathChanged(e);
                    return;
                }
                else
                    parent = parent.Parent;
            }

            SuperColor.Parent = null;
            _style.Cascade(null);

            base.OnNodePathChanged(e);
        }
        
		public string DashStyle
		{
			get { return _style.DashStyle; }
            set
            {
                _style.DashStyle = value;
                OnNotifyPropertyChanged("DashStyle");
            }
		}

		public string PatternFile
		{
            get { return _style.PatternFile; }
            set
            {
                _style.PatternFile = value;
                OnNotifyPropertyChanged("PatternFile");
            }
        }

		public string Width
		{
            get { return _style.Width; }
            set
            {
                _style.Width = value;
                OnNotifyPropertyChanged("Width");
            }
        }
        
        public InheritableColor SuperColor { get; set; }

        public string Color
		{
            get { return _style.Color; }
            set { _style.Color = value; }
		}

		public string DashOffset
		{
            get { return _style.DashOffset; }
            set { _style.DashOffset = value; }
		}

        public string Opacity
        {
            get { return _style.Opacity; }
            set { _style.Opacity = value; }
        }

        public override Map Map
        {
            get { return base.Map; }
            internal set
            {
                base.Map = value;
                _style.Map = value;
            }
        }

		public string Miterlimit
        {
            get { return _style.Miterlimit; }
            set { _style.Miterlimit = value; }
        }

		public string DashArray
        {
            get { return _style.DashArray; }
            set { _style.DashArray = value; }
        }

		public string StartCap
        {
            get { return _style.StartCap; }
            set { _style.StartCap = value; }
        }

		public string EndCap
        {
            get { return _style.EndCap; }
            set { _style.EndCap = value; }
        }

		public string Join
        {
            get { return _style.Join; }
            set { _style.Join = value; }
        }

        public static string Text
        {
            get { return "Stroke"; }
        }

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }
        
		internal override bool Cascade(Style style)
		{
			var lineStyle = style as LineStyle;
            if (lineStyle == null /*|| string.Compare(lineStyle.Id, Id, true) != 0*/) return false;
             
            _style.Cascade(lineStyle);
            return true;
		}

		public override object Clone()
		{
			return MemberwiseClone();
		}

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            base.WriteXmlAttributes(writer);
            _style.WriteXmlAttributes(writer);
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        public override void ReadXml(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
                if (!_style.TryReadXmlAttribute(reader))
                    MapXmlReader.HandleUnexpectedAttribute(reader.LocalName);


            reader.MoveToElement();
            if (!reader.IsEmptyElement && reader.Read() && reader.MoveToContent() != XmlNodeType.EndElement)
                throw new Exception("Stroke element should be empty");

            reader.Read();
        }

        void style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnNotifyPropertyChanged(e.PropertyName);
        }
    }

	[Flags]
	public enum StrokeFields
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
