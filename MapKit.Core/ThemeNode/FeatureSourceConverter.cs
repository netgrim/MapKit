using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MapKit.Core
{
    class FeatureSourceConverter : TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return GetValueTypeConverter(context).GetPropertiesSupported();            
        }

        private TypeConverter GetValueTypeConverter(ITypeDescriptorContext context)
        {
            return TypeDescriptor.GetConverter(context.PropertyDescriptor.GetValue(context.Instance));
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, System.Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value, new[] { new BrowsableAttribute(true) });
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return GetValueTypeConverter(context).CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type destinationType)
        {
            return GetValueTypeConverter(context).CanConvertFrom(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return GetValueTypeConverter(context).ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return GetValueTypeConverter(context).ConvertFrom(context, culture, value);
        }
    }
}
