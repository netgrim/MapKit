using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;

namespace MapKit.Core
{
    class FeatureSourceEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            var valueEditor = GetValueEditor(context);
            return valueEditor != null 
                ? valueEditor.GetEditStyle(context)
                : base.GetEditStyle(context);
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var valueEditor = GetValueEditor(context);
            return valueEditor != null
                ? valueEditor.EditValue(context, value)
                : base.EditValue(context, provider, value);
        }

        private UITypeEditor GetValueEditor(ITypeDescriptorContext context)
        {
            return (UITypeEditor)TypeDescriptor.GetEditor(context.PropertyDescriptor.GetValue(context.Instance), typeof(UITypeEditor));
        }
    }
}
