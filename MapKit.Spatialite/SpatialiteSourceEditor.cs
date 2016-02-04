using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;

namespace MapKit.Spatialite
{
    class SpatialiteSourceEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (var f = new SourcePropDialog())
            {
                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    MessageBox.Show("OK");
            }
            return value;
        }
    }
}
