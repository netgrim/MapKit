using System;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Cyrez.UI
{
    public class OpacityValueEditor :UITypeEditor
    {
        UserControl _c;
        IWindowsFormsEditorService edSvc;
        Color _value;

        public OpacityValueEditor()
        {
             _c = new UserControl();
             _c.Size = new Size(255, SystemInformation.MenuHeight);
             _c.Paint += gradient_Paint;
            
             _c.MouseUp += c_MouseUp;
             _c.MouseMove += c_MouseMove; 
        }

        void c_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int alpha = e.X;
                if (alpha < 0)
                    alpha = 0;
                else if (alpha > 255)
                    alpha = 255;
                _value = Color.FromArgb(alpha, _value);
                _c.Invalidate();
            }
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _value = (Color)value;
            edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
                edSvc.DropDownControl(_c);

            value = _value;
            return base.EditValue(context, provider, value);
        }

        void c_MouseUp(object sender, MouseEventArgs e)
        {
            int alpha = e.X;
            if (alpha < 0)
                alpha = 0;
            else if (alpha > 255)
                alpha = 255; _value = Color.FromArgb(alpha, _value);
            edSvc.CloseDropDown();
        }


        void gradient_Paint(object sender, PaintEventArgs e)
        {
            var c = (UserControl)sender;
            using (Brush checkerBrush = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.LightGray))
                e.Graphics.FillRectangle(checkerBrush, e.ClipRectangle);

            using (Brush linearGradiant = new LinearGradientBrush(c.ClientRectangle, Color.FromArgb(0, _value), Color.FromArgb(255, _value), 0f))
                e.Graphics.FillRectangle(linearGradiant, e.ClipRectangle);

            var thumb = new Rectangle(_value.A - 5, 0, 10, _c.Height);
            ControlPaint.DrawBorder3D(e.Graphics, thumb, Border3DStyle.RaisedInner);
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override bool IsDropDownResizable
        {
            get
            {
                return false;
            }
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            using (Brush checkerBrush = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.LightGray))
                e.Graphics.FillRectangle(checkerBrush, e.Bounds);

            using (var brush = new SolidBrush((Color)e.Value))
                e.Graphics.FillRectangle(brush, e.Bounds);
        }

        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
            
        }
    }
}
