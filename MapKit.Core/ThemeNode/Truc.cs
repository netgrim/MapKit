using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;

namespace MapKit.Core
{

    class InheritableColorTypeConverter : TypeConverter
    {
        
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, System.Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var original = (InheritableColor)context.PropertyDescriptor.GetValue(context.Instance);
            original.Expression = (string)value;

            context.PropertyDescriptor.SetValue(context.Instance, null);
            return original;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value == null)
                return null;
            return base.ConvertTo(context, culture, value, destinationType);
            //var color = (Stroke.InheritableColor)value;
            //return color.Value;
        }
    }

    public class InheritableColorEditor : UITypeEditor
    {
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            var inheritableColor = (InheritableColor)e.Value;

            using (var brush = new SolidBrush(inheritableColor.Value))
                e.Graphics.FillRectangle(brush, e.Bounds);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var inheritableColor = (InheritableColor)value;

            inheritableColor.Value = (Color)new ColorEditor().EditValue(context, provider, inheritableColor.Value);

            return inheritableColor;
        }
    }
}
