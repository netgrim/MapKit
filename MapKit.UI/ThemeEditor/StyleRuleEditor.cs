using System;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;
using Cyrez.GIS.Data;

namespace Cyrez.GIS.UI
{
    class StyleRuleEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override bool IsDropDownResizable
        {
            get
            {
                return true;
            }
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
                const int nullIndex = 0;
                const int newIndex = 1;

            var instance = context.Instance as StyleRule;
            if (instance != null)
            {
                var styleRule = (instance).Row;
                var ds = (styleRule.Table.DataSet as ThemeDs);
                var styles = ds.Style.OrderBy(x => x.Name);

                var listBox = new ListBox();
                listBox.IntegralHeight = false;
                listBox.DisplayMember = ds.Style.NameColumn.ColumnName;
                listBox.BorderStyle = BorderStyle.None;
                
                listBox.BeginUpdate();
                listBox.Items.Add("[None]");
                listBox.Items.Add("[New]");
                listBox.Items.AddRange(styles.ToArray());
                if (value != null)
                    listBox.SelectedItem = ((Style)value).StyleRow;
                else
                    listBox.SelectedIndex = nullIndex;

                listBox.Dock = DockStyle.Fill;
                listBox.EndUpdate();
                                
                var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    listBox.SelectedValueChanged += (sender, e) => edSvc.CloseDropDown();

                    edSvc.DropDownControl(listBox);
                }

                if (listBox.SelectedIndex == nullIndex)
                    return null;
                if (listBox.SelectedIndex == newIndex)
                {
                    using(var form = new NewStyle())
                        if (edSvc.ShowDialog(form) == DialogResult.OK)
                        {
                            var style = ds.Style.NewStyleRow();
                            style.Name = form.StyleName;
                            ds.Style.AddStyleRow(style);

                            switch (form.StyleType)
                            {
                                case StyleType.AreaStyle: 
                                    var areaStyle = new AreaStyle(ds.AreaStyle.NewAreaStyleRow(), style);
                                    areaStyle.AreaStyleRow.StyleRow = style;
                                    ds.AreaStyle.AddAreaStyleRow(areaStyle.AreaStyleRow);
                                    return areaStyle;
                                case StyleType.LineStyle: 
                                    var lineStyle = new LineStyle(ds.LineStyle.NewLineStyleRow(), style);
                                    lineStyle.LineStyleRow.StyleRow = style;
                                    ds.LineStyle.AddLineStyleRow(lineStyle.LineStyleRow);
                                    return lineStyle;
                                case StyleType.PointStyle: 
                                    var pointStyle = new PointStyle(ds.PointStyle.NewPointStyleRow(), style); 
                                    pointStyle.PointStyleRow.StyleRow = style;
                                    ds.PointStyle.AddPointStyleRow(pointStyle.PointStyleRow);
                                    return pointStyle;
                                case StyleType.TextStyle: 
                                    var textStyle = new TextStyle(ds.TextStyle.NewTextStyleRow(), style);
                                    textStyle.TextStyleRow.StyleRow = style;
                                    ds.TextStyle.AddTextStyleRow(textStyle.TextStyleRow);
                                    return textStyle;
                                default:
                                    Debug.Fail("enum");
                                    return value;
                            }
                        }
                    return value;
                }
                return new Style((ThemeDs.StyleRow)listBox.SelectedItem);
            }
            return base.EditValue(context, provider, value);
        }

    }
}
