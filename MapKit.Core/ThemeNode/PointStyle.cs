using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;

namespace MapKit.Core
{
    [DebuggerDisplay("PointStyle: Label = {Label}, Path={NodePath}")]
    public class PointStyle : Style
	{
        public const string ElementName = "pointStyle";
        private const string AngleField = "angle";
        private const string ScaleXField = "scaleX";
        private const string ScaleYField = "scaleY";
        private const string OpacityField = "opacity";
        private const string OverlappableField = "overlappable";
        private const string AllowOverlapField = "allowOverlap";
        private const string FileField = "file";
        private const string AlignmentField = "alignment";
        private const string ColorField = "color";

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

        private string _angle;
        private string _scaleX;
        private string _scaleY;
        private string _opacity;
        private string _overlappable;
        private string _allowOverlap;
        private string _file;
        private string _color;
        private string _alignment;

        static PointStyle()
        {
            _nodeType = new StyleNodeType(PointStyle.Text, PointStyle.ElementName, typeof(PointStyle));
        }

        public string Angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                Fields |= PointStyleFields.Angle;
                OnFieldChanged(AnglePropertyName);
            }
        }

        public string ScaleX
        {
            get { return _scaleX; }
            set
            {
                _scaleX = value;
                Fields |= PointStyleFields.ScaleX;
                OnFieldChanged(ScaleXPropertyName);
            }
        }

        public string ScaleY
        {
            get { return _scaleY; }
            set
            {
                _scaleY = value;
                Fields |= PointStyleFields.ScaleY;
                OnFieldChanged(ScaleYPropertyName);
            }
        }

        public string Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                Fields |= PointStyleFields.Opacity;
                OnFieldChanged(OpacityPropertyName);
            }
        }

        public string Overlappable
        {
            get { return _overlappable; }
            set
            {
                _overlappable = value;
                Fields |= PointStyleFields.Overlappable;
                OnFieldChanged(OverlappablePropertyName);
            }
        }

        public string AllowOverlap
        {
            get { return _allowOverlap; }
            set
            {
                _allowOverlap = value;
                Fields |= PointStyleFields.AllowOverlap;
                OnFieldChanged(AllowOverlapPropertyName);
            }
        }

        public string File
        {
            get { return _file; }
            set
            {
                _file = value;
                Fields |= PointStyleFields.File;
                OnFieldChanged(FilePropertyName);
            }
        }

        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                Fields |= PointStyleFields.Color;
                OnFieldChanged(ColorPropertyName);
            }
        }

        public string Alignment
        {
            get { return _alignment; }
            set
            {
                _alignment = value;
                Fields |= PointStyleFields.Alignment;
                OnFieldChanged(AlignmentPropertyName);
            }
        }

        [Browsable(false)]
        internal PointStyleFields Fields { get; set; }

        [Browsable(false)]
        internal PointStyleFields InheritedFields { get; set; }

        public static string Text
        {
            get { return "PointStyle"; }
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
            //var pointStyle = style as PointStyle;
            //if (pointStyle == null || pointStyle.Id != Id) return false;
            
            //Cascade(pointStyle);
            return true;
        }

        private void Cascade(PointStyle pointStyle)
		{
            var missing = (pointStyle.Fields | pointStyle.InheritedFields) & ~Fields;
			if (missing == PointStyleFields.None) return;

			foreach (PointStyleFields fields in Enum.GetValues(typeof(PointStyleFields)))
				if (missing.HasFlag(fields))
					switch (fields)
					{
						case PointStyleFields.Angle: 
                            _angle = pointStyle.Angle;
                            OnNotifyPropertyChanged(AnglePropertyName);
                            break;
						case PointStyleFields.ScaleX: 
                            _scaleX = pointStyle.ScaleX;
                            OnNotifyPropertyChanged(ScaleXPropertyName);
                            break;
						case PointStyleFields.ScaleY: 
                            _scaleY = pointStyle.ScaleY;
                            OnNotifyPropertyChanged(ScaleYPropertyName);
                            break;
						case PointStyleFields.Opacity:
                            _opacity = pointStyle.Opacity;
                            OnNotifyPropertyChanged(OpacityPropertyName);
                            break;
						case PointStyleFields.Overlappable: 
                            _overlappable = pointStyle.Overlappable;
                            OnNotifyPropertyChanged(OverlappablePropertyName);
                            break;
						case PointStyleFields.AllowOverlap: 
                            _allowOverlap = pointStyle.AllowOverlap;
                            OnNotifyPropertyChanged(AllowOverlapPropertyName);
                            break;
                        case PointStyleFields.File: 
                            _file = pointStyle.File;
                            OnNotifyPropertyChanged(FilePropertyName);
                            break;
                        case PointStyleFields.Alignment: 
                            _alignment = pointStyle.Alignment;
                            OnNotifyPropertyChanged(AlignmentPropertyName);
                            break;
                        case PointStyleFields.Color: 
                            _color = pointStyle.Color;
                            OnNotifyPropertyChanged(ColorPropertyName);
                            break;
					}
            InheritedFields = missing;
        }

        protected internal override void WriteXmlAttributes(XmlWriter writer)
        {
            base.WriteXmlAttributes(writer);

            if (!string.IsNullOrEmpty(Angle) && Fields.HasFlag(PointStyleFields.Angle)) writer.WriteAttributeString(AngleField, Angle);
            if (!string.IsNullOrEmpty(ScaleX) && Fields.HasFlag(PointStyleFields.ScaleX)) writer.WriteAttributeString(ScaleXField, ScaleX);
            if (!string.IsNullOrEmpty(ScaleY) && Fields.HasFlag(PointStyleFields.ScaleY)) writer.WriteAttributeString(ScaleYField, ScaleY);
            if (!string.IsNullOrEmpty(Opacity) && Fields.HasFlag(PointStyleFields.Opacity)) writer.WriteAttributeString(OpacityField, Opacity);
            if (!string.IsNullOrEmpty(Overlappable) && Fields.HasFlag(PointStyleFields.Overlappable)) writer.WriteAttributeString(OverlappableField, Overlappable);
            if (!string.IsNullOrEmpty(AllowOverlap) && Fields.HasFlag(PointStyleFields.AllowOverlap)) writer.WriteAttributeString(AllowOverlapField, AllowOverlap);
            if (!string.IsNullOrEmpty(File) && Fields.HasFlag(PointStyleFields.File)) writer.WriteAttributeString(FileField, File);
            if (!string.IsNullOrEmpty(AlignmentField) && Fields.HasFlag(PointStyleFields.Alignment)) writer.WriteAttributeString(AlignmentField, AlignmentField);
            if (!string.IsNullOrEmpty(Color) && Fields.HasFlag(PointStyleFields.Color)) writer.WriteAttributeString(ColorField, Color);
        }

        public override NodeType GetNodeType()
        {
            return NodeType;
        }

        protected internal override bool TryReadXmlAttribute(XmlReader reader)
        {
            if (reader.LocalName == PointStyle.FileField) File = reader.Value;
            else if (reader.LocalName == PointStyle.AllowOverlapField) AllowOverlap = reader.Value;
            else if (reader.LocalName == PointStyle.OverlappableField) Overlappable = reader.Value;
            else if (reader.LocalName == PointStyle.OpacityField) Opacity = reader.Value;
            else if (reader.LocalName == PointStyle.ScaleXField) ScaleX = reader.Value;
            else if (reader.LocalName == PointStyle.ScaleYField) ScaleY = reader.Value;
            else if (reader.LocalName == PointStyle.AngleField) Angle = reader.Value;
            else if (reader.LocalName == PointStyle.VisibleField) Visible = Convert.ToBoolean(reader.Value);
            else if (reader.LocalName == PointStyle.AlignmentField) Alignment = reader.Value;
            else if (reader.LocalName == PointStyle.ColorField) Color = reader.Value;
            else if (!base.TryReadXmlAttribute(reader)) return false;
            return true;
        }
    }

    [Flags]
    public enum PointStyleFields
    {
        None = 0,
        Angle = 0x1,
        ScaleX = 0x2,
        ScaleY = 0x4,
        Alignment = 0x8,
        Color = 0x10,
        Opacity = 0x20,
        Overlappable = 0x40,
        AllowOverlap = 0x80,
        File = 0x100,
    }
}
