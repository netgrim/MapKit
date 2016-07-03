using System;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;

namespace MapKit.Core
{
    [DebuggerDisplay("SolidFillStyle: Label = {Label}, Path={NodePath}")]
    public class SolidFillStyle : Style
	{
        internal const string ElementName = "solidFillStyle";
        internal const string ColorField = "color";
        internal const string OpacityField = "opacity";
        internal const string ColorPropertyName = "Color";
        internal const string OpacityPropertyName = "Opacity";
        internal readonly static string Text = "Solid Fill Style";

        private static StyleNodeType _nodeType;

		private string _color;
        private string _opacity;

        static SolidFillStyle()
        {
            _nodeType = new StyleNodeType(SolidFillStyle.Text, SolidFillStyle.ElementName, typeof(SolidFillStyle));
        }

        public string Color
        {
			get { return _color; }
			set
			{
				_color =  value;
				Fields |= SolidFillStyleFields.Color;
                OnFieldChanged(ColorPropertyName);
			}
		}

        public string Opacity
		{
			get { return _opacity; }
			set
			{
				_opacity = value;
				Fields |= SolidFillStyleFields.Opacity;
                OnFieldChanged(OpacityPropertyName);
            }
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
            //var solidFillStyle = style as SolidFillStyle;
            //if (solidFillStyle == null || solidFillStyle.Id != Id) return false;

            //Cascade(solidFillStyle);
            return true;
        }

        [Browsable(false)]
        internal SolidFillStyleFields Fields { get; set; }

        [Browsable(false)]
        internal SolidFillStyleFields InheritedFields { get; set; }

		private void Cascade(SolidFillStyle style)
		{
            var missing = (style.Fields | style.InheritedFields) & ~Fields;
			if (missing == SolidFillStyleFields.None) return;

			foreach (SolidFillStyleFields fields in Enum.GetValues(typeof(SolidFillStyleFields)))
				if (missing.HasFlag(fields))
					switch (fields)
					{
						case SolidFillStyleFields.Color: 
                            _color = style.Color;
                            OnNotifyPropertyChanged(ColorPropertyName);
                            break;
						case SolidFillStyleFields.Opacity: 
                            _opacity = style.Opacity;
                            OnNotifyPropertyChanged(OpacityPropertyName);
                            break;
					}
            InheritedFields = missing;
		}

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == SolidFillStyle.ColorField) Color = reader.Value;
            else if (reader.LocalName == SolidFillStyle.VisibleField) Visible = Convert.ToBoolean(reader.Value);
            else if (reader.LocalName == SolidFillStyle.OpacityField) Opacity = reader.Value;
            else if (!base.TryReadXmlAttribute(reader)) return false;
            return true;
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            base.WriteXmlAttributes(writer);

            if (!string.IsNullOrEmpty(Color) && Fields.HasFlag(SolidFillStyleFields.Color))
                writer.WriteAttributeString(ColorField, Color);
            if (!string.IsNullOrEmpty(Opacity) && Fields.HasFlag(SolidFillStyleFields.Opacity))
                writer.WriteAttributeString(OpacityField, Opacity);
        }
        
        public override NodeType GetNodeType()
        {
            return NodeType;
        }
    }

	[Flags]
	public enum SolidFillStyleFields
	{
		None = 0,
		Color = 0x1,
		Opacity = 0x2,
	}
}
