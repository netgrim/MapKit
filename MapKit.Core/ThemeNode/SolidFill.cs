using System;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;

namespace MapKit.Core
{
    [DebuggerDisplay("SolidFill: Label = {Label}, Path={NodePath}")]
    public class SolidFill : ThemeNode
	{
        internal const string ElementName = "solidFill";
        internal const string ColorPropertyName = "Color";
        internal const string OpacityPropertyName = "Opacity";
        internal readonly static string Text = "Solid Fill";

        private static StyleNodeType _nodeType;

        static SolidFill()
        {
            _nodeType = new StyleNodeType(SolidFill.Text, SolidFill.ElementName, typeof(SolidFill));
        }

        public SolidFill()
        {
            Style = new SolidFillStyle();
            Style.PropertyChanged += new PropertyChangedEventHandler(Style_PropertyChanged);
        }

        public SolidFillStyle Style { get; private set; }

        public string Color
        {
            get { return Style.Color; }
            set { Style.Color = value; }
        }

        public string Opacity
        {
            get { return Style.Opacity; }
            set { Style.Opacity = value; }
        }

        public static NodeType NodeType
        {
            get { return _nodeType; }
        }
		
		public override object Clone()
		{
            throw new NotImplementedException();
		}

		internal override bool Cascade(Style style)
		{
            return Style.Cascade(style);
		}

        public override void ReadXml(XmlReader reader)
        {
            var solidFill = new SolidFill();

            while (reader.MoveToNextAttribute())
                if (!Style.TryReadXmlAttribute(reader))
                    MapXmlReader.HandleUnexpectedAttribute(reader.LocalName);

            reader.MoveToElement();
            if (!reader.IsEmptyElement && reader.Read() && reader.MoveToContent() != XmlNodeType.EndElement)
                throw new Exception("SolidFill element should be empty");
            reader.Read();
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

        void Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnNotifyPropertyChanged(e);
        }
    }

}
